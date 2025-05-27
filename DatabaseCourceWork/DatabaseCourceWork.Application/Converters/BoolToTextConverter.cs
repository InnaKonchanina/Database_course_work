using System.Globalization;
using System.Windows.Data;

namespace DatabaseCourceWork.DesktopApplication.Converters
{
    public class BoolToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var textOptions = ((string)parameter).Split('|');
            return (bool)value ? textOptions[1] : textOptions[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
