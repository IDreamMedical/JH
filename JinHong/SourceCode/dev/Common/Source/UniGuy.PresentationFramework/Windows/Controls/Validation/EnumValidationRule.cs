using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace UniGuy.Controls.ValidationRules
{
    /// <summary>
    /// 枚举验证规则
    /// </summary>
    public class EnumValidationRule:ValidationRule
    {
        /// <summary>
        /// 合法值列表
        /// </summary>
        private IEnumerable<object> validValues;
        /// <summary>
        /// 验证失败时的错误信息
        /// </summary>
        private string errorMessage = "Value is not in predefined valid values.";

        /// <summary>
        /// 获得或者设置合法值列表
        /// </summary>
        public IEnumerable<object> ValidValues
        {
            get { return validValues; }
            set { validValues = value; }
        }

        /// <summary>
        /// 获得或者设置验证失败时的错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (validValues != null)
            {
                foreach (object validValue in validValues)
                {
                    if (Comparer.Default.Compare(validValue, value) == 0)
                        return new ValidationResult(true, null);
                }
            }
            return new ValidationResult(false, errorMessage);
        }
    }
}
