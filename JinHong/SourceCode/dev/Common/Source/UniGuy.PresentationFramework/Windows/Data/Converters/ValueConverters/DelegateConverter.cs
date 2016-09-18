using System;
using System.Globalization;
using System.Windows.Data;
using UniGuy.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// XAML:
    /// <TextBlock Text="{Binding xxx, Converter={x:Static local:myViewModel.myConverter}}" />
    /// 
    /// ViewModel:
    /// class myViewModel
    /// {
    ///     public static DelegateConverter myConverter = new DelegateConverter
    ///     { 
    ///         ConvertDelegate = (value, t, p, c) =>
    ///         {
    ///             return do_something_with_value;
    ///         }
    ///     }
    /// }
    /// </summary>
    public class DelegateConverter: ProvideSelfMarkupExtension, IValueConverter
    {
        #region Fields
        public Func<object, Type, object, CultureInfo, object> ConvertDelegate { get; set; }
        public Func<object, Type, object, CultureInfo, object> ConvertBackDelegate { get; set; }
        #endregion

        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertDelegate(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBackDelegate(value, targetType, parameter, culture);
        }
        #endregion
    }
}
