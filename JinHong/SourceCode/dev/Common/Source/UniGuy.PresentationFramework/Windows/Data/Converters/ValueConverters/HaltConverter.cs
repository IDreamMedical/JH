using System;
using System.Globalization;
using System.Windows.Data;
using System.Diagnostics;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    public class HaltConverter:MarkupExtension, IValueConverter
    {
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
