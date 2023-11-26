using Smart_Shop.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Smart_Shop.Converters
{
    public class StatusCodeToColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = value switch
            {
                "Success" => new SolidColorBrush(Colors.Green),
                "Error" => new SolidColorBrush(Colors.Red),
                "Warning" => new SolidColorBrush(Colors.Yellow),
                _ => new SolidColorBrush(Colors.Black),

            };

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
