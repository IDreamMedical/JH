using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace UniGuy.Controls.Markup
{
    [MarkupExtensionReturnType(typeof(object[]))]
    public class EnumValuesExtension: MarkupExtension
    {
        [ConstructorArgument("enumType")]
        public Type EnumType
        {
            get;
            set;
        }

        public EnumValuesExtension() { }

        public EnumValuesExtension(Type enumType)
        {
            this.EnumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (this.EnumType == null)
                throw new ArgumentException("The enum type is not set");
            return Enum.GetValues(this.EnumType);
        } 
    }
}

//  <ComboBox ItemsSource="{local:EnumValues local:EmployeeType}"/> 
