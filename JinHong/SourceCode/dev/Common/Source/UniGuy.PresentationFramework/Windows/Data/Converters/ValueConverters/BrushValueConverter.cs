using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace UniGuy.Controls.Converters
{
    [ValueConversion(typeof(object), typeof(object))]
    public class BrushValueConverter:MarkupExtension, IValueConverter
    {
        #region Static 
        private static readonly BrushConverter _converter = new BrushConverter();

        private static object Convert(object value, Type targetType)
        {
            if (value == null)
                return Binding.DoNothing;

            var sourceType = value.GetType();

            if (typeof(Brush).IsAssignableFrom(sourceType) && _converter.CanConvertTo(null, targetType))
            {
                return _converter.ConvertTo(null, CultureInfo.CurrentUICulture, value, targetType);
            }

            if (typeof(Brush).IsAssignableFrom(targetType))
            {
                if (_converter.CanConvertFrom(null, sourceType))
                {
                    return _converter.ConvertFrom(null, CultureInfo.CurrentUICulture, value);
                }
                if (sourceType == typeof(Color))
                {
                    return new SolidColorBrush((Color)value);
                }
            }

            return Binding.DoNothing;
        }
        #endregion

        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType);
        }
        #endregion

        #region MarkupExtension
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        #endregion
    }
}
