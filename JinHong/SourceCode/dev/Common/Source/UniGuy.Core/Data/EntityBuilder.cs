using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace UniGuy.Core.Data
{
    public class EntityBuilder
    {
        /// <summary>
        /// 根据DataTable的某一行创建实体
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dataTable"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public static object BuildEntity(Type type, DataTable dataTable, int rowIndex)
        {

            if (dataTable != null && dataTable.Rows.Count > rowIndex)
            {
                DataRow row = dataTable.Rows[rowIndex];
                Dictionary<string, object> kvd = new Dictionary<string, object>();

                PropertyInfo[] columnProperites = DatabaseHelper.GetColumnProperties(type);
                foreach (PropertyInfo prop in columnProperites)
                {
                    ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                    if (columnAttr != null)
                    {
                        string columnName = columnAttr.ColumnName ?? prop.Name;
                        if (dataTable.Columns.Contains(columnName))
                        {
                            object dataValue = row[columnName];

                            //  这里不能用kvd.Add(columnAttr.ColumnName, dataValue);
                            //  因为如果类中多个属性使用相同Name的ColumnAttribute,这里会引发重复键异常
                            if (dataValue != System.DBNull.Value)
                                kvd[columnName] = dataValue;
                        }
                    }
                }
                ColumnAttribute tcolumnAttr = DatabaseHelper.GetColumnAttribute(type);
                if (tcolumnAttr != null)
                {
                    string tcolumnName = tcolumnAttr.ColumnName ?? type.Name;
                    if (dataTable.Columns.Contains(tcolumnName))
                    {
                        object value = row[tcolumnName];
                        if (value != System.DBNull.Value)
                            kvd[tcolumnName] = value;
                    }
                }

                //return entity;
                return DatabaseHelper.FromKeyValueData(type, kvd, ColumnIoType.Read);
            }
            return null;
           
        }

        public static object BuildEntity(Type type, DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0)
                return BuildEntity(type, ds.Tables[0], 0);
            return null;
        }

        public static List<object> BuildEntities(Type type, DataTable dataTable)
        {
            if (dataTable != null)
            {
                List<object> objs = new List<object>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                    objs.Add(BuildEntity(type, dataTable, i));
                return objs;
            }
            return null;
        }
        
        /// <summary>
        /// 根据DataTable创建对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public static T BuildEntity<T>(DataTable dataTable, int rowIndex) where T:IIdObject, new()
        {
            if (dataTable != null && dataTable.Rows.Count > rowIndex)
            {
                DataRow row = dataTable.Rows[rowIndex];
                Dictionary<string, object> kvd = new Dictionary<string, object>();

                PropertyInfo[] columnProperites = DatabaseHelper.GetColumnProperties(typeof(T));
                foreach (PropertyInfo prop in columnProperites)
                {
                    ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                    if (columnAttr != null)
                    {
                        string columnName = columnAttr.ColumnName ?? prop.Name;
                        if (dataTable.Columns.Contains(columnName))
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
                    string tcolumnName = tcolumnAttr.ColumnName ?? typeof(T).Name;
                    if (dataTable.Columns.Contains(tcolumnName))
                    {
                        object value = row[tcolumnName];
                        if (value != System.DBNull.Value)
                            kvd.Add(tcolumnName, value);
                    }
                }

                //return entity;
                return (T)DatabaseHelper.FromKeyValueData(typeof(T), kvd, ColumnIoType.Read);
            }
            return default(T);
        }

        public static List<T> BuildEntities<T>(DataTable dataTable) where T:IIdObject, new()
        {
            if (dataTable != null)
            {
                List<T> ts = new List<T>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                    ts.Add(BuildEntity<T>(dataTable, i));
                return ts;
            }
            return null;
        }

        public static List<T> BuildEntities<T>(DataSet dataSet) where T : IIdObject, new()
        {
            if (dataSet != null && dataSet.Tables.Count>0)
            {
                return BuildEntities<T>(dataSet.Tables[0]);
            }
            return null;
        }

        /// <summary>
        /// 根据DataTable创建对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public static T BuildEntityUsingColumns<T>(DataTable dataTable, int rowIndex) where T : IIdObject, new()
        {
            if (dataTable != null && dataTable.Rows.Count > rowIndex)
            {
                DataRow row = dataTable.Rows[rowIndex];
                Dictionary<string, object> kvd = new Dictionary<string, object>();

                PropertyInfo[] columnProperites = DatabaseHelper.GetColumnProperties(typeof(T));
                foreach (PropertyInfo prop in columnProperites)
                {
                    ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                    if (columnAttr != null)
                    {
                        string columnName = columnAttr.ColumnName ?? prop.Name;
                        if (dataTable.Columns.Contains(columnName))
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
                    string tcolumnName = tcolumnAttr.ColumnName ?? typeof(T).Name;
                    if (dataTable.Columns.Contains(tcolumnName))
                    {
                        object value = row[tcolumnName];
                        if (value != System.DBNull.Value)
                            kvd.Add(tcolumnName, value);
                    }
                }

                //return entity;
                return (T)DatabaseHelper.FromKeyValueData(typeof(T), kvd);
            }
            return default(T);
        }

        public static List<T> BuildEntitiesUsingColumns<T>(DataTable dataTable) where T : IIdObject, new()
        {
            if (dataTable != null)
            {
                List<T> ts = new List<T>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                    ts.Add(BuildEntityUsingColumns<T>(dataTable, i));
                return ts;
            }
            return null;
        }

        public static List<T> BuildEntitiesUsingColumns<T>(DataSet dataSet) where T : IIdObject, new()
        {
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                return BuildEntitiesUsingColumns<T>(dataSet.Tables[0]);
            }
            return null;
        }
    }
}
