/*
===================================================================
	 The AuthenticationService is a helper class that provides
	 the login and logout functionality of the application
	 
	 This is my custom implementation as per the requirements
	 of the BikeService Center Inventory Management System.
===================================================================
*/

using BikeServiceCenterInventory.Models;
using BikeServiceCenterInventory.Data;

namespace BikeServiceCenterInventory.Services;

public class AuthenticationService
{
	public Task<bool> isLoggedIn()
	{
		if (Preferences.Default.ContainsKey("loggedIn"))
		{  
			if (Preferences.Default.Get("loggedIn", false))
			{
				return Task.FromResult(true);
			}
		}

		return Task.FromResult(false);
	}

	public Task<bool> logIn(String username, String password)
	{
		AdminData adminDbContext = new AdminData();

		List<Admin> result = adminDbContext.admins.Where(admin => admin.UserName.Equals(username) && admin.Password.Equals(password)).ToList();

		if(result.Count > 0)
		{
			var admin = result.First();
			Preferences.Default.Set("loggedIn", true);
			Preferences.Default.Set("userId", admin.Id);
			Preferences.Default.Set("userName", admin.UserName);
			Preferences.Default.Set("loggedInAt", DateTime.Now);
			return Task.FromResult(true);
		}

		return Task.FromResult(false);
	}

	public bool logOut()
	{
		Preferences.Default.Clear();
		return true;
	}

	public Task<List<Admin>> loginTest()
	{
		AdminData adminDbContext = new AdminData();

		return Task.FromResult(adminDbContext.admins);
	}

	public Task<String> getUserName()
	{
		AdminData adminDbContext = new AdminData();

		String username = Preferences.Default.Get("userName", "NA");

		return Task.FromResult(username);
	}

	public Task<int> getUserId()
	{
		AdminData adminDbContext = new AdminData();

		int id = Preferences.Default.Get("userId", -1);

		return Task.FromResult(id);
	}

	public Task<Admin> getUser()
	{
		AdminData adminDbContext = new AdminData();

		int id = Preferences.Default.Get("userId", -1);

		Admin result = adminDbContext.admins.First(admin => admin.Id == id);

		return Task.FromResult(result);
	}

	public Task<String> getUserFullName()
	{
		AdminData adminDbContext = new AdminData();

		int id = Preferences.Default.Get("userId", -1);

		Admin user = adminDbContext.admins.First(admin => admin.Id == id);
		String result = user.FirstName + " " + user.LastName;

		return Task.FromResult(result);
	}

	public Task<bool> updateUser(Admin newAdmin)
	{
		AdminData adminDbContext = new AdminData();

		Admin replacement = adminDbContext.admins.FirstOrDefault(admin => admin.Id == newAdmin.Id);
		var index = adminDbContext.admins.IndexOf(replacement);

		if (index != -1) adminDbContext.admins[index] = newAdmin;

		Preferences.Default.Set("userId", newAdmin.Id);
		Preferences.Default.Set("userName", newAdmin.UserName);

		return Task.FromResult(adminDbContext.SaveChanges());
	}

}