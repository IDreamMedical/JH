using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Data;
using System.Globalization;

namespace UniGuy.Controls.Converters
{
    public class EnumerableMultiValueConverter:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            List<object> objs = new List<object>();
            foreach (object value in values)
            {
                if (value is IEnumerable)
                {
                    foreach (object obj in (IEnumerable)value)
                        objs.Add(obj);
                }
                else if(value!=null)
                    objs.Add(value);
            }
            return objs;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot perform reverse-conversion");
        }
    }
}
