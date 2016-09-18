using System;
using System.Globalization;
using System.Windows.Data;
using UniGuy.Core;

namespace UniGuy.Controls.Converters
{
    public class CodeAssemblyCombineConverter:IMultiValueConverter
    {
        // Methods
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            CodeAssembly ca = null;
            foreach (object obj2 in values)
            {
                CodeAssembly item = obj2 as CodeAssembly;
                if (item != null)
                    (ca ?? (ca = new CodeAssembly())).Combine(item);
            }
            return ca;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
