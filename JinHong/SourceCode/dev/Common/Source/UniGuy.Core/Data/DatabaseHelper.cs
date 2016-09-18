using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using UniGuy.Core.Helpers;

namespace UniGuy.Core.Data
{
    public class DatabaseHelper
    {
        /// <summary>
        /// 获得类型的TableAttribute特性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static TableAttribute GetTableAttribute(Type type)
        {
            object[] tableAttrs = type.GetCustomAttributes(typeof(TableAttribute), false);
            if (tableAttrs.Length > 0)
                return (TableAttribute)tableAttrs[0];
            return null;
        }
        /// <summary>
        /// 获得类型对应的表名
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>对应的数据库表名</returns>
        public static string GetTableName(Type type)
        {
            TableAttribute tableAttr = GetTableAttribute(type);
            // 如果拥有类型定义了TableAttribute特性，则取该书性的名称, 否则直接返回类型名称
            return tableAttr == null ? type.Name : tableAttr.TableName;
        }
        /// <summary>
        /// 获得对应数据库字段的属性数组
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static PropertyInfo[] GetColumnProperties(Type type)
        {
            List<PropertyInfo> columnPropertyList = new List<PropertyInfo>();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                ColumnAttribute columnAttr = GetColumnAttribute(prop);
                if (columnAttr != null)
                    columnPropertyList.Add(prop);
            }
            return columnPropertyList.ToArray();
        }

        /// <summary>
        /// 获得对应数据库字段的属性数组
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static PropertyInfo[] GetPrimaryKeyProperties(Type type)
        {
            List<PropertyInfo> primaryKeyPropertyList = new List<PropertyInfo>();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                PrimaryKeyAttribute primaryKeyAttr = GetPrimaryKeyAttribute(prop);
                if (primaryKeyAttr != null)
                    primaryKeyPropertyList.Add(prop);
            }
            return primaryKeyPropertyList.ToArray();
        }

        /// <summary>
        /// 获得属性的ColumnAttrite特性
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static ColumnAttribute GetColumnAttribute(PropertyInfo pi)
        {
            object[] columnAttrs = pi.GetCustomAttributes(typeof(ColumnAttribute), true);
            if (columnAttrs.Length > 0)
                return (ColumnAttribute)columnAttrs[0];
            return null;
        }
        /// <summary>
        /// 获得类型的ColumnAttribute属性
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ColumnAttribute GetColumnAttribute(Type t)
        {
            object[] columnAttrs = t.GetCustomAttributes(typeof(ColumnAttribute), true);
            if (columnAttrs.Length > 0)
                return (ColumnAttribute)columnAttrs[0];
            return null;
        }
        /// <summary>
        /// 获得类型及其属性的ColumnAttribute数组
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ColumnAttribute[] GetColumnAttributes(Type t)
        {
            PropertyInfo[] props = GetColumnProperties(t);
            ColumnAttribute attr = GetColumnAttribute(t);
            ColumnAttribute[] attrs = null;
            if (props != null)
                attrs = Array.ConvertAll<PropertyInfo, ColumnAttribute>(props, new Converter<PropertyInfo, ColumnAttribute>(delegate(PropertyInfo prop)
                    {
                        return GetColumnAttribute(prop);
                    }));
            if (attr != null)
            {
                if (attrs == null)
                    attrs = new ColumnAttribute[] { attr };
                else
                {
                    Array.Resize<ColumnAttribute>(ref attrs, attrs.Length + 1);
                    attrs[attrs.Length - 1] = attr;
                }
            }
            return attrs;
        }
        /// <summary>
        /// 获得和属性对应的字段列名
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static string GetColumnName(PropertyInfo pi)
        {
            ColumnAttribute columnAttr = GetColumnAttribute(pi);
            //  如果定义了ColumnAttribute则返回其定义的列名，否则直接返回属性名
            return columnAttr == null ? pi.Name : columnAttr.ColumnName??pi.Name;
        }

        /// <summary>
        /// 根据类型和属性名获得列名
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string GetColumnName(Type type, string propertyName)
        {
            PropertyInfo prop = type.GetProperty(propertyName);
            return prop == null ? null : GetColumnName(prop);
        }

        /// <summary>
        /// 获得属性的PrimaryKeyAttrite特性
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static PrimaryKeyAttribute GetPrimaryKeyAttribute(PropertyInfo pi)
        {
            object[] primaryKeyAttrs = pi.GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
            if (primaryKeyAttrs.Length > 0)
                return (PrimaryKeyAttribute)primaryKeyAttrs[0];
            return null;
        }

        /// <summary>
        /// 把对象转换为键值数据
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="datarowable">是否查看对象为IDataRowable</param>
        /// <returns>键值数据</returns>
        public static Dictionary<string, object> ToKeyValueData(object obj, ColumnIoType columnIoType)
        {
            Dictionary<string, object> kvd = new Dictionary<string, object>();
            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(obj.GetType());
            if (columnProperties != null)
            {
                foreach (PropertyInfo prop in columnProperties)
                {
                    ColumnAttribute columnAttr = GetColumnAttribute(prop);
                    if (columnAttr != null && ((columnAttr.ColumnIoType & columnIoType) != 0))
                    {
                        string columnName = columnAttr.ColumnName ?? prop.Name;
                        object value = prop.GetValue(obj, null);
                        if (columnAttr.SerializationType == SerializationType.None)
                            kvd[columnName] = ConvertionHelper.ConvertToString(value);
                        else if (columnAttr.SerializationType == SerializationType.Binary)
                            kvd[columnName] = value == null ? null : SerializationHelper.Serialize(value);
                        else if (columnAttr.SerializationType == SerializationType.Xml)
                            kvd[columnName] = value == null ? null : SerializationHelper.XmlSerializeToString(value);
                        else if (columnAttr.SerializationType == SerializationType.DateTime)
                            kvd[columnName] = value;
                        else if (columnAttr.SerializationType == SerializationType.BoolToByte)
                            kvd[columnName] = Convert.ToInt32(value);
                        else if (columnAttr.SerializationType == SerializationType.EnumToByte)
                            kvd[columnName] = (int)value;

                        // Dictionary
                        else if (columnAttr.SerializationType == SerializationType.Dictionary)
                        {
                            IDictionary<string, string> dct = value as IDictionary<string, string>;
                            if (dct != null)
                            {
                                foreach (string key in dct.Keys)
                                    kvd[key] = dct[key];
                            }
                        }
                        else
                            kvd[columnName] = value;
                    }
                }
            }
            Type type = obj.GetType();
            ColumnAttribute tcolumnAttr = GetColumnAttribute(type);
            if (tcolumnAttr != null && ((tcolumnAttr.ColumnIoType & columnIoType) != 0))
            {
                string tcolumnName = tcolumnAttr.ColumnName ?? type.Name;
                if (tcolumnAttr.SerializationType == SerializationType.None)
                    kvd[tcolumnName] = ConvertionHelper.ConvertToString(obj);
                else if (tcolumnAttr.SerializationType == SerializationType.Binary)
                    kvd[tcolumnName] = obj == null ? null : SerializationHelper.Serialize(obj);
                else if (tcolumnAttr.SerializationType == SerializationType.Xml)
                    kvd[tcolumnName] = obj == null ? null : SerializationHelper.XmlSerializeToString(obj);
                else if (tcolumnAttr.SerializationType == SerializationType.DateTime)
                    kvd[tcolumnName] = obj;
                else if (tcolumnAttr.SerializationType == SerializationType.BoolToByte)
                    kvd[tcolumnName] = Convert.ToBoolean(obj);
                else if (tcolumnAttr.SerializationType == SerializationType.BoolToByte)
                    kvd[tcolumnName] = Enum.ToObject(type, obj);
                
                // Dictionary
                else if (tcolumnAttr.SerializationType == SerializationType.Dictionary)
                {
                    IDictionary<string, string> dct = obj as IDictionary<string, string>;
                    if (dct != null)
                    {
                        foreach(string key in dct.Keys)
                            kvd[key] = dct[key];
                    }
                }
                else
                    kvd[tcolumnName] = obj;
            }
            return kvd;
        }
        /// <summary>
        /// 获得类型的列特性集合
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="columnIoType"></param>
        /// <returns>列特性集合</returns>
        public static List<ColumnAttribute> ToColumnAttributes(Type type, ColumnIoType columnIoType)
        {
            List<ColumnAttribute> cas = new List<ColumnAttribute>();
            ColumnAttribute tcolumnAttr = GetColumnAttribute(type);
            if (tcolumnAttr != null && ((tcolumnAttr.ColumnIoType & columnIoType) != 0))
                cas.Add(tcolumnAttr);
            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(type);
            if (columnProperties != null)
            {
                foreach (PropertyInfo prop in columnProperties)
                {
                    ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                    if (columnAttr != null && ((columnAttr.ColumnIoType & columnIoType) != 0))
                        cas.Add(columnAttr);

                    //  Dictionary
                }
            }
            return cas;
        }
        /// <summary>
        /// 获得类型的键集合
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>键集合</returns>
        public static List<string> ToKeys(Type type, ColumnIoType columnIoType)
        {
            List<string> keys = new List<string>();
            ColumnAttribute tcolumnAttr = GetColumnAttribute(type);
            if (tcolumnAttr != null && ((tcolumnAttr.ColumnIoType & columnIoType) != 0))
                keys.Add(tcolumnAttr.ColumnName??type.Name);
            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(type);
            if (columnProperties != null)
            {
                foreach (PropertyInfo prop in columnProperties)
                {
                    ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                    if (columnAttr != null && ((columnAttr.ColumnIoType & columnIoType) != 0))
                        keys.Add(columnAttr.ColumnName??prop.Name);

                    //  Dictionary
                }
            }
            return keys;
        }
        /// <summary>
        /// 从键值数据填充对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="kvd">键值数据</param>
        /// <param name="datarowable">是否查看对象为IDataRowable</param>
        public static object FromKeyValueData(Type type, Dictionary<string, object> kvd, ColumnIoType columnIoType)
        {
            object obj = null;

            ColumnAttribute tcolumnAttr = GetColumnAttribute(type);
            //  如果整个对象保存了持久化数据就直接反序列化
            if (tcolumnAttr != null && ((tcolumnAttr.ColumnIoType & columnIoType) != 0))
            {
                string tcolumnName = tcolumnAttr.ColumnName ?? type.Name;
                if (kvd.ContainsKey(tcolumnName))
                {
                    object value = kvd[tcolumnName];
                    if (value == null)
                        obj = null;
                    else if (tcolumnAttr.SerializationType == SerializationType.None)
                        obj = ConvertionHelper.ConvertToObject(type, value);
                    else if (tcolumnAttr.SerializationType == SerializationType.Binary)
                        obj = SerializationHelper.Deserialize((byte[])value);
                    else if (tcolumnAttr.SerializationType == SerializationType.Xml)
                        obj = SerializationHelper.XmlDeserializeFromString(type, (string)value);
                    else if (tcolumnAttr.SerializationType == SerializationType.DateTime)
                        obj = DateTime.Parse(value.ToString());
                    else if (tcolumnAttr.SerializationType == SerializationType.BoolToByte)
                        obj = Convert.ToBoolean(value);
                    else if (tcolumnAttr.SerializationType == SerializationType.EnumToByte)
                        obj = Enum.ToObject(type, value);
                    else
                        obj = Convert.ChangeType(value, type);
                }
            }

            if (obj == null)
                obj = Activator.CreateInstance(type);

            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(type);
            if (columnProperties != null)
            {
                foreach (PropertyInfo prop in columnProperties)
                {
                    ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                    if (columnAttr != null && (columnAttr.ColumnIoType & columnIoType) != 0)
                    {
                        string columnName = columnAttr.ColumnName ?? prop.Name;
                        if (kvd.ContainsKey(columnName))
                        {
                            object value = kvd[columnName];
                            if (columnAttr.SerializationType == SerializationType.None)
                                prop.SetValue(obj, ConvertionHelper.ConvertToObject(prop.PropertyType, value), null);
                            else if (columnAttr.SerializationType == SerializationType.Binary)
                                prop.SetValue(obj, value == null ? null : SerializationHelper.Deserialize((byte[])value), null);
                            else if (columnAttr.SerializationType == SerializationType.Xml)
                                prop.SetValue(obj, value == null ? null : SerializationHelper.XmlDeserializeFromString(prop.PropertyType, (string)value), null);
                            else if (columnAttr.SerializationType == SerializationType.DateTime)
                                prop.SetValue(obj, DateTime.Parse(value.ToString()), null);
                            else if (columnAttr.SerializationType == SerializationType.BoolToByte)
                                prop.SetValue(obj, Convert.ToBoolean(value), null);
                            else if (columnAttr.SerializationType == SerializationType.EnumToByte)
                                prop.SetValue(obj, Enum.ToObject(prop.PropertyType, value), null);
                            else
                                prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType),null);
                        }
                    }
                }
            }
            return obj;
        }

        public static object FromKeyValueData(Type type, Dictionary<string, object> kvd)
        {
            object obj = Activator.CreateInstance(type);
            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(type);
            if (columnProperties != null)
            {
                foreach (PropertyInfo prop in columnProperties)
                {
                    ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                    if (columnAttr != null)
                    {
                        string columnName = columnAttr.ColumnName ?? prop.Name;
                        if (kvd.ContainsKey(columnName))
                        {
                            object value = kvd[columnName];
                            if (columnAttr.SerializationType == SerializationType.None)
                                prop.SetValue(obj, ConvertionHelper.ConvertToObject(prop.PropertyType, value), null);
                            else if (columnAttr.SerializationType == SerializationType.Binary)
                                prop.SetValue(obj, value == null ? null : SerializationHelper.Deserialize((byte[])value), null);
                            else if (columnAttr.SerializationType == SerializationType.Xml)
                                prop.SetValue(obj, value == null ? null : SerializationHelper.XmlDeserializeFromString(prop.PropertyType, (string)value), null);
                            else if (columnAttr.SerializationType == SerializationType.DateTime)
                                prop.SetValue(obj, DateTime.Parse(value.ToString()), null);
                            else if (columnAttr.SerializationType == SerializationType.BoolToByte)
                                prop.SetValue(obj, Convert.ToBoolean(value), null);
                            else if (columnAttr.SerializationType == SerializationType.EnumToByte)
                                prop.SetValue(obj, Enum.ToObject(prop.PropertyType, value), null);
                            else
                                prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType), null);
                        }
                    }
                }
            }
            return obj;
        }
        /// <summary>
        /// 把属性本身或序列化结果进一步根据数据库字段类型转换
        /// 这里的ChangeType可能需要具体实现
        /// ToKeyValueData方法可以调用本方法
        /// </summary>
        /// <param name="ca"></param>
        /// <param name="pv"></param>
        /// <returns></returns>
        [Obsolete]
        public static object StringToObject(ColumnAttribute ca, object pv)
        {
            object result = null;
            switch (ca.DbType)
            {
                case DBType.Binary:
                    result = Convert.ChangeType(pv, typeof(byte[]));
                    break;
                case DBType.Byte:
                    result = Convert.ChangeType(pv, typeof(byte));
                    break;
                //  TODO
                default:
                    result = pv;
                    break;
            }
            return pv;
        }
        /// <summary>
        /// 把属性本身或序列化结果进一步根据数据库字段类型反转换
        /// 这里的ChangeType可能需要具体实现
        /// FromKeyValueData方法可以调用本方法
        /// </summary>
        /// <param name="ca"></param>
        /// <param name="sv"></param>
        /// <returns></returns>
        [Obsolete]
        public static object ObjectToString(ColumnAttribute ca, object sv)
        {
            object result = null;
            switch (ca.DbType)
            {
                case DBType.Binary:
                    result = Convert.ChangeType(sv, typeof(string));
                    break;
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// "abc"->"'abc'", & null->"null"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ValueStringNormalize(string str)
        {
            return str == null ? "null" : string.Format("'{0}'", str);
        }
    }
}
