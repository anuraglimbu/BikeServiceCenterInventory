namespace BikeServiceCenterInventory.Models;

public class Item
{
    public int Id { get; set; }

    public string ItemName { get; set; }

    public int Quantity { get; set; }

    public DateTime? LastRestocked { get; set; }

    public DateTime? LastTakenOut { get; set; }
}
