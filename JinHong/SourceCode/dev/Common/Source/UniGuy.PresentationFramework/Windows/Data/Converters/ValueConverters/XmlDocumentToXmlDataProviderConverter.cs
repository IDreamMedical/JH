using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xml;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// </summary>
    [ValueConversion(typeof(XmlDocument), typeof(XmlDataProvider))]
    public class XmlDocumentToXmlDataProviderConverter:MarkupExtension, IValueConverter
    {
        #region Ctor
        public XmlDocumentToXmlDataProviderConverter() { }
        #endregion

        #region IValueConverter
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            XmlDocument doc = value as XmlDocument;
            if (doc != null)
                return new XmlDataProvider() { Document = doc };
            else
                throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
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
