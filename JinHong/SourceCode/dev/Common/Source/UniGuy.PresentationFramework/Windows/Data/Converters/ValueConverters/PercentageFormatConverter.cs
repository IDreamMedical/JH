using System;
using System.Globalization;
using System.Windows.Data;

namespace UniGuy.Controls.Converters
{
    public class PercentageFormatConverter : IValueConverter
    {
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double num = (double)value;
            return string.Format(CultureInfo.CurrentCulture, "{0:0.##}%", new object[] { num });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}
