using System;
using System.Globalization;
using System.Windows.Data;

namespace UniGuy.Controls.Converters
{
    public sealed class PercentageConverter : IValueConverter
    {
        // Methods
        public object Convert(object o, Type targetType, object parameter, CultureInfo culture)
        {
            if ((o == null) || o.Equals(MixedProperty.Mixed))
            {
                return o;
            }
            double num = (double)o;
            return (num * 100.0);
        }

        public object ConvertBack(object o, Type targetType, object parameter, CultureInfo culture)
        {
            double num = (double)o;
            return (num / 100.0);
        }
    }
}
