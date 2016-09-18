using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JinHong.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// 获得datatable中的某个项并转换为制定类型
        /// </summary>
        /// <typeparam employeeName="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="rowName"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static T GetValue<T>(this DataTable table, string rowName, string colName)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row[0] is string)
                {
                    if ((string)row[0] == rowName)
                    {
                        object obj = row[colName];
                        return obj is DBNull?default(T):(T)Convert.ChangeType(obj, typeof(T));
                    }
                }
            }

            throw new Exception("No value found.");
        }
    }
}
