using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// IMultiValueConvertor传入参数自扩展列表
    /// </summary>
    /// <example>
    /// 在IMultiValueConvertor的转换开始前对values调用
    /// values = ConvertionObjectArrayList.GetArray(values);
    /// 即可;
    /// </example>
    public class ConvertionObjectArrayList:ArrayList
    {
        /// <summary>
        /// 获得扩展后的对象列表
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static object[] GetArray(object[] values)
        {
            ArrayList al = new ArrayList();

            foreach (object value in values)
            {
                if (value is ConvertionObjectArrayList)
                    al.AddRange(((ConvertionObjectArrayList)value).GetArray());
                else
                    al.Add(value);
            }

            return al.ToArray();
        }

        public object[] GetArray()
        {
            return this.ToArray();
        }
    }
}
