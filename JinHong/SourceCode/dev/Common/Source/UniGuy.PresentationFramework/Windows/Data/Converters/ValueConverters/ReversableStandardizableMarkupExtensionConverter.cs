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
    /// 继承自MarkupExtensionConverter且同时支持反向转换和向目标标准化转换的转换器
    /// </summary>
    public abstract class ReversableStandardizableMarkupExtensionConverter : ReversableMarkupExtensionConverter
    {
        /// <summary>
        /// 是否使用标准化
        /// </summary>
        public bool IsStandardize
        {
            get;
            set;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsStandardize ? value : IsReverse ? ConvertOverride(value, targetType, parameter, culture) : ConvertBackOverride(value, targetType, parameter, culture);
        }
    }
}
