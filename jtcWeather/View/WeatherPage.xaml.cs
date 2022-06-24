namespace jtcWeather.View;

public partial class WeatherPage : ContentPage
{
	
	public WeatherPage(WeatherViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

		if (args != null)
        {
			entryCityName.Text = lblCity.Text;
        }
    }
}