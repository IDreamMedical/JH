using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 字符串转换为可为空的布尔类型
    /// </summary>
    [ValueConversion(typeof(string), typeof(bool?))]
    public class StringToNullableBoolConverter:IValueConverter
    {
        private string trueString = true.ToString();
        private string falseString = false.ToString();
        private string nullString = null;

        public string TrueString
        {
            get { return trueString; }
            set { trueString = value; }
        }

        public string FalseString
        {
            get { return falseString; }
            set { falseString = value; }
        }

        public string NullString
        {
            get { return nullString; }
            set { nullString = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value as string;
            if (string.Compare(trueString, str, true) == 0)
                return true;
            else if (string.Compare(falseString, str, true) == 0)
                return false;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? b = value as bool?;
            if (b.HasValue)
            {
                if (b.Value)
                    return trueString;
                else
                    return falseString;
            }
            else
                return nullString;
        }
    }
}
