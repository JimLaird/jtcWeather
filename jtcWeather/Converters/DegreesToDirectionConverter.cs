using System.Globalization;

namespace jtcWeather.Converters
{
    public class DegreesToDirectionConverter : IValueConverter
    {
        string direction;
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            switch (value)
            {
                case >= 0 and <= 11:
                    direction = "N";
                    break;
                case > 11 and <= 34:
                    direction = "NNE";
                    break;
                case > 34 and <= 56:
                    direction = "NE";
                    break;
                case > 56 and <= 79:
                    direction = "ENE";
                    break;
                case > 79 and <= 101:
                    direction = "E";
                    break;
                case > 101 and <= 124:
                    direction = "ESE";
                    break;
                case > 124 and <= 146:
                    direction = "SE";
                    break;
                case > 146 and <= 169:
                    direction = "SSE";
                    break;
                case > 169 and <= 191:
                    direction = "S";
                    break;
                case > 191 and <= 214:
                    direction = "SSW";
                    break;
                case > 214 and <= 236:
                    direction = "SW";
                    break;
                case > 236 and <= 259:
                    direction = "WSW";
                    break;
                case > 259 and <= 281:
                    direction = "W";
                    break;
                case > 281 and <= 304:
                    direction = "WNW";
                    break;
                case > 304 and <= 326:
                    direction = "NW";
                    break;
                case > 326 and <= 349:
                    direction = "NNW";
                    break;
                case > 349 and <= 360:
                    direction = "N";
                    break;
            }

            return direction;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
