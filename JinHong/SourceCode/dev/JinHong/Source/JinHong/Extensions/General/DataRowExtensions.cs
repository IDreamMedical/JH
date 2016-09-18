using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using UniGuy.Core.Common;
using UniGuy.Core.Data;
using System.Reflection;

namespace JinHong.Extensions
{
    public static class DataRowExtensions
    {
        /// <summary>
        /// 根据DataRow创建指定类型的实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T BuildEntity<T>(this DataRow row) where T: new()
        {
            DataTable table = row.Table;
            Dictionary<string, object> kvd = new Dictionary<string, object>();
            PropertyInfo[] columnProperites = DatabaseHelper.GetColumnProperties(typeof(T));
            string columnName = null;
            foreach (PropertyInfo prop in columnProperites)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);

                if (columnAttr != null)
                {
                    columnName = columnAttr.ColumnName ?? prop.Name;
                    if (table.Columns.Contains(columnName))
                    {
                        object dataValue = row[columnName];
                        if (dataValue != System.DBNull.Value)
                            kvd.Add(columnName, dataValue);
                    }
                }
            }
            ColumnAttribute tcolumnAttr = DatabaseHelper.GetColumnAttribute(typeof(T));

            if (tcolumnAttr != null)
            {
                columnName = tcolumnAttr.ColumnName ?? typeof(T).Name;
                if (table.Columns.Contains(columnName))
                {
                    object value = row[columnName];
                    if (value != System.DBNull.Value)
                        kvd.Add(columnName, value);
                }
            }

            return (T)DatabaseHelper.FromKeyValueData(typeof(T), kvd, ColumnIoType.Read);
        }
    }
}
