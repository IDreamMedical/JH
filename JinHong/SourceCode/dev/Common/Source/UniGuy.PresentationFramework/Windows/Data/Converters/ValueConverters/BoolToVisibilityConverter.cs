using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    public sealed class MixedProperty
    {
        // Fields
        public static readonly object Mixed = new MixedProperty();

        // Methods
        private MixedProperty()
        {
        }
    }

    public sealed class BoolToVisibilityConverter : MarkupExtension, IValueConverter
    {
        // Methods
        public object Convert(object o, Type targetType, object parameter, CultureInfo culture)
        {
            if (o == MixedProperty.Mixed)
            {
                return Visibility.Visible;
            }
            return (((bool)o) ? Visibility.Visible : Visibility.Collapsed);
        }

        public object ConvertBack(object o, Type targetType, object parameter, CultureInfo culture)
        {
            return (((Visibility)o) == Visibility.Visible);
        }

        #region Overrides
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            //  爽快
            return this;
        }
        #endregion
    }
}
