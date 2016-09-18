// Copyright (C) Michael Agroskin 2010
using System;
using System.Globalization;
using System.Windows.Data;
using UniGuy.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// XAML:
    /// <TextBlock>
    ///     <TextBlock.Text>
    ///         <MultiBinding Converter="{x:Static local:myViewModel.myConverter}">
    ///             <Binding xxxxx/>
    ///             <Binding xxxxx/>
    ///             <Binding xxxxx/>
    ///         </MultiBinding>
    ///     </TextBlock.Text>
    ///  </TextBlock>
    /// 
    /// ViewModel:
    /// class myViewModel
    /// {
    ///     public static DelegateMultiConverter myConverter = new DelegateMultiConverter
    ///     { 
    ///         ConvertDelegate = (values, t, p, c) =>
    ///         {
    ///             return do_something_with_values;
    ///         }
    ///     }
    /// }
    /// </summary>
    public class DelegateMultiConverter : ProvideSelfMarkupExtension, IMultiValueConverter
    {
        public Func<object[], Type, object, CultureInfo, object> ConvertDelegate { get; set; }
        public Func<object, Type[], object, CultureInfo, object[]> ConvertBackDelegate { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertDelegate(values, targetType, parameter, culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return ConvertBackDelegate(value, targetTypes, parameter, culture);
        }
    }
}
