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
    /// 继承自MarkupExtensionConverter且支持反向转换的转换器
    /// </summary>
    public abstract class ReversableMarkupExtensionConverter : MarkupExtensionConverter
    {
        /// <summary>
        /// 是否使用反向转换
        /// </summary>
        public bool IsReverse
        {
            get;
            set;
        }

        public abstract object ConvertOverride(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBackOverride(object value, Type targetType, object parameter, CultureInfo culture);

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsReverse ? ConvertBackOverride(value, targetType, parameter, culture) : ConvertOverride(value, targetType, parameter, culture);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsReverse ? ConvertOverride(value, targetType, parameter, culture) : ConvertBackOverride(value, targetType, parameter, culture);
        }
    }
}
