using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Globalization;
using System.Diagnostics;

namespace UniGuy.Controls.ValidationRules
{
    /// <summary>
    /// 非符合验证规则(包含一个验证器)
    /// </summary>
    public class NotValidationRule:ValidationRule
    {
        /// <summary>
        /// 验证规则
        /// </summary>
        private ValidationRule validationRule;
        /// <summary>
        /// 验证失败时的错误信息
        /// </summary>
        private string errorMessage = "Validation check failed.";

        /// <summary>
        /// 获得或者设置验证规则
        /// </summary>
        public ValidationRule ValidationRule
        {
            get { return validationRule; }
            set { validationRule = value; }
        }
        /// <summary>
        /// 获得或者设置验证失败时的错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        private NotValidationRule() : base() { }
        public NotValidationRule(ValidationRule validationRule)
            : this()
        {
            this.ValidationRule = validationRule;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Debug.Assert(ValidationRule != null);
            ValidationResult validationResult = validationRule.Validate(value, cultureInfo);
            return validationResult.IsValid?new ValidationResult(false, this.errorMessage)
                :new ValidationResult(true, null);
        }
    }
}
