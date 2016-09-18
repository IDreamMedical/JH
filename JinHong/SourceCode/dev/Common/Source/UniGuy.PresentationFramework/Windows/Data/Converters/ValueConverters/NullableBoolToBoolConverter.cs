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
    /// 可为空的布尔类型到布尔类型的转换器
    /// </summary>
    [ValueConversion(typeof(bool?), typeof(bool))]
    public class NullableBoolToBoolConverter:MarkupExtension, IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (targetType != typeof(bool))
            //    throw new InvalidOperationException("The target must be a boolean");
            bool? b = (bool?)value;
            return b.HasValue ? b.Value : default(bool);
            
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (targetType != typeof(bool?))
            //    throw new InvalidOperationException("The source must be a nullable boolean");
            return (bool?)value;
        }
        #endregion

        #region
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        #endregion
    }
}
