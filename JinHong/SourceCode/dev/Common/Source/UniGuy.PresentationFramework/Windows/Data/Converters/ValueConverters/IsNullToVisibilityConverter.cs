using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 不为空则可见
    /// </summary>
	[ValueConversion(typeof(object), typeof(Visibility))]
    public class IsNullToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
            return value == null ? Visibility.Hidden : Visibility.Visible;
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
            throw new NotSupportedException();
		}
	}
}