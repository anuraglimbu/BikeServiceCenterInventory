using System.Text.Json;

using BikeServiceCenterInventory.Constants;
using BikeServiceCenterInventory.Models;
using BikeServiceCenterInventory.Services;

namespace BikeServiceCenterInventory.Data;

public class RecordData
{
	private String _documnentName = "records.json";
	private JsonHandlerService _jsonHandler = new JsonHandlerService();
	public List<Record> records;

	public RecordData()
	{
		if (!_jsonHandler.exists(_documnentName)) writeSeedItemData();


		string jsonString = _jsonHandler.read(_documnentName);
		if (jsonString == null)
		{
			writeSeedItemData();
			jsonString = _jsonHandler.read(_documnentName);
		}

		records = JsonSerializer.Deserialize<List<Record>>(jsonString);
	}

	public bool SaveChanges()
	{
		string jsonString = JsonSerializer.Serialize(records);

		return _jsonHandler.write(_documnentName, jsonString);
	}

	public void writeSeedItemData()
	{
		var seedRecordData = SeedData.RecordSeed;

		string seedJsonData = JsonSerializer.Serialize(seedRecordData);
		_jsonHandler.write(_documnentName, seedJsonData);
	}
}
