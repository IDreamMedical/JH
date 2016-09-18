using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Windows.Data;
using System.Globalization;
using UniGuy.Core;
using UniGuy.Core.Attributes;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 通过Uri指定的内容反序列化为CodeAssembly
    /// </summary>
    [Author("UniGuy"), LastModifyTime("2010-11-27")]
    [ValueConversion(typeof(Uri), typeof(DataAssembly))]
    public class UriToCodeAssemblyConverter : MarkupExtension, IValueConverter
    {
        #region Fields

        /// <summary>
        /// 静态缓存
        /// </summary>
        private static Dictionary<string, WeakReference> cache;

        /// <summary>
        /// 是否使用静态缓存(默认为True)
        /// </summary>
        private bool usingCache = true;

        #endregion

        #region Properties
        /// <summary>
        /// 获得或者设置是否使用静态缓存
        /// </summary>
        public bool UsingCache
        {
            get { return usingCache; }
            set { usingCache = value; }
        }
        #endregion

        #region Ctor
        static UriToCodeAssemblyConverter()
        {
            cache = new Dictionary<string, WeakReference>();
        }
        public UriToCodeAssemblyConverter() { }
        #endregion

        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Uri source = value as Uri;
            string key = source.ToString();

            if (usingCache)
                if (cache != null)
                    if (cache.ContainsKey(key))
                        if (cache[key].IsAlive)
                            return cache[key].Target;

            if (source != null)
            {
                Stream stream = System.Windows.Application.GetResourceStream(source).Stream;
                XmlSerializer xs = new XmlSerializer(typeof(CodeAssembly));
                CodeAssembly ca = (CodeAssembly)xs.Deserialize(stream);
                if (usingCache)
                    cache[key] = new WeakReference(ca);
                return ca;
            }
            throw new ArgumentNullException();
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
