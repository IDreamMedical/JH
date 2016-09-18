using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Data;
using System.Globalization;
using UniGuy.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    //  TODO
    public class FirstMultiValueConverter:ProvideSelfMarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (values!=null && values.Length>0)?values[0]:null;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return value as object[];
        }
    }
}
