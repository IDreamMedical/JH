using System;
using System.Globalization;
using System.Windows.Data;

namespace UniGuy.Controls.Converters
{
    public class MultiplyConverter : IValueConverter
    {
        // Fields
        private double multiplyValue;

        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double num;
            if (value is int)
            {
                num = (int)value;
            }
            else if (value is uint)
            {
                num = (uint)value;
            }
            else
            {
                num = (double)value;
            }
            return (num * this.multiplyValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double num;
            if (value is int)
            {
                num = (int)value;
            }
            else
            {
                num = (double)value;
            }
            return (num / this.multiplyValue);
        }

        // Properties
        public double MultiplyValue
        {
            get
            {
                return this.multiplyValue;
            }
            set
            {
                this.multiplyValue = value;
            }
        }
    }
}
