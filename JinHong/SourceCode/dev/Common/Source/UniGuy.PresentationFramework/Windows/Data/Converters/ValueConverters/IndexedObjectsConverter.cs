using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Data;
using System.Globalization;
using UniGuy.Windows.Markup;
using UniGuy.Core.Extensions;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// 根据索引列表返回子集, 放在ConvertionObjectArrayList中
    /// </summary>
    public class IndexedObjectsConverter:ProvideSelfMarkupExtension, IMultiValueConverter
    {
        /// <summary>
        /// 所要的索引列表, 索引有顺序, 2,3和3,2对返回的结果有影响;
        /// 格式如: 1,4,5,6
        /// </summary>
        private string indexesString;
        public string IndexesString
        {
            get { return indexesString; }
            set { indexesString = value; }
        }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            values = ConvertionObjectArrayList.GetArray(values);
            ConvertionObjectArrayList al = new ConvertionObjectArrayList();
            foreach (string indexString in indexesString.SplitAndTrimEach(','))
            {
                int index = 0;
                if (int.TryParse(indexString, out index))
                {
                    al.Add(values[index]);
                }
                else
                {
                    throw new Exception();
                }
            }
            return al;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
