using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// 数据库代理访问器
    /// </summary>
    /// <remarks>
    /// 如果直接使用GeneralDatabase, 那么只能执行一些基本的底层方法, 也就是Select, Scalar和NonQuery;
    /// 这个类是对GeneralDatabase的封装, 支持一些面向业务的基本方法
    /// </remarks>
    public class DatabaseProxy
    {
        #region Fields
        /// <summary>
        /// 数据库
        /// </summary>
        private IDatabase _database;
        #endregion //   Fields

        #region Properties
        /// <summary>
        /// 获得或者设置通用数据库
        /// </summary>
        public IDatabase Database
        {
            get { return _database; }
            set { _database = value; }
        }
        #endregion //   Properties

        #region Constructors
        public DatabaseProxy(IDatabase database)
        {
            this._database = database;
        }
        #endregion //   Constructors

        #region Methods

        #region Basic
        #endregion //   Basic

        #region General
        /// <summary>
        /// 获得是否能和数据库建立连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public bool IsConnected(string connectionString)
        {
            return Database.IsConnected(connectionString);
        }

        /// <summary>
        /// 获得指定类型的Id列表
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public List<string> GetIds(string connectionString, Type t)
        {
            string sql = SqlBuilder.BuildSelectIdsSql(connectionString, t);
            DataSet ds = Database.Select(connectionString, sql, null);
            //  查询成功
            if (ds != null)
            {
                List<string> ids = new List<string>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string id = null;
                    foreach (DataColumn col in ds.Tables[0].Columns)
                        id += row[col].ToString();
                    ids.Add(id);
                }
                return ids;
            }
            // 查询失败
            else
            {
                return null;
                // TODO: If failed , then log...
            }
        }
        public List<string> GetColumnData(string connectionString, Type t, string columnName)
        {
            string sql = SqlBuilder.BuildSelectColumnDataSql(connectionString, t, columnName);
            DataSet ds = Database.Select(connectionString, sql, null);
            //  查询成功
            if (ds != null)
            {
                List<string> columnData = new List<string>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string datum = null;
                    foreach (DataColumn col in ds.Tables[0].Columns)
                        datum += row[col].ToString();
                    columnData.Add(datum);
                }
                return columnData;
            }
            // 查询失败
            else
            {
                return null;
                // TODO: If failed , then log...
            }
        }
        /// <summary>
        /// 获得指定类型，使用指定条件参数的Id列表
        /// </summary>
        /// <param name="t"></param>
        /// <param name="whereString"></param>
        /// <param name="whereParas"></param>
        /// <returns></returns>
        public string[] GetIdsWithWhere(string connectionString, Type t, string whereString, SqlParameterDictionary whereParas)
        {
            SqlParameterDictionary parameters = null;
            string sql = SqlBuilder.BuildSelectIdsCommandWithWhere(connectionString, t, whereString, whereParas, ref parameters);
            DataSet ds = Database.Select(connectionString, sql, parameters);
            //  查询成功
            if (ds != null)
            {
                List<string> ids = new List<string>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string id = null;
                    foreach (DataColumn col in ds.Tables[0].Columns)
                        id = row[col].ToString();
                    ids.Add(id);
                }
                return ids.ToArray();
            }
            // 查询失败
            else
            {
                return null;
                // TODO: If failed , then log...
            }
        }
        /// <summary>
        /// 判断某个标识的实体是否存在
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="id">实体标识</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        public bool Exists(string connectionString, Type type, string id)
        {
            SqlParameterDictionary parameters = null;
            SqlParameterDictionary paras = new SqlParameterDictionary();
            //  获得唯一标识对应的数据库列名
            string columnName = "Id";
            System.Reflection.PropertyInfo pi = type.GetProperty("Id");
            object[] caAttrs = pi.GetCustomAttributes(typeof(ColumnAttribute), false);
            if (caAttrs.Length > 0)
                columnName = ((ColumnAttribute)caAttrs[0]).ColumnName ?? columnName;

            paras.Add(columnName, id);
            string sql = SqlBuilder.BuildIsExistsCommand(connectionString, type, paras, ref parameters);
            int i = Convert.ToInt32(Database.Scalar(connectionString, sql, parameters));
            return i > 0;
        }

       
        /// <summary>
        /// 判断是否存在和某个实体类对应的数据库表
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>存在则返回True</returns>
        public bool ExistsTable(string connectionString, Type type)
        {
            SqlParameterDictionary parameters = null;
            string sql = SqlBuilder.BuildIsExistsTableCommand(connectionString, type, ref parameters);
            int i = Convert.ToInt32(Database.Scalar(connectionString, sql, parameters));
            return i > 0;
        }
        /// <summary>
        /// 创建和某个实体类型对应的数据库表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="connectionString"></param>
        public void CreateTable(string connectionString, Type type)
        {
            string sql = SqlBuilder.BuildCreateTableSql(connectionString, type);
            Database.NonQuery(connectionString, sql, null);
        }
        /// <summary>
        /// 查找指定类型指定标识的对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="id">对象标识</param>
        /// <returns>对象</returns>
        public IIdObject Select(string connectionString, Type type, string id)
        {
            SqlParameterDictionary parameters = null;
            SqlParameterDictionary paras = new SqlParameterDictionary();

            //  获得唯一标识对应的数据库列名
            string columnName = "Id";
            System.Reflection.PropertyInfo pi = type.GetProperty("Id");
            object[] caAttrs = pi.GetCustomAttributes(typeof(ColumnAttribute), false);
            if (caAttrs.Length > 0)
                columnName = ((ColumnAttribute)caAttrs[0]).ColumnName??columnName;

            paras.Add(columnName, id);
            string sql = SqlBuilder.BuildSelectByWhereCommand(connectionString, type, paras, ref parameters);
            DataSet ds = Database.Select(connectionString, sql, parameters);
            object obj = EntityBuilder.BuildEntity(type, ds);
            return obj as IIdObject;
        }
        /// <summary>
        /// 删除指定类型指定标识的对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="id">对象标识</param>
        /// <returns>删除操作成功与否</returns>
        public bool Delete(string connectionString, Type type, string id)
        {
            SqlParameterDictionary parameters = null;
            SqlParameterDictionary paras = new SqlParameterDictionary();
            //  获得唯一标识对应的数据库列名
            string columnName = "Id";
            System.Reflection.PropertyInfo pi = type.GetProperty("Id");
            object[] caAttrs = pi.GetCustomAttributes(typeof(ColumnAttribute), false);
            if (caAttrs.Length > 0)
                columnName = ((ColumnAttribute)caAttrs[0]).ColumnName ?? columnName;

            paras.Add(columnName, id);
            string sql = SqlBuilder.BuildDeleteCommand(connectionString, type, paras, ref parameters);
            int i = Database.NonQuery(connectionString, sql, parameters);
            return i == 1;
        }
        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>插入操作成功与否</returns>
        public bool Insert(string connectionString, IIdObject obj)
        {
            SqlParameterDictionary parameters = null;
            string sql = SqlBuilder.BuildInsertCommand(connectionString, obj, ref parameters);
            int i = Database.NonQuery(connectionString, sql, parameters);
            return i == 1;
        }
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>更新操作成功与否</returns>
        public bool Update(string connectionString, IIdObject obj)
        {
            SqlParameterDictionary parameters = null;
            string sql = SqlBuilder.BuildUpdateCommand(connectionString, obj, ref parameters);
            int i = Database.NonQuery(connectionString, sql, parameters);
            return i == 1;
        }
        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>保存操作成功与否</returns>
        public bool Save(string connectionString, IIdObject obj)
        {
            //  要保存的实体不能为空
            if (obj == null)
                throw new NullReferenceException("IIdObject to be saved can not be 'null'.");

            Type t = obj.GetType();
            //  如果不存在表则首先建立表
            if (!ExistsTable(connectionString, t))
            {
                CreateTable(connectionString, t);
                return Insert(connectionString, obj);
            }
            //  根据主键判断是否已有该实体，并进行更新或插入操作
            else
            {
                return Exists(connectionString, t, obj.Id) ? Update(connectionString, obj) : Insert(connectionString, obj);
            }
        }
        #endregion //   General

        #endregion //   Methods
    }
}
