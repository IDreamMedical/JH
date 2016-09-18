using System;
using System.Globalization;
using System.Windows.Data;

namespace UniGuy.Controls.Converters
{
    public sealed class LessThanEqualsConverter :  IValueConverter
    {
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (((value != null) && (parameter != null)) && (((double)value) <= double.Parse((string)parameter, CultureInfo.InvariantCulture)))
                {
                    return true;
                }
            }
            catch (FormatException)
            {
            }
            catch (OverflowException)
            {
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
