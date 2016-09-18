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
    /// 如果为空则什么也不做的转换器包装
    /// </summary>
    /// <remarks>
    /// 举个例子, 现在制作一个自定义列表控件, 其中的某个控件的ItemTemplate TemplateBinding该控件的某个属性;
    /// 我们希望如果该属性为空则改模板中的ItemsControl使用自己的默认模板, 而不要使用绑定的null.
    /// </remarks>
    [ValueConversion(typeof(object), typeof(object))]
    public class DoNothingIfNullConverterWrapper : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// 包含的转换器
        /// </summary>
        private IValueConverter converter;
        /// <summary>
        /// DoNothingIfNullMode模式
        /// </summary>
        private DoNothingIfNullMode mode = DoNothingIfNullMode.Convert;

        /// <summary>
        /// 获得或者设置包含的转换器
        /// </summary>
        public IValueConverter Converter
        {
            get { return converter; }
            set { converter = value; }
        }

        /// <summary>
        /// 获得或者设置DoNothingIfNullMode模式
        /// </summary>
        public DoNothingIfNullMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public DoNothingIfNullConverterWrapper() { }
        public DoNothingIfNullConverterWrapper(IValueConverter converter) : this() { Converter = converter; }
        public DoNothingIfNullConverterWrapper(IValueConverter converter, DoNothingIfNullMode mode) : this(converter) { Mode = mode; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object ret = Converter==null?value:Converter.Convert(value, targetType, parameter, culture);
            return (ret == null && (mode & DoNothingIfNullMode.Convert) != 0) ? Binding.DoNothing : ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object ret = Converter == null ? value : Converter.ConvertBack(value, targetType, parameter, culture);
            return (ret == null && (mode & DoNothingIfNullMode.ConvertBack) != 0) ? Binding.DoNothing : ret;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        [Flags]
        public enum DoNothingIfNullMode
        {
            Convert=1,
            ConvertBack=2,
            Both=Convert|ConvertBack
        }
    }
}
