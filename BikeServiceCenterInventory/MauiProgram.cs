using Microsoft.Extensions.Logging;
using BikeServiceCenterInventory.Data;
using Microsoft.Extensions.DependencyInjection;

using BikeServiceCenterInventory.Services;
using BikeServiceCenterInventory.Shared;

namespace BikeServiceCenterInventory;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		/*
			Registering our service classes as Singleton dependency injection objects
			which means that only one instance of the class will be kept alive and
			will be injected whenever it is used during the application lifecycle.
		*/
		builder.Services.AddSingleton<AuthenticationService>(); // register AuthenticationService class
		builder.Services.AddSingleton<InventoryService>();      // register InventoryService class
		builder.Services.AddSingleton<RecordKeepingService>();  // register RecordKeepingService class

		return builder.Build();
	}
}
