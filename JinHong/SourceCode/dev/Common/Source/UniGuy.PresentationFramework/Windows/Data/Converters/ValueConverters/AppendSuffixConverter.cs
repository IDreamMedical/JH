using System;
using System.Globalization;
using System.Windows.Data;

namespace UniGuy.Controls.Converters
{
    public class AppendSuffixConverter : IValueConverter
    {
        // Fields
        private string suffix;

        // Methods
        public object Convert(object o, Type targetType, object parameter, CultureInfo culture)
        {
            return (o.ToString() + this.Suffix);
        }

        public object ConvertBack(object o, Type targetType, object value, CultureInfo culture)
        {
            return null;
        }

        // Properties
        public string Suffix
        {
            get
            {
                return this.suffix;
            }
            set
            {
                this.suffix = value;
            }
        }
    }
}
