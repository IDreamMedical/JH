using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Globalization;

namespace UniGuy.Controls.ValidationRules
{
    /// <summary>
    /// 或符合验证规则
    /// Aggregates a set of <see cref="ValidationRule"/> instances, performing validation by allowing each ValidationRule to perform its own.
    /// </summary>
    /// <remarks>
    /// Validation results are logged only if all composed ValidationRule log results.
    /// </remarks>
    public class OrCompositeValidationRule:ValidationRule
    {
        /// <summary>
        /// 验证规则集合
        /// </summary>
        private IEnumerable<ValidationRule> validationRules;
        /// <summary>
        /// 验证失败时的错误信息
        /// </summary>
        private string errorMessage = "All ValidationRules failed.";

        /// <summary>
        /// 获得或者设置验证规则集合
        /// </summary>
        public IEnumerable<ValidationRule> ValidationRules
        {
            get { return validationRules; }
            set { validationRules = value; }
        }
        /// <summary>
        /// 获得或者设置验证失败时的错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public OrCompositeValidationRule() : base() { }
        public OrCompositeValidationRule(params ValidationRule[] validationRules)
            : this()
        {
            this.validationRules = validationRules;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (validationRules != null)
            {
                foreach (ValidationRule validationRule in validationRules)
                {
                    ValidationResult validationResult = validationRule.Validate(value, cultureInfo);
                    if (validationResult.IsValid)
                        return validationResult;
                    else
                        continue;
                }
                return new ValidationResult(false, this.errorMessage);
            }
            return new ValidationResult(true, null);
        }
    }
}
