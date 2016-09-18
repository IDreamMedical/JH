using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Data;
using System.Globalization;
using UniGuy.Windows.Markup;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 根据一个索引返回列表中的某一项
    /// </summary>
    public class IndexedObjectConverter:ProvideSelfMarkupExtension, IMultiValueConverter
    {
        /// <summary>
        /// 所要的索引
        /// </summary>
        private int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            values = ConvertionObjectArrayList.GetArray(values);
            return values[index];
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
