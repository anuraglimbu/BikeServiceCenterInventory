using BikeServiceCenterInventory.Models;

namespace BikeServiceCenterInventory.Constants;

public static class SeedData
{
	public static List<Admin> AdminSeed = new List<Admin>
		{
			new Admin
			{
				Id = 0,
				FirstName = "Saharsha",
				LastName = "Pokharel",
				UserName = "admin",
				Password = "admin"
			},
			new Admin
			{
				Id = 1,
				FirstName = "Test",
				LastName = "User",
				UserName = "admin2",
				Password = "admin2"
			}
		};

	public static List<Item> ItemSeed = new List<Item> 
	{
		new Item
			{
				Id = 0,
				ItemName = "Wheel",
				Quantity = 80,
				LastRestocked = DateTime.Now
			},
			new Item
			{
				Id = 1,
				ItemName = "Screws",
				Quantity = 5000,
				LastRestocked = DateTime.Now
			},
			new Item
			{
				Id = 2,
				ItemName = "Brake Pads",
				Quantity = 100,
				LastRestocked = DateTime.Now
			},
			new Item
			{
				Id = 3,
				ItemName = "Engine Oil",
				Quantity = 120,
				LastRestocked = DateTime.Now
			}
	};

	public static List<Record> RecordSeed = new List<Record>
		{
			new Record
			{
				Id = 0,
				ItemId = 0,
				QuantityTakenOut = 2,
				ApprovedBy = "Saharsha",
				TakenBy = "Harry",
				DateTimeTakenOut = DateTime.Parse("2023-06-22T23:42:53.6791799+05:45")
			},
			new Record
			{
				Id = 1,
				ItemId = 1,
				QuantityTakenOut = 10,
				ApprovedBy = "Saharsha",
				TakenBy = "Harry",
				DateTimeTakenOut = DateTime.Parse("2023-06-22T23:42:55.6791799+05:45")
			},
			new Record
			{
				Id = 2,
				ItemId = 0,
				QuantityTakenOut = 2,
				ApprovedBy = "Saharsha",
				TakenBy = "Harry",
				DateTimeTakenOut = DateTime.Parse("2023-07-22T23:42:53.6791799+05:45")
			},
			new Record
			{
				Id = 3,
				ItemId = 1,
				QuantityTakenOut = 10,
				ApprovedBy = "Saharsha",
				TakenBy = "Harry",
				DateTimeTakenOut = DateTime.Parse("2023-07-22T23:42:55.6791799+05:45")
			},
		};
}
