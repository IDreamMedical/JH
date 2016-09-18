using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace UniGuy.Controls.Converters
{


    /// <summary>
    /// 布尔类型到Int的布尔类型的转换器
    /// </summary>
    [ValueConversion(typeof(int), typeof(bool))]
    public class BoolToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int re = (int)value;
            if (re == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int strValue = System.Convert.ToInt32(value);

            if (strValue == 0)
            {
                return true;
            }
            if (strValue == 1)
            {
                return false;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
