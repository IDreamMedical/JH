using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace UniGuy.Windows.Controls
{
    public class DoubleRangeValidationRule : ValidationRule
    {
        public double Maximum { get; set; }
        public double Minimum { get; set; }
        public bool IsNullable { get; set; }
        public string InvalidMessage { get; set; }

        public DoubleRangeValidationRule()
        {
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (IsNullable && ReferenceEquals(value, null))
                return ValidationResult.ValidResult;

            double? v;
            if (value is double)
            {
                v = (double)value;
            }
            else if (value is double?)
            {
                v = (double?)value;
            }
            else if (value is string)
            {
                double temp;
                double.TryParse((string)value, out temp);
                v = temp;
            }
            else
                v = null;

            if (v.HasValue)
            {
                if (v.Value >= Minimum && v.Value <= Maximum)
                    return ValidationResult.ValidResult;
                else
                    return new ValidationResult(false, InvalidMessage);
            }
            else
            {
                return new ValidationResult(false, "Invalid input");
            }
        }
    }
}
