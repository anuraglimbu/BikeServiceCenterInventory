using System.Text.Json;

using BikeServiceCenterInventory.Constants;
using BikeServiceCenterInventory.Models;
using BikeServiceCenterInventory.Services;

namespace BikeServiceCenterInventory.Data;

public class ItemData
{
    private String _documnentName = "items.json";
    private JsonHandlerService _jsonHandler = new JsonHandlerService();
    public List<Item> items;

    public ItemData()
    {
        if (!_jsonHandler.exists(_documnentName)) writeSeedItemData();


        string jsonString = _jsonHandler.read(_documnentName);
        if (jsonString == null)
        {
            writeSeedItemData();
            jsonString = _jsonHandler.read(_documnentName);
        }

        items = JsonSerializer.Deserialize<List<Item>>(jsonString);
    }

    public bool SaveChanges()
    {
        string jsonString = JsonSerializer.Serialize(items);

        return _jsonHandler.write(_documnentName, jsonString);
    }

    public void writeSeedItemData()
    {
        var seedItemData = SeedData.ItemSeed;

        string seedJsonData = JsonSerializer.Serialize(seedItemData);
        _jsonHandler.write(_documnentName, seedJsonData);
    }
}
