using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 如果没有实现IEnumerable，则放入List中的转换器
    /// </summary>
    [ValueConversion(typeof(object), typeof(IEnumerable))]
    public class SingleToArrayConverter : MarkupExtension, IValueConverter
    {
        public SingleToArrayConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
                return null;
            bool? autoCheckIsEnumerable = parameter as bool?;
            if (autoCheckIsEnumerable==true && (value is IEnumerable))
                return value;
            Array arr = Array.CreateInstance(value.GetType(), 1);
            arr.SetValue(value, 0);
            return arr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
