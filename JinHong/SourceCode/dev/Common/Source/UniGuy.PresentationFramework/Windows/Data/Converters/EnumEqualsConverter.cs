using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 如果等于指定枚举的某个值返回True,否则返回False
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class EnumEqualsConverter:MarkupExtension, IValueConverter
    {
        #region Fields
        /// <summary>
        /// Enum Type
        /// </summary>
        private Type enumType;
        #endregion  // Fields

        #region Properties
        /// <summary>
        /// 获得或者设置Enum Type
        /// </summary>
        public Type EnumType
        {
            get
            {
                return this.enumType;
            }
            set
            {
                this.enumType = value;
            }
        }
        #endregion  // Properties

        #region Ctor
        public EnumEqualsConverter() { }
        public EnumEqualsConverter(Type enumType)
        { this.enumType = enumType; }
        #endregion

        #region IValueConverter
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter.GetType() == enumType)
                return value==parameter;
            if (parameter is string)
                return Enum.GetName(enumType, value) == (string)parameter;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
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
