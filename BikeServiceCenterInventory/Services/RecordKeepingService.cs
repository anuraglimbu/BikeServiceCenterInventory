/*
===================================================================
	 The RecordKeepingService is a helper class that provides
	 the records management functionality of the application
	 
	 This is my custom implementation as per the requirements
	 of the BikeService Center Inventory Management System.
===================================================================
*/


using BikeServiceCenterInventory.Models;
using BikeServiceCenterInventory.Data;

namespace BikeServiceCenterInventory.Services;

public class RecordKeepingService
{
	public async Task<bool> addRecord(int itemId, int qty, String takenByName)
	{
		ItemData itemDbContext = new ItemData();
		RecordData recordDbContext = new RecordData();

		AuthenticationService authenticationService = new AuthenticationService();
		InventoryService inventoryService = new InventoryService();

		int LastID = recordDbContext.records.Last().Id + 1;
		qty = (qty < 0) ? 0 : qty;
		DateTime? dateTime = (qty > 0) ? DateTime.Now : null;

		String name = await authenticationService.getUserFullName();


		Record newRecord = new Record
		{
			Id = LastID,
			ItemId = itemId,
			QuantityTakenOut = qty,
			ApprovedBy = name,
			TakenBy = takenByName,
			DateTimeTakenOut = dateTime,
		};

		Item selectedItem = itemDbContext.items.First(item => item.Id == itemId);
		int newQty = selectedItem.Quantity - qty;

		Item newItem = new Item
		{
			Id = selectedItem.Id,
			ItemName = selectedItem.ItemName,
			Quantity = newQty,
			LastRestocked = selectedItem.LastRestocked,
			LastTakenOut = dateTime,
		};

		recordDbContext.records.Add(newRecord);

		if (await inventoryService.updateItem(newItem) && recordDbContext.SaveChanges())
		{
			return true;
		}

		return false;
	}

	public Task<bool> restockItem(int id, int qty)
	{
		ItemData itemDbContext = new ItemData();

		Item restockedItem = itemDbContext.items.First(item => item.Id == id);
		var index = itemDbContext.items.IndexOf(restockedItem);

		qty = (qty <= 0) ? 0 : (restockedItem.Quantity + qty);
		DateTime? dateTime = (qty > restockedItem.Quantity) ? DateTime.Now : restockedItem.LastRestocked;

		Item newItem = new Item
		{
			Id = restockedItem.Id,
			ItemName = restockedItem.ItemName,
			Quantity = qty,
			LastRestocked = dateTime,
			LastTakenOut = restockedItem.LastTakenOut
		};

		if (index != -1) itemDbContext.items[index] = newItem;

		return Task.FromResult(itemDbContext.SaveChanges());
	}

	public Task<List<Record>> getAllRecords()
	{
		RecordData recordDbContext = new RecordData();

		return Task.FromResult(recordDbContext.records.ToList());
	}

	public Task<List<BaseRecordView>> getBaseRecordViewAllData()
	{
		RecordData recordDbContext = new RecordData();
		ItemData itemDbContext = new ItemData();

		List<BaseRecordView> BaseRecordViewData = new List<BaseRecordView>();

		foreach (Record record in recordDbContext.records)
		{
			BaseRecordView baseRecordView = new BaseRecordView()
			{
				Id = record.Id,
				ItemId = record.Id,
				ItemName = itemDbContext.items.First(item => item.Id == record.Id).ItemName,
				QuantityTakenOut = record.QuantityTakenOut,
				ApprovedBy = record.ApprovedBy,
				TakenBy = record.TakenBy,
				DateTimeTakenOut = record.DateTimeTakenOut,
			};

			BaseRecordViewData.Add(baseRecordView);
		}

		return Task.FromResult(BaseRecordViewData.OrderByDescending(i => i.DateTimeTakenOut).ToList());
	}

	public Task<List<BaseRecordView>> getBaseRecordViewMonthData(DateTime month)
	{
		RecordData recordDbContext = new RecordData();
		ItemData itemDbContext = new ItemData();

		List<BaseRecordView> BaseRecordViewData = new List<BaseRecordView>();

		foreach (Record record in recordDbContext.records)
		{
			if (!((record.DateTimeTakenOut!.Value.Year == month.Year) && (record.DateTimeTakenOut!.Value.Month == month.Month))) continue;

			BaseRecordView baseRecordView = new BaseRecordView()
			{
				Id = record.Id,
				ItemId = record.Id,
				ItemName = itemDbContext.items.First(item => item.Id == record.Id).ItemName,
				QuantityTakenOut = record.QuantityTakenOut,
				ApprovedBy = record.ApprovedBy,
				TakenBy = record.TakenBy,
				DateTimeTakenOut = record.DateTimeTakenOut,
			};

			BaseRecordViewData.Add(baseRecordView);
		}

		return Task.FromResult(BaseRecordViewData.OrderByDescending(i => i.DateTimeTakenOut).ToList());
	}

	public Task<DateTime?> getLastApprovedDateTime()
	{
		RecordData recordDbContext = new RecordData();
		return Task.FromResult(recordDbContext.records.Last().DateTimeTakenOut);
	}
}