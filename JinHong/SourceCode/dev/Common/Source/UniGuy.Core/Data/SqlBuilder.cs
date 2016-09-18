using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data.Common;
using System.Data.OleDb;
using UniGuy.Core.Helpers;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// 这个类主要是为了读取实体类上的特性，来拼接字符串。
    /// 可以对实体类中的索引器进行映射。
    /// </summary>
    public class SqlBuilder
    {
        /// <summary>
        /// 使用委托包装字符串
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="str">原始字符串</param>
        /// <returns>包装后的字符串</returns>
        public static string Encap(string connectionString, string fieldName)
        {
            Database.OnNameEncapsulate(connectionString, ref fieldName);
            return fieldName;
        }
        /// <summary>
        /// 使用委托解码数据库数据类型枚举
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="dbt">数据库数据类型枚举</param>
        /// <returns>数据库类型特异的数据类型描述字符串</returns>
        public static string DecodeType(string connectionString, DBType dbt, ref bool needSize)
        {
            string typeName = null;
            Database.OnSpecialDbTypeToTypeName(connectionString, dbt, ref typeName, ref needSize);
            return typeName;
        }
        /// <summary>
        /// 创建sql
        /// </summary>
        /// <param name="sqlType">sql类型</param>
        /// <param name="entity">关联的对象</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildSql(string connectionString, SqlType sqlType, object entity)
        {
            string strSql = null;
            switch (sqlType)
            {
                case SqlType.Select:
                    strSql = BuildSelectByIdSql(connectionString, entity);
                    break;
                case SqlType.Insert:
                    strSql = BuildInsertSql(connectionString, entity);
                    break;
                case SqlType.Update:
                    strSql = BuildUpdateSql(connectionString, entity);
                    break;
                case SqlType.Delete:
                    strSql = BuildDeleteSql(connectionString, entity);
                    break;
                case SqlType.IsExists:
                    strSql = BuildIsExistsSql(connectionString, entity);
                    break;
                default:
                    break;
            }
            return strSql;
        }
        /// <summary>
        /// 创建数据库命令
        /// </summary>
        /// <param name="sqlType">sql类型</param>
        /// <param name="entity">关联的对象</param>
        /// <param name="parameters">用键值对字典表示的命令参数，这是一个引用类型的参数</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildCommand(string connectionString, SqlType sqlType, object entity, ref SqlParameterDictionary parameters)
        {
            string strSql = null;
            switch (sqlType)
            {
                case SqlType.Select:
                    strSql = BuildSelectByKeysCommand(connectionString, entity, ref parameters);
                    break;
                case SqlType.Insert:
                    strSql = BuildInsertCommand(connectionString, entity, ref parameters);
                    break;
                case SqlType.Update:
                    strSql = BuildUpdateCommand(connectionString, entity, ref parameters);
                    break;
                case SqlType.Delete:
                    strSql = BuildDeleteCommand(connectionString, entity, ref parameters);
                    break;
                case SqlType.IsExists:
                    strSql = BuildIsExistsCommand(connectionString, entity, ref parameters);
                    break;
                default:
                    break;
            }
            return null;
        }
        /// <summary>
        /// 创建IsExistsTable(是否存在和实体对应的表)的sql
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildIsExistsTableSql(string connectionString, Type type)
        {
            OleDbConnection conn = Database.GetConnection(connectionString);
            if (conn == null)
                throw new Exception("Can not create connection.");
            StringBuilder sbSql = null;
            switch (conn.Provider)
            {
                case "SQLOLEDB":
                    sbSql = new StringBuilder("SELECT Count(*) FROM ");
                    sbSql.Append("sysobjects WHERE xType='U' AND Name='");
                    sbSql.Append(DatabaseHelper.GetTableName(type));
                    sbSql.Append("'");
                    break;
                case "OraOLEDB.Oracle.1":
                    sbSql = new StringBuilder("SELECT Count(*) FROM ");
                    sbSql.Append("user_tables WHERE table_name='");
                    sbSql.Append(DatabaseHelper.GetTableName(type));
                    sbSql.Append("'");
                    break;
                //  TODO
                default:
                    break;
            }
            return sbSql == null ? null : sbSql.ToString();
        }
        public static string BuildIsExistsTableCommand(string connectionString, Type type, ref SqlParameterDictionary parameters)
        {
            OleDbConnection conn = Database.GetConnection(connectionString);
            if (conn == null)
                throw new Exception("Can not create connection.");
            StringBuilder sbSql = null;
            SqlParameterDictionary whereParas = null;
            switch (conn.Provider)
            {
                case "SQLOLEDB":
                    sbSql = new StringBuilder("SELECT Count(*) FROM sysobjects WHERE ");
                    whereParas = new SqlParameterDictionary();
                    whereParas.Add("xType", "U");
                    whereParas.Add("Name", DatabaseHelper.GetTableName(type));
                    sbSql.Append(BuildWhereWithParameters(connectionString, whereParas, ref parameters));
                    break;
                case "OraOLEDB.Oracle.1":
                    sbSql = new StringBuilder("SELECT Count(*) FROM user_tables WHERE ");
                    whereParas = new SqlParameterDictionary();
                    whereParas.Add("table_name", DatabaseHelper.GetTableName(type).ToUpper());
                    sbSql.Append(BuildWhereWithParameters(connectionString, whereParas, ref parameters));
                    break;
                //  TODO
                default:
                    break;
            }
            return sbSql == null ? null : sbSql.ToString();
        }
        /// <summary>
        /// 创建指定实体类型的建表语句
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>建表语句</returns>
        public static string BuildCreateTableSql(string connectionString, Type type)
        {
            List<ColumnAttribute> cas = DatabaseHelper.ToColumnAttributes(type, ColumnIoType.Write);
            if (cas != null)
            {
                StringBuilder sbSql = new StringBuilder("CREATE TABLE ")
                    .Append(Encap(connectionString, DatabaseHelper.GetTableName(type)))
                    .Append(" (");

                foreach (ColumnAttribute ca in cas)
                {
                    bool needSize = default(bool);
                    sbSql.Append(Encap(connectionString, ca.ColumnName)).Append(' ')
                        .Append(DecodeType(connectionString, ca.DbType, ref needSize));
                    if (needSize)
                        sbSql.Append('(').Append(ca.ColumnSize).Append(')');
                    sbSql.Append(", ");
                }
                sbSql.Remove(sbSql.Length - 2, 2);
                sbSql.Append(")");

                return sbSql.ToString();
            }
            else
            {
                throw new Exception(string.Format("Type[{0}] does not have any ColumnAttributes.", type.Name));
            }
        }
        /// <summary>
        /// 创建IsExists的sql
        /// </summary>
        /// <param name="entity">关联的对象</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildIsExistsSql(string connectionString, object entity)
        {
            StringBuilder sbSql = new StringBuilder("SELECT Count(*) FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(entity.GetType())));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithPrimary(connectionString, entity));
            return sbSql.ToString();
        }
        public static string BuildIsExistsSql(string connectionString, Type type, SqlParameterDictionary whereParas)
        {
            StringBuilder sbSql = new StringBuilder("SELECT count(*) FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(type)));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithParameters(connectionString, whereParas));
            return sbSql.ToString();
        }
        /// <summary>
        /// 创建IsExists的数据库命令
        /// </summary>
        /// <param name="entity">关联的对象</param>
        /// <param name="parameters">用键值对字典表示的命令参数，这是一个引用类型的参数</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildIsExistsCommand(string connectionString, object entity, ref SqlParameterDictionary parameters)
        {
            StringBuilder sbSql = new StringBuilder("SELECT count(*) FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(entity.GetType())));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithPrimary(connectionString, entity, ref parameters));
            return sbSql.ToString();
        }
        public static string BuildIsExistsCommand(string connectionString, Type type, SqlParameterDictionary whereParas, ref SqlParameterDictionary parameters)
        {
            StringBuilder sbSql = new StringBuilder("SELECT count(*) FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(type)));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithParameters(connectionString, whereParas, ref parameters));
            return sbSql.ToString();
        }

        /// <summary>
        /// 创建Delete的sql
        /// </summary>
        /// <param name="entity">关联的对象</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildDeleteSql(string connectionString, object entity)
        {
            StringBuilder sbSql = new StringBuilder("DELETE FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(entity.GetType())));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithPrimary(connectionString, entity));
            return sbSql.ToString();
        }

        /// <summary>
        /// 创建Delete的sql
        /// </summary>
        /// <param name="type">关联对象的类型</param>
        /// <param name="whereParas">Where条件参数</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildDeleteSql(string connectionString, Type type, SqlParameterDictionary whereParas)
        {
            StringBuilder sbSql = new StringBuilder("DELETE FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(type)));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithParameters(connectionString, whereParas));
            return sbSql.ToString();
        }
        /// <summary>
        /// 创建Delete的命令
        /// </summary>
        /// <param name="entity">关联的对象</param>
        /// <param name="whereParas">Where条件参数</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildDeleteCommand(string connectionString, object entity, ref SqlParameterDictionary parameters)
        {
            StringBuilder sbSql = new StringBuilder("DELETE FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(entity.GetType())));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithPrimary(connectionString, entity, ref parameters));
            return sbSql.ToString();
        }
        /// <summary>
        /// 创建Delete命令
        /// </summary>
        /// <param name="type">关联对象的类型</param>
        /// <param name="whereParas">Where条件参数</param>
        /// <param name="parameters">用键值对字典表示的命令参数，这是一个引用类型的参数</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildDeleteCommand(string connectionString, Type type, SqlParameterDictionary whereParas, ref SqlParameterDictionary parameters)
        {
            StringBuilder sbSql = new StringBuilder("DELETE FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(type)));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithParameters(connectionString, whereParas, ref parameters));
            return sbSql.ToString();
        }
        /// <summary>
        /// 创建使用Where的查询语句
        /// </summary>
        /// <param name="type">关联对象的类型</param>
        /// <param name="whereParas">Where条件参数</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildSelectByWhereSql(string connectionString, Type type, SqlParameterDictionary whereParas)
        {
            StringBuilder sbSql = new StringBuilder("SELECT ");
            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(type);

            int count = 0;
            /*
            foreach (PropertyInfo prop in columnProperties)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                if (columnAttr != null)
                {
                    sbSql.Append(columnAttr.ColumnName + ", ");
                    count++;
                }
            }*/

            foreach (string key in DatabaseHelper.ToKeys(type, ColumnIoType.Read))
            {
                sbSql.Append(Encap(connectionString, key) + ", ");
                count++;
            }

            if (count > 0)
                sbSql.Remove(sbSql.Length - 2, 2);

            sbSql.Append(" FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(type)));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithParameters(connectionString, whereParas));
            return sbSql.ToString();
        }
        /// <summary>
        /// 创建使用Id的查询语句
        /// </summary>
        /// <param name="entity">关联的对象</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildSelectByIdSql(string connectionString, object entity)
        {
            Type type = entity.GetType();
            StringBuilder sbSql = new StringBuilder("SELECT ");
            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(type);

            int count = 0;
            /*
            foreach (PropertyInfo prop in columnProperties)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                if (columnAttr != null)
                {
                    sbSql.Append(columnAttr.ColumnName + ", ");
                    count++;
                }
            }*/
            foreach (string key in DatabaseHelper.ToKeys(type, ColumnIoType.Read))
            {
                sbSql.Append(Encap(connectionString, key) + ", ");
                count++;
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 2, 2);

            sbSql.Append(" FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(type)));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithPrimary(connectionString, entity));
            return sbSql.ToString();
        }
        public static string BuildSelectByKeysSql(string connectionString, Type type, SqlParameterDictionary primaryKeyParas)
        {
            StringBuilder sbSql = new StringBuilder("SELECT ");
            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(type);

            int count = 0;
            foreach (string key in DatabaseHelper.ToKeys(type, ColumnIoType.Read))
            {
                sbSql.Append(Encap(connectionString, key) + ", ");
                count++;
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 2, 2);

            sbSql.Append(" FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(type)));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithPrimary(connectionString, type, primaryKeyParas));
            return sbSql.ToString();
        }
        /// <summary>
        /// 根据Id创建查询命令
        /// </summary>
        /// <param name="entity">关联的对象</param>
        /// <param name="parameters">用键值对字典表示的命令参数，这是一个引用类型的参数</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildSelectByKeysCommand(string connectionString, object entity, ref SqlParameterDictionary parameters)
        {
            Type type = entity.GetType();
            StringBuilder sbSql = new StringBuilder("SELECT ");
            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(type);

            int count = 0;
            /*
            foreach (PropertyInfo prop in columnProperties)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                if (columnAttr != null)
                {
                    sbSql.Append(columnAttr.ColumnName + ", ");
                    count++;
                }
            }*/
            foreach (string key in DatabaseHelper.ToKeys(type, ColumnIoType.Read))
            {
                sbSql.Append(Encap(connectionString, key) + ", ");
                count++;
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 2, 2);

            sbSql.Append(" FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(type)));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithPrimary(connectionString, entity, ref parameters));
            return sbSql.ToString();
        }
        public static string BuildSelectByKeysCommand(string connectionString, Type type, SqlParameterDictionary primaryKeyParas, ref Dictionary<string, object> parameters)
        {
            StringBuilder sbSql = new StringBuilder("SELECT ");
            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(type);

            int count = 0;
            /*
            foreach (PropertyInfo prop in columnProperties)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                if (columnAttr != null)
                {
                    sbSql.Append(columnAttr.ColumnName + ", ");
                    count++;
                }
            }*/
            foreach (string key in DatabaseHelper.ToKeys(type, ColumnIoType.Read))
            {
                sbSql.Append(Encap(connectionString, key) + ", ");
                count++;
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 2, 2);

            sbSql.Append(" FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(type)));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithPrimary(connectionString, type, primaryKeyParas, ref parameters));
            return sbSql.ToString();
        }
        /// <summary>
        ///  创建使用Where的查询命令
        /// </summary>
        /// <param name="type">关联对象的类型</param>
        /// <param name="whereParas">Where条件参数</param>
        /// <param name="parameters">用键值对字典表示的命令参数，这是一个引用类型的参数</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildSelectByWhereCommand(string connectionString, Type type, SqlParameterDictionary whereParas, ref SqlParameterDictionary parameters)
        {
            StringBuilder sbSql = new StringBuilder("SELECT ");
            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(type);

            int count = 0;
            /*
            foreach (PropertyInfo prop in columnProperties)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                if (columnAttr != null)
                {
                    sbSql.Append(columnAttr.ColumnName + ", ");
                    count++;
                }
            }*/
            foreach (string key in DatabaseHelper.ToKeys(type, ColumnIoType.Read))
            {
                sbSql.Append(Encap(connectionString, key) + ", ");
                count++;
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 2, 2);

            sbSql.Append(" FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(type)));
            sbSql.Append(" WHERE ");

            sbSql.Append(BuildWhereWithParameters(connectionString, whereParas, ref parameters));
            return sbSql.ToString();
        }
        /// <summary>
        /// 创建查询某类型所有结果的语句
        /// </summary>
        /// <param name="type">要查询的对象类型</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildSelectStringAll(string connectionString, Type type)
        {
            StringBuilder sbSql = new StringBuilder("SELECT * FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(type)));
            return sbSql.ToString();
        }
        /// <summary>
        /// 创建插入语句
        /// </summary>
        /// <param name="entity">相关联的对象</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildInsertSql(string connectionString, object entity)
        {
            StringBuilder sbSql = new StringBuilder("INSERT INTO ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(entity.GetType())));
            sbSql.Append("(");
            ArrayList columnValueList = new ArrayList();
            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(entity.GetType());
            int count = 0;
            /*
            foreach (PropertyInfo prop in columnProperties)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                if (columnAttr != null)
                {
                    sbSql.Append(columnAttr.ColumnName + ", ");
                    columnValueList.Add(ConvertToDbType(entity, prop));
                    count++;
                }
            }*/
            Dictionary<string, object> keyValues = DatabaseHelper.ToKeyValueData(entity, ColumnIoType.Write);
            foreach (string key in keyValues.Keys)
            {
                sbSql.Append(Encap(connectionString, key) + ", ");
                columnValueList.Add(keyValues[key]);
                count++;
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 2, 2);

            sbSql.Append(") VALUES (");

            count = 0;
            for (int i = 0; i < columnValueList.Count; i++)
            {
                sbSql.Append(string.Format("{{0}}, " , i.ToString()));
                count++;
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 2, 2);
            sbSql.Append(")");

            return string.Format(sbSql.ToString(), columnValueList.ToArray());
        }
        /// <summary>
        /// 创建插入命令
        /// </summary>
        /// <param name="entity">相关联的对象</param>
        /// <param name="parameters">用键值对字典表示的命令参数，这是一个引用类型的参数</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildInsertCommand(string connectionString, object entity, ref SqlParameterDictionary parameters)
        {
            StringBuilder sbSql = new StringBuilder("INSERT INTO ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(entity.GetType())));
            sbSql.Append("(");
            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(entity.GetType());
            int count = 0;
            /*
            foreach (PropertyInfo prop in columnProperties)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                if (columnAttr != null)
                {
                    sbSql.Append(columnAttr.ColumnName + ", ");
                    (parameters ?? (parameters = new Dictionary<string, object>())).Add(columnAttr.ColumnName, prop.GetValue(entity, null));
                    count++;
                }
            }*/
            Dictionary<string, object> keyValues = DatabaseHelper.ToKeyValueData(entity, ColumnIoType.Write);
            foreach (string key in keyValues.Keys)
            {
                sbSql.Append(Encap(connectionString, key) + ", ");
                (parameters ?? (parameters = new SqlParameterDictionary())).Add(key, keyValues[key]);
                count++;
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 2, 2);

            sbSql.Append(") VALUES (");

            foreach(string key in keyValues.Keys)
                sbSql.Append(string.Format("{{{0}}}, ", key));
            if (count > 0)
                sbSql.Remove(sbSql.Length - 2, 2);
            sbSql.Append(")");
            return sbSql.ToString();
        }
        /// <summary>
        /// 创建更新语句
        /// </summary>
        /// <param name="entity">相关联的对象</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildUpdateSql(string connectionString, object entity)
        {
            //PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(entity.GetType());
            StringBuilder sbSql = new StringBuilder("UPDATE ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(entity.GetType())));
            sbSql.Append(" SET ");
            ArrayList columnArrayList = new ArrayList();
            int count = 0;

            /*
            foreach (PropertyInfo prop in columnProperties)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                if (columnAttr != null)
                {
                    sbSql.Append(columnAttr.ColumnName + " = {" + count.ToString() + "}, ");
                    columnArrayList.Add(ConvertToDbType(entity, prop));
                    count++;
                }
            }*/
            Dictionary<string, object> keyValues = DatabaseHelper.ToKeyValueData(entity, ColumnIoType.Write);
            foreach (string key in keyValues.Keys)
            {
                sbSql.Append(string.Format("{0} = {{1}}, ", Encap(connectionString, key), count.ToString()));
                columnArrayList.Add(keyValues[key]);
                count++;
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 2, 2);

            sbSql.Append(" WHERE ");
            sbSql.Append(BuildWhereWithPrimary(connectionString, entity));
            return string.Format(sbSql.ToString(), columnArrayList.ToArray());
        }
        /// <summary>
        /// 创建更新命令
        /// </summary>
        /// <param name="entity">相关联的对象</param>
        /// <param name="parameters">用键值对字典表示的命令参数，这是一个引用类型的参数</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildUpdateCommand(string connectionString, object entity, ref SqlParameterDictionary parameters)
        {
            //PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(entity.GetType());
            parameters = new SqlParameterDictionary();
            StringBuilder sbSql = new StringBuilder("UPDATE ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(entity.GetType())));
            sbSql.Append(" SET ");
            int count = 0;
            /*
            foreach (PropertyInfo prop in columnProperties)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                if (columnAttr != null)
                {
                    sbSql.Append(columnAttr.ColumnName + " = ?, ");
                    (parameters ?? (parameters = new Dictionary<string, object>())).Add(columnAttr.ColumnName, prop.GetValue(entity, null));
                    count++;
                }
            }*/
            Dictionary<string, object> keyValues = DatabaseHelper.ToKeyValueData(entity, ColumnIoType.Write);
            foreach (string key in keyValues.Keys)
            {
                sbSql.Append(string.Format("{0} = {{{1}}}, ", Encap(connectionString, key), key));
                parameters.Add(key, keyValues[key]);
                count++;
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 2, 2);

            sbSql.Append(" WHERE ");
            sbSql.Append(BuildWhereWithPrimary(connectionString, entity, ref parameters));
            return sbSql.ToString();
        }

        /// <summary>
        /// 查看类型所有的Ids的Sql
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildSelectIdsSql(string connectionString, Type type)
        {
            StringBuilder sbSql = new StringBuilder("SELECT ");
            int count = 0;
            foreach (PropertyInfo prop in DatabaseHelper.GetPrimaryKeyProperties(type))
            {
                PrimaryKeyAttribute primaryKeyAttr = DatabaseHelper.GetPrimaryKeyAttribute(prop);
                if (primaryKeyAttr != null)
                {
                    sbSql.Append(Encap(connectionString, primaryKeyAttr.ColumnName) + ", ");
                    count++;
                }
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 2, 2);
            sbSql.Append(" FROM ");
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(type)));
            return sbSql.ToString();
        }

        public static string BuildSelectColumnDataSql(string connectionString, Type type, string columnName)
        {
            StringBuilder sbSql = new StringBuilder(string.Format("SELECT {0} FROM ", Encap(connectionString, columnName)));
            sbSql.Append(Encap(connectionString, DatabaseHelper.GetTableName(type)));
            return sbSql.ToString();
        }

        /// <summary>
        /// 创建查看类型指定Where条件的Ids的Sql
        /// </summary>
        /// <param name="type">相关对象的类型</param>
        /// <param name="whereString">sql where 部分</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildSelectIdsSqlWithWhere(string connectionString, Type type, string whereString)
        {
            return string.Format("{0} WHERE {1}", BuildSelectIdsSql(connectionString, type), whereString);
        }

        public static string BuildSelectColumnDataSql(string connectionString, Type type, string columnName, string whereString)
        {
            return string.Format("{0} WHERE {1}", BuildSelectColumnDataSql(connectionString, type, columnName), whereString);
        }

        /// <summary>
        /// 创建查看类型指定Where条件的Ids的Sql命令
        /// </summary>
        /// <param name="type">相关对象的类型</param>
        /// <param name="whereString">参数化的sql where 部分</param>
        /// <param name="whereParas">参数列表</param>
        /// <param name="parameters">用键值对字典表示的命令参数，这是一个引用类型的参数</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql</returns>
        public static string BuildSelectIdsCommandWithWhere(string connectionString, Type type, string whereString, SqlParameterDictionary whereParas, ref SqlParameterDictionary parameters)
        {
            parameters = whereParas;
            return BuildSelectIdsSqlWithWhere(connectionString, type, whereString);
        }

        public static string BuildSelectColumnDataCommandWithWhere(string connectionString, Type type, string columnName, string whereString, SqlParameterDictionary whereParas, ref SqlParameterDictionary parameters)
        {
            parameters = whereParas;
            return BuildSelectColumnDataSql(connectionString, type, whereString);
        }

        /// <summary>
        /// 转换特殊数据库类型的实例到字符串
        /// </summary>
        /// <param name="prop">要转换的属性</param>
        /// <returns>转换后的字符串表示</returns>
        private static object ConvertToDbType(object entity, PropertyInfo prop)
        {
            ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
            if (columnAttr != null)
            {
                switch (columnAttr.DbType)
                {
                    case DBType.Default:
                        //为日期时间和字符串类型，而特性类没有加DBType参数的话按字符串处理
                        if (!prop.PropertyType.Equals(typeof(string)) && !prop.PropertyType.Equals(typeof(DateTime)))
                            return prop.GetValue(entity, null);
                        else
                            return string.Format("'{0}'", Convert.ToString(prop.GetValue(entity, null)));
                    case DBType.Time:
                        string timeStr = Convert.ToDateTime(prop.GetValue(entity, null)).ToString(@"yyyy-MM-dd HH\:mm\:ss");
                        return string.Format("'{0}'", timeStr);
                    case DBType.String:
                        return string.Format("'{0}'", Convert.ToString(prop.GetValue(entity, null)));
                    default:
                        throw new SqlException("No matched Database Type");
                }
            }
            else
            {
                throw new SqlException(string.Format("No ColumnAttribute defined on Property[{0}].", prop.Name));
            }
        }
        /// <summary>
        /// 使用主键创建语句Where部分
        /// </summary>
        /// <param name="entity">相关联的对象</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql where部分</returns>
        private static string BuildWhereWithPrimary(string connectionString, object entity)
        {
            StringBuilder sbSql = new StringBuilder();
            int count = 0;

            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(entity.GetType());
            foreach (PropertyInfo prop in columnProperties)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                if (columnAttr is PrimaryKeyAttribute)
                {
                    sbSql.Append(Encap(connectionString, columnAttr.ColumnName) + "=");
                    sbSql.Append(ConvertToDbType(entity, prop));
                    sbSql.Append(" AND ");
                    count++;
                }
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 5, 5);
            return sbSql.ToString();
        }
        public static string BuildWhereWithPrimary(string connectionString, Type type, SqlParameterDictionary primaryKeyParas)
        {
            StringBuilder sbSql = new StringBuilder();
            int count = 0;

            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(type);
            foreach (PropertyInfo prop in columnProperties)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                if (columnAttr is PrimaryKeyAttribute)
                {
                    sbSql.Append(Encap(connectionString, columnAttr.ColumnName) + "=");
                    sbSql.Append(primaryKeyParas[columnAttr.ColumnName]);
                    sbSql.Append(" AND ");
                    count++;
                }
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 5, 5);
            return sbSql.ToString();
        }
        /// <summary>
        /// 使用参数列表创建语句Where部分
        /// </summary>
        /// <param name="parameters">参数列表</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql where部分</returns>
        private static string BuildWhereWithParameters(string connectionString, SqlParameterDictionary whereParas)
        {
            StringBuilder sbSql = new StringBuilder();
            int count = 0;
            foreach (string key in whereParas.Keys)
            {
                sbSql.Append(Encap(connectionString, key) + "=");
                sbSql.Append(whereParas[key].ToString());
                sbSql.Append(" AND ");
                count++;
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 5, 5);
            return sbSql.ToString();
        }
        /// <summary>
        /// 使用主键创建命令Where部分
        /// </summary>
        /// <param name="entity">相关联的对象</param>
        /// <param name="parameters">用键值对字典表示的命令参数，这是一个引用类型的参数</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql where部分</returns>
        private static string BuildWhereWithPrimary(string connectionString, object entity, ref SqlParameterDictionary parameters)
        {
            StringBuilder sbSql = new StringBuilder();
            int count = 0;

            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(entity.GetType());
            foreach (PropertyInfo prop in columnProperties)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                if (columnAttr is PrimaryKeyAttribute)
                {
                    string columnName = columnAttr.ColumnName ?? prop.Name;
                    sbSql.Append(string.Format("{0} = {{{1}}}", Encap(connectionString, columnName), columnName));
                    if (parameters == null)
                        parameters = new SqlParameterDictionary();
                    if (!parameters.ContainsKey(columnName))
                        parameters.Add(columnName, prop.GetValue(entity, null));
                    sbSql.Append(" AND ");
                    count++;
                }
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 5, 5);
            return sbSql.ToString();
        }
        private static string BuildWhereWithPrimary(string connectionString, Type type, SqlParameterDictionary primaryKeyParas, ref Dictionary<string, object> parameters)
        {
            StringBuilder sbSql = new StringBuilder();
            int count = 0;

            PropertyInfo[] columnProperties = DatabaseHelper.GetColumnProperties(type);
            foreach (PropertyInfo prop in columnProperties)
            {
                ColumnAttribute columnAttr = DatabaseHelper.GetColumnAttribute(prop);
                if (columnAttr is PrimaryKeyAttribute)
                {
                    sbSql.Append(string.Format("{0}={{{1}}}", Encap(connectionString, columnAttr.ColumnName), columnAttr.ColumnName));
                    if (parameters == null)
                        parameters = new SqlParameterDictionary();
                    if (!parameters.ContainsKey(columnAttr.ColumnName))
                        parameters.Add(columnAttr.ColumnName, primaryKeyParas[columnAttr.ColumnName]);
                    sbSql.Append(" AND ");
                    count++;
                }
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 5, 5);
            return sbSql.ToString();
        }
        /// <summary>
        /// 使用参数列表创建命令Where部分
        /// </summary>
        /// <param name="whereParas">参数列表</param>
        /// <param name="parameters">用键值对字典表示的命令参数，这是一个引用类型的参数</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>sql where部分</returns>
        private static string BuildWhereWithParameters(string connectionString, SqlParameterDictionary whereParas, ref SqlParameterDictionary parameters)
        {
            StringBuilder sbSql = new StringBuilder();
            int count = 0;

            foreach (string key in whereParas.Keys)
            {
                sbSql.Append(string.Format("{0} = {{{1}}}", Encap(connectionString, key), key)); 
                if (parameters == null)
                    parameters = new SqlParameterDictionary();
                string colName = key;
                int i = 0;
                while (parameters.ContainsKey(colName))
                    colName += i++;
                parameters.Add(colName, whereParas[key]);
                sbSql.Append(" AND ");
                count++;
            }
            if (count > 0)
                sbSql.Remove(sbSql.Length - 5, 5);
            return sbSql.ToString();
        }
        
        /// <summary>
        /// 使用参数化Where语句创建命令Where部分
        /// </summary>
        /// <param name="whereString">whereString</param>
        /// <param name="whereParas">条件参数</param>
        /// <param name="parameters">用键值对字典表示的命令参数，这是一个引用类型的参数</param>
        /// <returns>sql where部分</returns>
        private static string BuildWhereWithParameteredSqlString(string whereString, SqlParameterDictionary whereParas, ref Dictionary<string, object> parameters)
        {
            return whereString;
        }

        /// <summary>
        /// OledbCommand中的sql使用非命名的?，此方法用于转换，在扩展中使用
        /// </summary>
        /// <param name="sql">命名参数的sql</param>
        /// <param name="parameters">用键值对字典表示的命令参数，这是一个引用类型的参数</param>
        /// <returns>sql</returns>
        private static string ConvertNamedParametersToUnnamedParameters(string sql, ref SqlParameterDictionary parameters)
        {
            //TODO
            //用正则表达式寻找&para形式的word，用?替换，同时对parameters的次序重构，进行必要的重复
            return null;
        }
    }
}
