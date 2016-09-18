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
    /// 继承自MarkupExtensionConverter且支持反向转换直接返回值而实现向目标标准化的转换器
    /// </summary>
    public abstract class StandardizableMarkupExtensionConverter : MarkupExtensionConverter
    {
        /// <summary>
        /// 是否使用标准化
        /// </summary>
        public bool IsStandardize
        {
            get;
            set;
        }

        public abstract object ConvertBackOverride(object value, Type targetType, object parameter, CultureInfo culture);

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsStandardize ? value : ConvertBackOverride(value, targetType, parameter, culture);
        }
    }
}
