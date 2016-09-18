using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Collections.Generic;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// CaseWhenConverter Format=red|红色|green|绿色|blue|蓝色|其他颜色
    /// 针对字符串，或者可转换为字符串的PrimitiveType或者Enum
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public class CaseWhenConverter:MarkupExtension, IValueConverter
    {
        #region Fields
        /// <summary>
        /// 格式字符串
        /// </summary>
        private string format = null;
        /// <summary>
        /// 如果全部不匹配,并且没有在CaseWhenString中给出默认值,最后的(其他颜色),如果为true则返回value自己
        /// </summary>
        private bool selfIfNoMatch = true;
        /// <summary>
        /// 如果全部不匹配,并且没有在CaseWhenString中给出默认值,最后的(其他颜色),并且selfIfNoMatch为False,则返回这个值
        /// </summary>
        private string elseString=null;
        #endregion  // Fields

        #region Properties
        public string Format
        {
            get { return this.format; }
            set { this.format = value; }
        }
        public bool SelfIfNoMatch
        {
            get{return this.selfIfNoMatch;}
            set{this.selfIfNoMatch = value;}
        }
        public string ElseString
        {
            get { return this.elseString; }
            set { this.elseString = value; }
        }
        #endregion  // Properties

        #region Ctor
        public CaseWhenConverter() { }
        public CaseWhenConverter(string format)
        { this.Format = format; }
        #endregion

        #region IValueConverter
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string str;
            if (value is string)
                str = value as string;
            else
            {
                Type type = value.GetType();
                if (type.IsEnum || type.IsPrimitive)
                    str = value.ToString();
                else
                    throw new ArgumentException("Must be string or enum of primitive type.");
            }

            System.Diagnostics.Debug.Assert(str != null);

            if (format != null)
            {
                List<string> tags = new List<string>(format.Split('|'));
                int index = tags.IndexOf(str);
                //  匹配
                if ((index & 1) == 0 && index < tags.Count - 1)
                    return tags[index + 1];
                //  不匹配但格式字符串奇数
                if ((tags.Count & 1) != 0)
                    return tags[tags.Count - 1];
            }
            //  不匹配,格式字符串偶数
            return selfIfNoMatch ? str : elseString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;

            if (str == null)
                return null;

            if (format != null)
            {
                List<string> tags = new List<string>(format.Split('|'));
                int index = tags.IndexOf(str);

                //  匹配
                if ((index & 1) != 0 && index > 0)
                    return tags[index - 1];
            }

            return str;
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
