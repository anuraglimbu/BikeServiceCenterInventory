namespace BikeServiceCenterInventory.Models;

public class Record
{
	public int Id { get; set; }

	public int ItemId { get; set; }

	public int QuantityTakenOut { get; set; }

	public string ApprovedBy { get; set; }

	public string TakenBy { get; set; }

	public DateTime? DateTimeTakenOut { get; set; }
}
