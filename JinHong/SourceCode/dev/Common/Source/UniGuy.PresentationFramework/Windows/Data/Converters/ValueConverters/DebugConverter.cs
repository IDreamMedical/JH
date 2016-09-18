using System;
using System.Globalization;
using System.Windows.Data;
using System.Diagnostics;
using UniGuy.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    public class DebugConverter : ProvideSelfMarkupExtension, IValueConverter
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
    }
}
