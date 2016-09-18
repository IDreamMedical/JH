using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Diagnostics;

namespace UniGuy.Windows.Data
{
    /// <summary>
    /// 用于预览绑定，一个界面属性绑定到两个值，一个当前值和一个预览值，如果预览值
    /// </summary>
    /// <example>
    /// <TextBlock>
    ///     <TextBlock.Text>
    ///         <MultiBinding Converter="{pw:PreviewableMultiConverter}">
    ///             <Binding Path="IsInPreviewMode"/>
    ///             <Binding Path="ActualValue"/>
    ///             <Binding Path="PreviewValue"/>
    ///         </MultiBinding>
    ///     </TextBlock.Text>
    /// </TextBlock>
    /// </example>
    public class PreviewableMultiConverter : MarkupExtension, IMultiValueConverter
    {
        //  TODO
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length != 3)
                throw new Exception("Must have 3 bindings.");
            bool isInPreviewMode = (bool)values[0];
            return isInPreviewMode ? values[2] : values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
