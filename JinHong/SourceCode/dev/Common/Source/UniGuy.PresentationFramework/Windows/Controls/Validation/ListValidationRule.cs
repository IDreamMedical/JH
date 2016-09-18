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
    public class ListValidationRule: ValidationRule
    {
        private List<object> values;
        private string errorMessage = "Value should be in list.";

        public List<object> Values
        {
            get { return values; }
            set { values = value; }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(values!=null && values.Contains(value))
                return new ValidationResult(true, null);
            return new ValidationResult(false, this.errorMessage);
        }
    }
}
