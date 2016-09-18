using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    [ValueConversion(typeof(double), typeof(double))]
    public class AddMultiConverter:MarkupExtension, IMultiValueConverter
    {
        #region IMultiValueConverter
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double result = 0.00;

            try
            {
                foreach (object value in values)
                {
                    if (value is string)
                    {
                        double dTemp0 = default(double);
                        if (double.TryParse((string)value, out dTemp0))
                            result += dTemp0;
                    }
                    else if (value is double)
                    {
                        result += (double)value;
                    }
                    else if (value is int)
                    {
                        result += (int)value;
                    }
                }

                return (targetType == typeof(string)) ? (object)result.ToString() : result;
            }
            catch (Exception)
            {
                return Binding.DoNothing;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            //  return (from type in targetTypes select Binding.DoNothing).ToArray();

            object[] results = new object[targetTypes.Length];
            for (int i = 0; i < targetTypes.Length; i++)
                results[i] = Binding.DoNothing;
            return results;
        }
        #endregion

        #region Overrides
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        #endregion
    }
}
