using System.Net.Http;
using Newtonsoft.Json;

namespace jtcWeather.Services
{
    public class RestService
    {
        HttpClient httpClient;

        public RestService()
        {
            httpClient = new HttpClient();
        }

        public async Task<WeatherData> GetWeatherData(string query)
        {
            WeatherData weatherData = null;

            try
            {
                var response = await httpClient.GetAsync(query);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            return weatherData;
        }
    }
}
