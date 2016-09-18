using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 时间单位
    /// </summary>
    public enum TimeUnit
    {
        Year,
        Month,
        Day,
        Hour,
        Minute,
        Second,
        Millisecond,
        Tick
    }
    /// <summary>
    /// 时间转换器,一个转换器只能固定一个偏移量和偏移量单位,用途有限
    /// </summary>
    public class DateTimeAddConverter:MarkupExtension, IValueConverter
    {
        #region Fields
        /// <summary>
        /// 偏移量
        /// </summary>
        private double offset;
        /// <summary>
        /// 偏移量单位
        /// </summary>
        private TimeUnit unit = TimeUnit.Day;
        #endregion  // Fields

        #region Properties
        /// <summary>
        /// 获得或者设置偏移量
        /// </summary>
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
        /// <summary>
        /// 获得或者设置偏移量单位
        /// </summary>
        public TimeUnit Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        #endregion  // Properties

        #region Ctor
        public DateTimeAddConverter() { }
        public DateTimeAddConverter(double offset) { this.offset = offset; }
        public DateTimeAddConverter(double offset, TimeUnit unit)
        { this.offset = offset; this.unit = unit; }
        #endregion

        #region IValueConverter
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                if (unit == TimeUnit.Day)
                    return ((DateTime)value).AddDays(offset);
                else if (unit == TimeUnit.Hour)
                    return ((DateTime)value).AddHours(offset);
                else if (unit == TimeUnit.Millisecond)
                    return ((DateTime)value).AddMilliseconds(offset);
                else if (unit == TimeUnit.Minute)
                    return ((DateTime)value).AddMinutes(offset);
                else if (unit == TimeUnit.Month)
                    return ((DateTime)value).AddMonths(offset);
                else if (unit == TimeUnit.Second)
                    return ((DateTime)value).AddSeconds(offset);
                else if (unit == TimeUnit.Tick)
                    return ((DateTime)value).AddTicks(offset);
                else if (unit == TimeUnit.Year)
                    return ((DateTime)value).AddYears(offset);
            }
            throw new ArgumentException("Argument must be of type DateTime");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                if (unit == TimeUnit.Day)
                    return ((DateTime)value).AddDays(-offset);
                else if (unit == TimeUnit.Hour)
                    return ((DateTime)value).AddHours(-offset);
                else if (unit == TimeUnit.Millisecond)
                    return ((DateTime)value).AddMilliseconds(-offset);
                else if (unit == TimeUnit.Minute)
                    return ((DateTime)value).AddMinutes(-offset);
                else if (unit == TimeUnit.Month)
                    return ((DateTime)value).AddMonths(-offset);
                else if (unit == TimeUnit.Second)
                    return ((DateTime)value).AddSeconds(-offset);
                else if (unit == TimeUnit.Tick)
                    return ((DateTime)value).AddTicks(-offset);
                else if (unit == TimeUnit.Year)
                    return ((DateTime)value).AddYears(-offset);
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
