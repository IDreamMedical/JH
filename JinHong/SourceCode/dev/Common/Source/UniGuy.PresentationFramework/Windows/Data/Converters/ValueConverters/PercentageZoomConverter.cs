using System;
using System.Globalization;
using System.Windows.Data;

namespace UniGuy.Controls.Converters
{
    public class PercentageZoomConverter : IValueConverter
    {
        // Methods
        public object Convert(object o, Type targetType, object parameter, CultureInfo culture)
        {
            if (o is double)
            {
                double num = (double)o;
                return (100.0 * num);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object o, Type targetType, object parameter, CultureInfo culture)
        {
            if (o is double)
            {
                double num = (double)o;
                return (num / 100.0);
            }
            return Binding.DoNothing;
        }
    }
}
