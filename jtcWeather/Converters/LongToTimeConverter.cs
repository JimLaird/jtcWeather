using System.Globalization;

namespace jtcWeather.Converters

{
    public class LongToTimeConverter : IValueConverter
    {
        DateTime _time = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int dateTime = (int)value;

            var date = _time.AddSeconds(dateTime);

            string dateString = $"{date.ToString("HH:mm")}";

            //return $"{_time.AddSeconds(dateTime).ToString()}";

            return dateString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
