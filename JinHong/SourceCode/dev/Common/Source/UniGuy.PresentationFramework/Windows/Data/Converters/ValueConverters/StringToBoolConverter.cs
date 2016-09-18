using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    public class StringToBoolConverter:MarkupExtension, IValueConverter
    {
        #region Fields
        /// <summary>
        /// 要转换为True的字符串
        /// </summary>
        private string trueString = bool.TrueString;
        /// <summary>
        /// 要转换为False的字符串
        /// </summary>
        private string falseString = bool.FalseString;
        /// <summary>
        /// 都不匹配的取值
        /// </summary>
        private bool elseValue = false;
        #endregion  // Fields

        #region Properties
        /// <summary>
        /// 获得或者设置要转换为True的字符串
        /// </summary>
        public string TrueString
        {
            get{return trueString;}
            set{trueString = value;}
        }
        /// <summary>
        /// 获得或者设置要转换为False的字符串
        /// </summary>
        public string FalseString
        {
            get{return falseString;}
            set{falseString = value;}
        }
        /// <summary>
        /// 获得或者设置都不匹配的取值
        /// </summary>
        public bool ElseValue
        {
            get{return elseValue;}
            set{elseValue = value;}
        }
        #endregion  // Properties

        #region Ctor
        public StringToBoolConverter() { }
        public StringToBoolConverter(string trueString, string falseString)
        {
            this.trueString = trueString;
            this.falseString = falseString;
        }
        public StringToBoolConverter(string trueString, string falseString, bool elseValue)
        :this(trueString, falseString)
        {
            this.elseValue = elseValue;
        }
        #endregion

        #region IValueConverter
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value==null || value is string)
                return (string)value==trueString?true:(string)value==falseString?false:elseValue;
            return elseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
                return (bool)value ? trueString : falseString;
            return null;
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
