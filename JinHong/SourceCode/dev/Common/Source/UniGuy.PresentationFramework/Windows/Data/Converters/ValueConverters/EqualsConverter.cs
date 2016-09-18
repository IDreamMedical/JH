using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    public class EqualsConverter : MarkupExtension, IValueConverter
    {
        // Fields
        private object defaultValue = false;
        private object matchValue = true;

        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (object.Equals(value, parameter))
            {
                return this.matchValue;
            }
            return this.defaultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }

        // Properties
        public object DefaultValue
        {
            get
            {
                return this.defaultValue;
            }
            set
            {
                this.defaultValue = value;
            }
        }

        public object MatchValue
        {
            get
            {
                return this.matchValue;
            }
            set
            {
                this.matchValue = value;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
