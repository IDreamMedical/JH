using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Reflection;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections;
using System.Windows.Media.Imaging;

namespace UniGuy.Windows.Data
{
    public class ValidationErrorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            ValidationError error = value as ValidationError;
            if (error == null)
                return null;

            if (error.Exception != null)
            {
                if (error.Exception.GetType() == typeof(TargetInvocationException) && error.Exception.InnerException != null)
                {
                    return error.Exception.InnerException.Message;
                }
                else
                {
                    return error.Exception.Message;
                }
            }
            else
                return error.ErrorContent;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class InverseBoolConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }

    public class ColorToSolidColorBrushConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Converts a Color to a SolidColorBrush.
        /// </summary>
        /// <param name="value">The Color produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted SolidColorBrush. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return new SolidColorBrush((Color)value);

            return value;
        }


        /// <summary>
        /// Converts a SolidColorBrush to a Color.
        /// </summary>
        /// <remarks>Currently not used in toolkit, but provided for developer use in their own projects</remarks>
        /// <param name="value">The SolidColorBrush that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return ((SolidColorBrush)value).Color;

            return value;
        }

        #endregion
    }



    public class CountToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((int)value > 0)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }



    

    public class MultiToRectConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double width = (double)values[0]+1;
            double height = (double)values[1];

            if (width < 0 || height < 0)
                return new Rect(0, 0, 0, 0);

            return new Rect(0, 0, width, height);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }


    public class WidthsToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double trackWidth = (double)values[0];
            double textWidth = (double)values[1];
            if (textWidth >= trackWidth - 8)
                return Visibility.Hidden;
            else
                return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class OffsetConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                double small = (double)values[0];
                double large = (double)values[1];
                double item = (double)values[2];

                // calculate offset into item
                double offset = (large - item) / 2;
                offset = small - offset;
                if (offset <= 0)
                    return 0.0;
                else if (offset >= item)
                    return 1.0;
                else
                    return offset / item;

            }
            catch
            {
                return 0.0;
            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }


    public class ValueToBackgroundConverter : IMultiValueConverter
    {
        public Brush NormalBrush { get; set; }
        public Brush WarningBrush { get; set; }
        public Brush ErrorBrush { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if ((double)values[0] >= (double)values[2])
            {
                return ErrorBrush;
            }
            else if ((double)values[0] >= (double)values[1])
            {
                return WarningBrush;
            }
            else
            {
                return NormalBrush;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }


    public class ScrollbarOnFarLeftConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return false;
            return ((double)value > 0);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class ScrollbarOnFarRightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values == null) return false;
            if (values[0] == null || values[1] == null || values[2] == null) return false;
            if (values[0].Equals(double.NaN) || values[1].Equals(double.NaN) || values[2].Equals(double.NaN)) return false;

            double dblHorizontalOffset = 0;
            double dblViewportWidth = 0;
            double dblExtentWidth = 0;

            double.TryParse(values[0].ToString(), out dblHorizontalOffset);
            double.TryParse(values[1].ToString(), out dblViewportWidth);
            double.TryParse(values[2].ToString(), out dblExtentWidth);

            bool fOnFarRight = Math.Round((dblHorizontalOffset + dblViewportWidth), 2) < Math.Round(dblExtentWidth, 2);
            return fOnFarRight;
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class AnyBoolToVisibility : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            foreach (object o in values)
            {
                if (o is bool)
                {
                    if ((bool)o)
                        return Visibility.Visible;
                }
            }

            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }


    public class BoolHiddenConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
                return (bool)value ? Visibility.Visible : Visibility.Hidden;

            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}