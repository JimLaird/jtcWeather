
using CommunityToolkit.Maui.Views;

namespace jtcWeather.ViewModel
{
    [QueryProperty("Favourite", "Favourite")]
    public partial class WeatherViewModel : BaseViewModel
    {
        IConnectivity connectivity;
        IGeolocation geolocation;
        IGeocoding geocoding;
        RestService restService;

        [ObservableProperty]
        string cityName;
        [ObservableProperty]
        string latitude;
        [ObservableProperty]
        string longitude;
        [ObservableProperty]
        CancellationTokenSource cancelTokenSource;
        [ObservableProperty]
        bool isCheckingLocation;
        [ObservableProperty]
        int offset;
        [ObservableProperty]
        int sunUp;
        [ObservableProperty]
        int sunDown;

        [ObservableProperty]
        ObservableCollection<WeatherData> weatherList;

        public WeatherViewModel(IConnectivity connectivity, IGeolocation geolocation, IGeocoding geocoding, RestService restService)
        {
            Title = "jtcWeather";
            this.connectivity = connectivity;
            this.geolocation = geolocation;
            this.geocoding = geocoding;
            this.restService = restService;


            TestNav();
        }

        [ObservableProperty]
        Favourite favourite;
        
        private void TestNav()
        {
            if (Favourite is null)
                return;

            CityName = Favourite.Item.ToString();
        }
        

        [ICommand]
        async Task GetCurrentLocationAsync()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Connectivity", $"No internet access detected.\n\n" + $"Please connect and try again.", "OK");
                return;
            }

            try
            {
                isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(30));
                cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, cancelTokenSource.Token);

                if (location != null)
                {
                    latitude = location.Latitude.ToString();
                    longitude = location.Longitude.ToString();

                    var city = await GetGeocodeReverseData(location.Latitude, location.Longitude);

                    CityName = city;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Location Error", $"Unable to complete request: {ex.Message}", "OK");
            }
            finally
            {
                isCheckingLocation = false;
            }
        }

        public void CancelRequest()
        {
            if (isCheckingLocation && cancelTokenSource != null && cancelTokenSource.IsCancellationRequested == false)
                cancelTokenSource.Cancel();
        }

        public async Task<string> GetGeocodeReverseData(double latitude, double longitude)
        {
            IEnumerable<Placemark> placemarks = await geocoding.GetPlacemarksAsync(latitude, longitude);
            Placemark placemark = placemarks?.Where(p => p.Locality != null).FirstOrDefault();

            return placemark.Locality;
        }

        [ICommand]
        async Task SelectFromFavouritesAsync()
        {
            await Shell.Current.GoToAsync($"/{nameof(FavouritesPage)}");

             
        }

        

        [ICommand]
        async Task GetWeatherAsync()
        {
            if (IsBusy)
                return;

            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Connectivity", $"No internet access detected.\n\n" + $"Please connect and try again.", "OK");
                return;
            }

            if (cityName is null || cityName.Length < 3)
            {
                await Shell.Current.DisplayAlert("Enter Location", $"Location Name Incomplete \n\n" + $"Please detect or enter location and try again.", "OK");
                return;
            }

            try
            {
                isCheckingLocation = true;
                IsBusy = true;

                IEnumerable<Location> locations = await geocoding.GetLocationsAsync(CityName);
                Location location = locations?.FirstOrDefault();

                if (location != null)
                {
                    latitude = location.Latitude.ToString();
                    longitude = location.Longitude.ToString();

                    WeatherData weatherData = await restService.GetWeatherData(GenerateRequestURL(Constants.OpenWeatherMapEndpoint));

                    WeatherList = new ObservableCollection<WeatherData>();
                    WeatherList.Add(weatherData);

                    Offset = weatherData.TimezoneOffset;
                    SunUp = weatherData.Current.Sunrise + Offset;
                    SunDown = weatherData.Current.Sunset + Offset;
                    
                    for (int i = 0; i < 48; i++)
                    {
                        weatherData.Hourly[i].Dt = weatherData.Hourly[i].Dt + Offset;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Location Error", $"Unable to complete request: {ex.Message}", "OK");
            }
            finally
            {
                isCheckingLocation = false;
                IsBusy = false;
            }
            
        }

        string GenerateRequestURL(string endPoint)
        {
            string requestURI = endPoint;
            requestURI += $"?lat={latitude}";
            requestURI += $"&lon={longitude}";
            requestURI += $"&units=imperial";
            requestURI += $"&appid={Constants.OpenWeatherMap}";
            return requestURI;
        }
    }
}
