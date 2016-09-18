using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 有时候什么也不做的转换器
    /// </summary>
    /// <remarks>
    /// 举个例子, 有一个界面, 上面有一个ComboBox和一个文本框, ComboBox中可以选则科室, Text绑定ComboBox的SelectedValue填充该科室默认用户;
    /// 但是需要允许Text手工填写; 现在的问题是如果绑定模式采用TwoWay, 则反向绑定可能出错, 如果采用OneWay, 则手工填写后绑定丢失;
    /// 可行的办法是采用TwoWay, 因为必须保证绑定不丢失; 然后重写反向绑定, 让其DoNothing.
    /// 于是DoNothingCheater就诞生了.
    /// </remarks>
    [ValueConversion(typeof(object), typeof(object))]
    public class DoNothingCheater : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// 包含的转换器
        /// </summary>
        private IValueConverter converter;
        /// <summary>
        /// DoNothing模式
        /// </summary>
        private bool convertDoNothing;

        /// <summary>
        /// 获得或者设置包含的转换器
        /// </summary>
        public IValueConverter Converter
        {
            get { return converter; }
            set { converter = value; }
        }

        /// <summary>
        /// 获得或者设置DoNothing(默认为false, 也就是Convert正常, ConvertBack DoNothing; 如果为true, 则反之)
        /// </summary>
        public bool ConvertDoNothing
        {
            get { return convertDoNothing; }
            set { convertDoNothing = value; }
        }

        public DoNothingCheater() { }
        public DoNothingCheater(IValueConverter converter) : this() { Converter = converter; }
        public DoNothingCheater(bool convertDoNothing) : this() { ConvertDoNothing = convertDoNothing; }
        public DoNothingCheater(IValueConverter converter, bool convertDoNothing) : this(converter) { ConvertDoNothing = convertDoNothing; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (convertDoNothing)
                return Binding.DoNothing;

            return Converter==null?value:Converter.Convert(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (convertDoNothing)
                return Converter==null?value:Converter.ConvertBack(value, targetType, parameter, culture);

            return Binding.DoNothing;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
