using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 布尔类型取反的转换器
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolToOppositeBoolConverter : MarkupExtensionConverter
    {
        #region IValueConverter Members
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value is bool?!(bool)value:Binding.DoNothing;
        }
        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
        #endregion
    }
}
