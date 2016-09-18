using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Globalization;

namespace UniGuy.Controls.ValidationRules
{
    /// <summary>
    /// 与复合验证规则
    /// Aggregates a set of <see cref="ValidationRule"/> instances, performing validation by allowing each ValidationRule to perform its own.
    /// </summary>
    /// <remarks>
    /// Validation fails if any of the composed ValidationRule fails.
    /// </remarks>
    public class AndCompositeValidationRule:ValidationRule
    {
        /// <summary>
        /// 验证规则集合
        /// </summary>
        private IEnumerable<ValidationRule> validationRules;

        /// <summary>
        /// 获得或者设置验证规则集合
        /// </summary>
        public IEnumerable<ValidationRule> ValidationRules
        {
            get { return validationRules; }
            set { validationRules = value; }
        }

        public AndCompositeValidationRule():base() { }
        public AndCompositeValidationRule(params ValidationRule[] validationRules)
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
                        continue;
                    return validationResult;
                }
            }
            return new ValidationResult(true, null);
        }
    }
}
