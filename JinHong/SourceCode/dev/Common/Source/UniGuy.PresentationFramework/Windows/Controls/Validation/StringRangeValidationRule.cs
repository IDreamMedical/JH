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

namespace UniGuy.Controls.ValidationRules
{
    /// <summary>
    /// 字符串长度验证规则
    /// </summary>
    public class StringRangeValidationRule: ValidationRule
    {
        /// <summary>
        /// 字符串最小长度
        /// </summary>
        private int minLength = -1;
        /// <summary>
        /// 字符串最大长度
        /// </summary>
        private int maxLength = int.MaxValue;
        /// <summary>
        /// 验证失败时的错误信息
        /// </summary>
        private string errorMessage = "String length should be between range.";

        /// <summary>
        /// 获得或者设置字符串最小长度
        /// </summary>
        public int MinLength
        {
            get { return minLength; }
            set { minLength = value; }
        }

        /// <summary>
        /// 获得或者设置字符串最大长度
        /// </summary>
        public int MaxLength
        {
            get { return maxLength; }
            set { maxLength = value; }
        }

        /// <summary>
        /// 获得或者设置验证失败时的错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string inputString = (value ?? string.Empty).ToString();
            if (inputString.Length < this.minLength || inputString.Length > this.maxLength)
                return new ValidationResult(false, this.errorMessage);
            return new ValidationResult(true, null);
        }
    }
}
