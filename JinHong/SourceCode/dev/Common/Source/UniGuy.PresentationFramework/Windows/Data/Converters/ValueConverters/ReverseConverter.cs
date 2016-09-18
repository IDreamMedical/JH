using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    public class ReverseConverter:MarkupExtension, IValueConverter
    {
        #region Fields
        /// <summary>
        /// 原转换器
        /// </summary>
        private IValueConverter converter;
        #endregion  // Fields

        #region Properties
        /// <summary>
        /// 获得或者设置原转换器
        /// </summary>
        public IValueConverter Converter
        {
            get{return converter;}
            set{converter = value;}
        }
        #endregion  // Properties

        #region Ctor
        public ReverseConverter() { }
        public ReverseConverter(IValueConverter converter)
        {
            this.converter = converter;
        }
        #endregion

        #region IValueConverter
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (converter == null)
                throw new Exception("Converter is null.");
            return Converter.ConvertBack(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (converter == null)
                throw new Exception("Converter is null.");
            return Converter.Convert(value, targetType, parameter, culture);
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
