namespace jtcWeather;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        //  Add Page Routes
        Routing.RegisterRoute(nameof(FavouritesPage), typeof(FavouritesPage));
        
    }
}
