using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Diagnostics;

namespace UniGuy.Windows.Data
{
    /// <summary>
    /// abc.xml -> abc;
    /// abc.def.xml -> abc.def
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public class TrimExtensionConverter : MarkupExtension, IValueConverter
    {
        #region IValueConverter
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            if (str == null)
                return null;

            int index = str.LastIndexOf('.');
            if (index > 0)
                str = str.Substring(0, index);
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;

            string extension = (string)parameter;
            Debug.Assert(extension.Length > 1 && extension[0] == '.');

            return str + extension;
        }
        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
