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
    public class IntRangeValidationRule: ValidationRule
    {
        private int min = int.MinValue;
        private int max = int.MaxValue;
        private string errorMessage = "Integer should be between range.";

        public int MinValue
        {
            get { return min; }
            set { min = value; }
        }

        public int MaxValue
        {
            get { return max; }
            set { max = value; }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int i = (int)value;
            if (i<min || i>max)
                return new ValidationResult(false, this.errorMessage);
            return new ValidationResult(true, null);
        }
    }
}
