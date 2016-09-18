using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 布尔类型取反的转换器
    /// </summary>
    [ValueConversion(typeof(bool?), typeof(bool?))]
    public class NullableBoolToOppositeNullableBoolConverter : MarkupExtension, IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? b = value as bool?;

            return b.HasValue ? !b : null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? b = value as bool?;

            return b.HasValue ? !b : null;
        }
        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
