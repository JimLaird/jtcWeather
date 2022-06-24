
using System.Globalization;

namespace jtcWeather.Converters
{
    public class FarenheitToCelsiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double farenheit = (double)value;
            double celsius = 0.00;

            
            celsius = (farenheit - 32) * 5 / 9;
         
            return Math.Round(celsius).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
