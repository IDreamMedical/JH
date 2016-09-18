using System;
using System.Text;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

//  WJ
//  Created 20110610
//  LastModified    20110610
namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 一个支持扩展标记返回自身的转换器抽象基类
    /// </summary>
    public abstract class MarkupExtensionConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
