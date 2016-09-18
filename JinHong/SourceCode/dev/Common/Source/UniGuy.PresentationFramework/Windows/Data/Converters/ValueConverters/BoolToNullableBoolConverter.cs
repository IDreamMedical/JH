using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 布尔类型到可为空的布尔类型的转换器
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool?))]
    public class BoolToNullableBoolConverter:IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool?))
                throw new InvalidOperationException("The target must be a nullable boolean");

            return (bool?)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The source must be a boolean");

            bool? b = (bool?)value;

            return b.HasValue ? b.Value : default(bool);
        }
        #endregion
    }
}
