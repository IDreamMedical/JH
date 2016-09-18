using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 时间转换器,偏移TimeSpan直接通过字符串参数传递,这个字符串参数必须可以被TimeSpan.Parse解析
    /// [ws][-]{ d | [d.]hh:mm[:ss[.ff]] }[ws]
    /// </summary>
    public class TimeOffsetConverter:MarkupExtension, IValueConverter
    {
        #region Ctor
        public TimeOffsetConverter() { }
        #endregion

        #region IValueConverter
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                if (parameter == null)
                    return value;

                if (parameter is string)
                {
                    TimeSpan ts = TimeSpan.Zero;
                    if (TimeSpan.TryParse((string)parameter, out ts))
                        return ((DateTime)value).Add(ts);
                    throw new ArgumentException("Parameter string must be parsable by Type TimeSpan.");
                }
                throw new ArgumentException("Parameter must be a string .");
            }
            throw new ArgumentException("Argument must be of type DateTime");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                if (parameter == null)
                    return value;

                if (parameter is string)
                {
                    TimeSpan ts = TimeSpan.Zero;
                    if (TimeSpan.TryParse((string)parameter, out ts))
                        return (DateTime)value - ts;
                    throw new ArgumentException("Parameter string must be parsable by Type TimeSpan.");
                }
                throw new ArgumentException("Parameter must be a string .");
            }
            throw new ArgumentException("Argument must be of type DateTime");
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
