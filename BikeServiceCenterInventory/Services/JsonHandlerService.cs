/*
==================================================================
     The JsonHandlerService is a helper class that provides
     an abstracted way to manage json file operations
     in the AppData folder for the MAUI Application.
     
     This is my custom implementation as per the requirements
     of the BikeService Center Inventory Management System.
==================================================================
*/

namespace BikeServiceCenterInventory.Services;

public class JsonHandlerService
{
    private String _dataFolder = FileSystem.Current.AppDataDirectory;

    public bool exists(String fileName)
    {
        String file = Path.Combine(_dataFolder, fileName);

        if (File.Exists(file))
        {
            return true;
        }

        return false;
    }

    public bool create(String fileName)
    {
        String file = Path.Combine(_dataFolder, fileName);

        try
        {
            using FileStream outputStream = File.Create(file);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public String read(String fileName)
    {
        String file = Path.Combine(_dataFolder, fileName);

        return File.ReadAllText(file);
    }

    public bool write(String fileName, String jsonString)
    {
        String file = Path.Combine(_dataFolder, fileName);

        try
        {
            File.WriteAllText(file, jsonString);
        }
        catch
        {
            return false;
        }

        return true;
    }

}
