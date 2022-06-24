namespace jtcWeather.View;

public partial class FavouritesPage : ContentPage
{
	public FavouritesPage(FavouritesViewModel viewmodel)
	{
		InitializeComponent();
	    BindingContext = viewmodel = new FavouritesViewModel();
	}

    
}