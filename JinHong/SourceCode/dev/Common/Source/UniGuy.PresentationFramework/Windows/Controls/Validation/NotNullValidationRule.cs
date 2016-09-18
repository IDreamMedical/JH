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
    public class NotNullValidationRule: ValidationRule
    {
        private string errorMessage="Can not be null.";

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value == null)
                return new ValidationResult(false, this.errorMessage);
            return new ValidationResult(true, null);
        }
    }
}
