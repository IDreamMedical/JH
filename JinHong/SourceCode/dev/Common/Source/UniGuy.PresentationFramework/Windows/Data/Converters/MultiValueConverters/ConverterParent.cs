using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Data;
using System.Globalization;
using UniGuy.Windows.Markup;
using System.Windows.Markup;

//  201109200936
namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 看起来是一个IMultiValueConverter, 其实内容是一个IValueConverter
    /// 把第一项使用内部转换器转换后返回
    /// 使用场合:
    /// 1. 在MultiValueConverterGroup中, 最后返回的仍然是object[], 很多时候我们只要取第一项, 并且有可能对这个第一项进一步使用IValueConverter转换
    /// </summary>
    /// 把MultiBinding中的前两个值相乘, 然后偏移3
    /// <example>
    /// <MultiConverterGroup>
    ///     <MultiConverterGroupStep>
    ///         <IndexedObjectConverter Index="0"/>
    ///         <IndexedObjectConverter Index="1"/>
    ///     </MultiConverterGroupStep>
    ///     <MultiConverterGroupStep>
    ///         <MultiplyConverter/>
    ///     </MultiConverterGroupStep>
    ///     <MultiConverterGroupStep>
    ///         <FirstItemConverterParent Converter="{OffsetConverter Offset=3}"/>
    ///     </MultiConverterGroupStep>
    /// </MultiConverterGroup>
    /// </example>
    [ContentProperty("Converter")]
    public class FirstItemConverterParent:ProvideSelfMarkupExtension, IMultiValueConverter
    {
        private IValueConverter converter;
        public IValueConverter Converter
        {
            get { return converter; }
            set { converter = value; }
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            values = ConvertionObjectArrayList.GetArray(values);
            return converter.Convert(values[0], targetType, parameter, culture);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 把MultiBinding中的每个项先用同一个IValueConverter预转换一下
    /// </summary>
    [ContentProperty("Converter")]
    public class AllItemConverterParent : ProvideSelfMarkupExtension, IMultiValueConverter
    {
        private IValueConverter converter;
        public IValueConverter Converter
        {
            get { return converter; }
            set { converter = value; }
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            values = ConvertionObjectArrayList.GetArray(values);
            ConvertionObjectArrayList al = new ConvertionObjectArrayList();

            for (int i = 0; i < values.Length; i++)
                al.Add(converter.Convert(values[i], targetType, parameter, culture));

            return al;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] values = ((ConvertionObjectArrayList)value).GetArray();

            for (int i = 0; i < values.Length; i++)
                values[i] = converter.ConvertBack(values[i], targetTypes[i], parameter, culture);

            return values;
        }
    }

    /// <summary>
    /// 这个转换器对每一项可以用IValueConverter预转换, converters有次序, 如果其中某项不需转换则置null;
    /// </summary>
    [ContentProperty("Converters")]
    public class EachItemConverterParent : ProvideSelfMarkupExtension, IMultiValueConverter
    {
        private Collection<IValueConverter> converters;
        public Collection<IValueConverter> Converters
        {
            get { return converters; }
            set { converters = value; }
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            values = ConvertionObjectArrayList.GetArray(values);

            ConvertionObjectArrayList al = new ConvertionObjectArrayList();
            for (int i = 0; i < values.Length && i < converters.Count; i++)
                if (converters[i] != null)
                    al.Add(converters[i].Convert(values[i], targetType, parameter, culture));
            for (int i = al.Count; i < values.Length; i++)
                al.Add(values[i]);

            return al;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] values = ((ConvertionObjectArrayList)value).GetArray();

            for (int i = 0; i < values.Length && i < converters.Count; i++)
                if (converters[i] != null)
                    values[i] = converters[i].ConvertBack(values[i], targetTypes[i], parameter, culture);

            return values;
        }
    }
}
