using System;
using System.Globalization;
using System.Windows.Data;

namespace UniGuy.Controls.Converters
{
    public class EqualsMultipleConverter : IMultiValueConverter
    {
        // Methods
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            for (int i = 1; i < values.Length; i++)
            {
                if (!object.Equals(values[i - 1], values[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
