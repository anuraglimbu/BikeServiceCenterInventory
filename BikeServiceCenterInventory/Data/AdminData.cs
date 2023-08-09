using System.Text.Json;

using BikeServiceCenterInventory.Constants;
using BikeServiceCenterInventory.Models;
using BikeServiceCenterInventory.Services;


namespace BikeServiceCenterInventory.Data;

public class AdminData
{
    private String _documnentName = "admins.json";
    private JsonHandlerService _jsonHandler = new JsonHandlerService();
    public List<Admin> admins;

    public AdminData()
    {
        if (!_jsonHandler.exists(_documnentName)) writeSeedAdminData();

        
        string jsonString = _jsonHandler.read(_documnentName);
        if (jsonString == null)
        {
            writeSeedAdminData();
            jsonString = _jsonHandler.read(_documnentName);
        }

        admins = JsonSerializer.Deserialize<List<Admin>>(jsonString);
    }

    public bool SaveChanges()
    {
        string jsonString = JsonSerializer.Serialize(admins);

        return _jsonHandler.write(_documnentName, jsonString);
    }

    

    public void writeSeedAdminData()
    {
        var seedAdminData = SeedData.AdminSeed;

        string seedJsonData = JsonSerializer.Serialize(seedAdminData);
        _jsonHandler.write(_documnentName, seedJsonData);
    }
}
