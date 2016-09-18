using System;
using System.Globalization;
using System.Windows.Data;
using System.Collections.Generic;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    public sealed class ComposingConverter : MarkupExtension, IValueConverter
    {
        // Fields
        private List<IValueConverter> converters = new List<IValueConverter>();

        // Methods
        public object Convert(object o, Type targetType, object parameter, CultureInfo culture)
        {
            for (int i = 0; i < this.converters.Count; i++)
            {
                o = this.converters[i].Convert(o, targetType, parameter, culture);
            }
            return o;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            for (int i = this.converters.Count - 1; i >= 0; i--)
            {
                value = this.converters[i].ConvertBack(value, targetType, parameter, culture);
            }
            return value;
        }

        // Properties
        public List<IValueConverter> Converters
        {
            get
            {
                return this.converters;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
