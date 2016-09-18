﻿using System;
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
    /// 如果为空就返回参数
    /// </summary>
    [ValueConversion(typeof(bool), typeof(object))]
    public class IfFalseConverter: MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool && (!(bool)value))?parameter:value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == parameter ? false : value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
