using System;
using System.Globalization;
using System.Windows.Data;
using UniGuy.Core;

namespace UniGuy.Controls.Converters
{
    public class DataAssemblyCombineConverter:IMultiValueConverter
    {
        // Methods
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DataAssembly da = null;
            foreach (object obj2 in values)
            {
                DataAssembly item = obj2 as DataAssembly;
                if (item != null)
                    (da ?? (da = new DataAssembly())).Combine(item);
            }
            return da;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
