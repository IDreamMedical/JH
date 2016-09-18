using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 如果没有实现IEnumerable，则放入List中的转换器
    /// </summary>
    [ValueConversion(typeof(object), typeof(IEnumerable))]
    public class ObjectsToArrayConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return value as object[];
        }
    }
}
