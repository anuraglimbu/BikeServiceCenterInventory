/*
===================================================================
	 The InventoryService is a helper class that provides
	 the inventory management functionality of the application
	 
	 This is my custom implementation as per the requirements
	 of the BikeService Center Inventory Management System.
===================================================================
*/

using BikeServiceCenterInventory.Models;
using BikeServiceCenterInventory.Data;

namespace BikeServiceCenterInventory.Services;

public class InventoryService
{
	public Task<bool> addItem(String name, int qty)
	{
		ItemData itemDbContext = new ItemData();

		int LastID = itemDbContext.items.Last().Id + 1;
		qty = (qty<0)? 0 : qty;
		DateTime? dateTime = (qty > 0) ? DateTime.Now : null;

		Item newItem = new Item 
		{
			Id = LastID,
			ItemName = name,
			Quantity = qty,
			LastRestocked = dateTime,
			LastTakenOut = null
		};

		itemDbContext.items.Add(newItem);

		return Task.FromResult(itemDbContext.SaveChanges());
		//return Task.FromResult(false);
	}

	public Task<bool> updateItem(Item updateItem)
	{
		ItemData itemDbContext = new ItemData();

		Item restockedItem = itemDbContext.items.First(item => item.Id == updateItem.Id);
		var index = itemDbContext.items.IndexOf(restockedItem);

		Item newItem = new Item
		{
			Id = restockedItem.Id,
			ItemName = updateItem.ItemName,
			Quantity = updateItem.Quantity,
			LastRestocked = updateItem.LastRestocked,
			LastTakenOut = updateItem.LastTakenOut
		};

		if (index != -1) itemDbContext.items[index] = newItem;

		return Task.FromResult(itemDbContext.SaveChanges());
		//return Task.FromResult(false);
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
		//return Task.FromResult(false);
	}

	public Task<List<Item>> getAllItems()
	{
		ItemData itemDbContext = new ItemData();

		return Task.FromResult(itemDbContext.items.ToList());
	}

	public Task<int> getQtyInStock(int id)
	{
		ItemData itemDbContext = new ItemData();

		return Task.FromResult(itemDbContext.items.First(item => item.Id == id).Quantity);
	}
}