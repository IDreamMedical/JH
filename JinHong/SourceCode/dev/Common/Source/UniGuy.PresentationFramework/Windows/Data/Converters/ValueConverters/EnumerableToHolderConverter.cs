using System;
using System.Globalization;
using System.Windows.Data;
using System.Collections;

namespace UniGuy.Controls.Converters
{
    [ValueConversion(typeof(IEnumerable), typeof(EnumerableHolder))]
    public class EnumerableToHolderConverter:IValueConverter
    {

        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EnumerableHolder h = new EnumerableHolder();
            if (parameter != null)
                h.Name = parameter.ToString();
            if (value is IEnumerable)
             h.Items = value as IEnumerable ;
            
            return new object[1]{h};
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
