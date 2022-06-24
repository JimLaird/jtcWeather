using CommunityToolkit.Maui;

namespace jtcWeather;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>().UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                fonts.AddFont("Mulish-Black.ttf", "MulishBlack");
                fonts.AddFont("Mulish-Bold.ttf", "MulishBold");
                fonts.AddFont("Mulish-ExtraBold.ttf", "MulishExtraBold");
                fonts.AddFont("Mulish-ExtraLight.ttf", "MulishExtraLight");
                fonts.AddFont("Mulish-Light.ttf", "MulishLight");
                fonts.AddFont("Mulish-Medium.ttf", "MulishMedium");
                fonts.AddFont("Mulish-Regular.ttf", "MulishRegular");
                fonts.AddFont("Mulish-SemiBold.ttf", "MulishSemiBold");
				fonts.AddFont("weathericons-regular-webfont.ttf", "WeatherIcons");
            });


		string dbPath = FileAccessHelper.GetLocalFilePath("faves.db3");


		//	Add Services
		builder.Services.AddSingleton<DataRepository>(s => ActivatorUtilities.CreateInstance<DataRepository>(s, dbPath));
		builder.Services.AddSingleton<RestService>();
		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddSingleton<IGeocoding>(Geocoding.Default);


		//	Add View Models
		builder.Services.AddSingleton<WeatherViewModel>();
		builder.Services.AddTransient<FavouritesViewModel>();
		

		//	Add Views
		builder.Services.AddSingleton<WeatherPage>();
		builder.Services.AddTransient<FavouritesPage>();
		

		return builder.Build();
	}
}
