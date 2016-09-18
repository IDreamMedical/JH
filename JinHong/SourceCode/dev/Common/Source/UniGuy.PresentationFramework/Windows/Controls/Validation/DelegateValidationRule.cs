using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace UniGuy.Windows.Controls
{
    public class DelegateValidationRule : ValidationRule
    {
        public Func<object, object, CultureInfo, ValidationResult> validationFunc;

        public Func<object,object, CultureInfo, ValidationResult> ValidationFunc
        {
            private get { return validationFunc; }
            set { validationFunc = value; }
        }

        // 放在委托里进行的验证可能需要利用其它上下文数据， 比如该value所在的数据环境,所属DataModel等...
        public object validationContext;
        public object ValidationContext
        {
            get { return validationContext; }
            set { validationContext = value; }
        }

        public DelegateValidationRule()
            : base()
        {
        }

        public DelegateValidationRule(Func<object, object, CultureInfo, ValidationResult> validationFunc)
            : this()
        {
            if (validationFunc == null)
                throw new ArgumentNullException("validationFunc", "cannot be null.");
            this.ValidationFunc = validationFunc;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (this.ValidationFunc != null)
                return this.ValidationFunc(value, this.ValidationContext,  cultureInfo);

            return ValidationResult.ValidResult;
        }
    }
}
