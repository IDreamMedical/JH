using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace UniGuy.Controls.ValidationRules
{
    /// <summary>
    /// A <see cref="System.Windows.Controls.ValidationRule"/>-derived class which 
    /// supports the use of GB data assembly string format for validation.
    /// </summary>
    public class FormatValidationRule:ValidationRule
    {
        #region Fields
        /// <summary>
        /// 验证错误时的提示消息
        /// </summary>
        private string errorMessage;
        /// <summary>
        /// 标准数据集中的格式字符串
        /// </summary>
        private string format;
        #endregion

        #region Properties
        /// <summary>
        /// 获得或者设置验证错误时的提示消息
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }
        /// <summary>
        /// 获得或者设置标准数据集中的格式字符串
        /// </summary>
        public string Format
        {
            get { return format; }
            set { format = value; }
        }
        #endregion

        #region Ctor
        public FormatValidationRule() { }
        public FormatValidationRule(string format)
            : this()
        {
            this.format = format;
        }
        public FormatValidationRule(string format, string errorMessage):this(format)
        {
            this.errorMessage = errorMessage;
        }
        #endregion

        #region Methods
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = ValidationResult.ValidResult;

            //  如果没有格式字符串, 则认为字符串合法
            if (!string.IsNullOrEmpty(this.format))
            {
                //  把要验证的对象转换为字符串
                string text = value as string ?? string.Empty;

                if (!AssemblyHelper.ValidateByFormat(format, text))
                    result = new ValidationResult(false, this.errorMessage);
            }
            return result;
        }
        #endregion
    }
}
