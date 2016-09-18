using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    ///
    /// </summary>
    public class EnumBooleanConverter :MarkupExtension, IValueConverter
    {
        #region Ctor
        public EnumBooleanConverter() { }
        #endregion

        #region IValueConverter
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string; 
            if (parameterString == null) 
                return DependencyProperty.UnsetValue; 
            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue; 
            object parameterValue = Enum.Parse(value.GetType(), parameterString);
            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                string parameterString = parameter as string;
                if (parameterString == null)
                    return DependencyProperty.UnsetValue;
                return Enum.Parse(targetType, parameterString);
            }
            return Binding.DoNothing;
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
