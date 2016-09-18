using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 给整型或者双精度型数据新增一个偏移量，
    /// 注意这里结合了MarkupExtension,所以不需要先定义在资源字典中而可以直接使用，
    /// 所有Converter都可以这么干，为什么不呢
    /// </summary>
    public class AdditionConverter:MarkupExtension, IValueConverter
    {
        #region Fields
        private double offset;
        #endregion  // Fields

        #region Properties
        public double Offset
        {
            get
            {
                return this.offset;
            }
            set
            {
                this.offset = value;
            }
        }
        #endregion  // Properties

        #region Ctor
        public AdditionConverter() { }
        public AdditionConverter(double offset)
        { this.offset = offset; }
        #endregion

        #region IValueConverter
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                return (((double)value) + this.offset);
            }
            if (value is int)
            {
                return (((int)value) + ((int)this.offset));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Overrides
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            //  爽快
            return this;
        }
        #endregion

    }
}
