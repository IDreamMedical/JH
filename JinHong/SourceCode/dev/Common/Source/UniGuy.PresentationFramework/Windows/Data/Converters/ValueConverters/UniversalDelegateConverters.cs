using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// Delegate converter的实例集合
    /// </summary>
    public class UniversalDelegateConverters
    {
        /// <summary>
        /// 如果相等则返回True,否则什么也不做,用于RadioButton,CheckBox等的绑定转换
        /// </summary>
        public static DelegateConverter IsEqualToBoolConverter = new DelegateConverter
        {
            ConvertDelegate = (value, t, p, c) =>
            {
                return StringComparer.InvariantCulture.Equals(value, p) ? true : false;
            },
            ConvertBackDelegate = (value, t, p, c) =>
            {
                return ((bool)value) ? p : Binding.DoNothing;
            }
        };

        /// <summary>
        /// 一个值如果和某个字符串相同则为true, 否则为false
        /// </summary>
        public static DelegateConverter IsEqualToTrueOrNullConverter = new DelegateConverter
        {
            ConvertDelegate = (value, t, p, c) =>
            {
                return StringComparer.InvariantCulture.Equals(value, p) ? true : false;
            },
            ConvertBackDelegate = (value, t, p, c) =>
            {
                if (value is bool)
                    return ((bool)value) ? p : null;
                else if(value is bool?)
                    return ((bool?)value).GetValueOrDefault() ? p : null;
                return Binding.DoNothing;
            }
        };

        //  TODO others
    }
}
