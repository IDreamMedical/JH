using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
#if DOT_NET_35
using System.Linq;
#endif
using UniGuy.Core.Extensions;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// Oracle数据库表调整器
    /// </summary>
    public class OracleDatabaseTableAdjuster : DatabaseTableAdjusterBase
    {
        #region Fields
        /// <summary>
        /// 数据库
        /// </summary>
        /// <remarks>在UniGuy.Sql中可以实现另外一套, 不过这里应该是OracleSqlProvider. TODO</remarks>
        private IDatabase _database;
        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <remarks>这里需要连接字符串, 因为Database是一类库, 并不包含连接字符串信息, 这样设计是否好还难说, 也许Database应该包含连接字符串</remarks>
        private string _connectionString;

        /// <summary>
        /// 默认列长度
        /// </summary>
        private int _defaultColumnLength = 100;

        /// <summary>
        /// 对象名称是否大小写敏感
        /// </summary>
        private bool caseSensitive;
        #endregion

        #region Properties
        /// <summary>
        /// 获得或者设置数据库
        /// </summary>
        public IDatabase Database
        {
            get { return _database; }
            set { _database = value; }
        }
        /// <summary>
        /// 获得或者设置连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// 获得或者设置默认列长度
        /// </summary>
        public int DefaultColumnLength
        {
            get { return _defaultColumnLength; }
            set { _defaultColumnLength = value; }
        }

        /// <summary>
        /// 获得或者设置对象名称是否大小写敏感
        /// </summary>
        public bool CaseSensitive
        {
            get { return caseSensitive; }
            set { caseSensitive = value; }
        }
        #endregion

        #region Constructors
        public OracleDatabaseTableAdjuster() { }
        public OracleDatabaseTableAdjuster(IDatabase database) { this._database = database; }
        public OracleDatabaseTableAdjuster(IDatabase database, string connectionString) : this(database) { _connectionString = connectionString; }
        #endregion

        #region Methods

        #region Internal

        /// <summary>
        /// 添加一列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnDefinition"></param>
        private void AddColumn(string tableName, DataColumnDefinition columnDefinition)
        {
            string sql = string.Format("ALTER table {0} ADD {1} varchar({2}) {3}NULL",
                CaseSensitive ? "\"" + tableName + "\"" : tableName,
                CaseSensitive ? "\"" + columnDefinition.ColumnName + "\"" : columnDefinition.ColumnName,
                columnDefinition.Length > 0 ? columnDefinition.Length : DefaultColumnLength,
                columnDefinition.IsNullable ? "" : "NOT ");
            Database.NonQuery(ConnectionString, sql, null);
        }

        /// <summary>
        /// 删除一列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        private void DeleteColumn(string tableName, string columnName)
        {
            string sql = string.Format("ALTER table {0} DROP column {1}",
                CaseSensitive ? "\"" + tableName + "\"" : tableName,
                CaseSensitive ? "\"" + columnName + "\"" : columnName);
            Database.NonQuery(ConnectionString, sql, null);
        }

        /// <summary>
        /// 更改一列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnDefinition"></param>
        private void AlterColumn(string tableName, DataColumnDefinition columnDefinition)
        {
            string sql = string.Format("ALTER table {0} ALTER column {1} varchar({2}) {3}NULL",
                CaseSensitive ? "\"" + tableName + "\"" : tableName,
                CaseSensitive ? "\"" + columnDefinition.ColumnName + "\"" : columnDefinition.ColumnName,
                columnDefinition.Length > 0 ? columnDefinition.Length : DefaultColumnLength,
                columnDefinition.IsNullable ? "" : "NOT ");
            Database.NonQuery(ConnectionString, sql, null);
        }

        /// <summary>
        /// 获得表的所有列定义
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private List<DataColumnDefinition> GetColumnDefinitions(string tableName)
        {
            string sql = CaseSensitive?"SELECT column_name, data_length, nullable FROM user_tab_columns WHERE table_name={TableName}":
                "SELECT column_name, data_length, isnullable FROM user_tab_columns WHERE table_name=upper({TableName})";
            SqlParameterDictionary parameters = new SqlParameterDictionary();
            parameters.Add("TableName", tableName);
            DataSet ds = Database.Select(ConnectionString, sql, parameters);
            System.Diagnostics.Debug.Assert(ds != null && ds.Tables.Count > 0);
            DataTable dt = ds.Tables[0];
            List<DataColumnDefinition> columnDefinitions = new List<DataColumnDefinition>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                System.Diagnostics.Debug.Assert(!(dt.Rows[i][0] is DBNull || dt.Rows[i][1] is DBNull || dt.Rows[i][2] is DBNull));
                columnDefinitions.Add(new DataColumnDefinition((string)dt.Rows[i][0], Convert.ToInt32(dt.Rows[i][1]), Convert.ToInt32(dt.Rows[i][2]) == 1));

            }
            return columnDefinitions;
        }

        #endregion

        #region General

        public override bool ExistsTable(string tableName)
        {
            System.Diagnostics.Debug.Assert(Database != null && ConnectionString != null);
            string sql = CaseSensitive?"SELECT Count(*) FROM user_tables WHERE table_name={TableName}"
                : "SELECT Count(*) FROM user_tables WHERE table_name=upper({TableName})";
            SqlParameterDictionary parameters = new SqlParameterDictionary();
            parameters.Add("TableName", tableName);
            return Convert.ToInt32(Database.Scalar(ConnectionString, sql, parameters)) == 1;
        }

        public override void CreateTable(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions)
        {
            System.Diagnostics.Debug.Assert(Database != null && !ExistsTable(tableName));

#if DOT_NET_35
            if (columnDefinitions != null && columnDefinitions.Count() > 0)
#else
            if (columnDefinitions != null && new List<DataColumnDefinition>(columnDefinitions).Count > 0)
#endif
                throw new ArgumentException("Can not be null or contains 0 item", "columnDefinitions");
            StringBuilder sbSql = new StringBuilder().AppendFormat("CREATE TABLE {0} (", tableName);
            foreach (DataColumnDefinition columnDefinition in columnDefinitions)
            {
                sbSql.AppendFormat("{0} varchar({1}) {2}NULL, ",
                    CaseSensitive ? "\"" + columnDefinition.ColumnName + "\"" : columnDefinition.ColumnName,
                    columnDefinition.Length > 0 ? columnDefinition.Length : DefaultColumnLength,
                    columnDefinition.IsNullable ? "" : "NOT ");
            }
            sbSql.Remove(sbSql.Length - 2, 2).Append(")");

            Database.NonQuery(ConnectionString, sbSql.ToString(), null);
        }

        public override void ModifyTable(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions)
        {
            System.Diagnostics.Debug.Assert(Database != null && ExistsTable(tableName));

            List<DataColumnDefinition> oldColumnDefinitions = GetColumnDefinitions(tableName);

            foreach (DataColumnDefinition oldColumnDefinition in oldColumnDefinitions)
            {
#if DOT_NET_35
                DataColumnDefinition columnDefinition = columnDefinitions.SingleOrDefault(a => a.NameEquals(oldColumnDefinition, CaseSensitive));
                if (columnDefinition.IsDefault())
                {
                    //  去除不再有的列
                    DeleteColumn(tableName, oldColumnDefinition.ColumnName);
                }
#else
                DataColumnDefinition columnDefinition = default(DataColumnDefinition);
                foreach (DataColumnDefinition dcd in columnDefinitions)
                {
                    if (dcd.NameEquals(oldColumnDefinition, CaseSensitive))
                    {
                        columnDefinition = dcd;
                        break;
                    }
                }
                if (columnDefinition==default(DataColumnDefinition))
                {
                    //  去除不再有的列
                    DeleteColumn(tableName, oldColumnDefinition.ColumnName);
                }
#endif
                else
                {
                    //  修改其他的列
                    if (oldColumnDefinition != columnDefinition)
                        AlterColumn(tableName, columnDefinition);
                }
            }
            foreach (DataColumnDefinition columnDefinition in columnDefinitions)
            {
#if DOT_NET_35
                DataColumnDefinition oldColumnDefinition = oldColumnDefinitions.SingleOrDefault(a => a.NameEquals(columnDefinition, CaseSensitive));
#else
                DataColumnDefinition oldColumnDefinition = default(DataColumnDefinition);
                foreach (DataColumnDefinition dcd in oldColumnDefinitions)
                {
                    if (dcd.NameEquals(columnDefinition, CaseSensitive))
                    {
                        oldColumnDefinition = dcd;
                        break;
                    }
                }
#endif
                if (oldColumnDefinition == default(DataColumnDefinition))
                {
                    //  添加新增的列
                    AddColumn(tableName, columnDefinition);
                }
            }
        }

        public override void AppendModifyTable(string tableName, IEnumerable<DataColumnDefinition> columnDefinitions)
        {
            System.Diagnostics.Debug.Assert(Database != null && ExistsTable(tableName));

            List<DataColumnDefinition> oldColumnDefinitions = GetColumnDefinitions(tableName);

            foreach (DataColumnDefinition oldColumnDefinition in oldColumnDefinitions)
            {
#if DOT_NET_35
                DataColumnDefinition columnDefinition = columnDefinitions.SingleOrDefault(a => a.NameEquals(oldColumnDefinition, CaseSensitive));
#else
                DataColumnDefinition columnDefinition = default(DataColumnDefinition);
                foreach (DataColumnDefinition dcd in columnDefinitions)
                {
                    if (dcd.NameEquals(columnDefinition, CaseSensitive))
                    {
                        columnDefinition = dcd;
                        break;
                    }
                }
#endif
                if (columnDefinition != default(DataColumnDefinition))
                {
                    //  修改其他的列
                    if (oldColumnDefinition != columnDefinition)
                    {
                        if (columnDefinition.Length > oldColumnDefinition.Length || columnDefinition.IsNullable != oldColumnDefinition.IsNullable)
                            AlterColumn(tableName, columnDefinition);
                    }
                }
            }
            foreach (DataColumnDefinition columnDefinition in columnDefinitions)
            {
#if DOT_NET_35
                DataColumnDefinition oldColumnDefinition = oldColumnDefinitions.SingleOrDefault(a => a.NameEquals(columnDefinition, CaseSensitive));
#else
                DataColumnDefinition oldColumnDefinition = default(DataColumnDefinition);
                foreach (DataColumnDefinition dcd in oldColumnDefinitions)
                {
                    if (dcd.NameEquals(columnDefinition, CaseSensitive))
                    {
                        oldColumnDefinition = dcd;
                        break;
                    }
                }
#endif
                if (oldColumnDefinition == default(DataColumnDefinition))
                {
                    //  添加新增的列
                    AddColumn(tableName, columnDefinition);
                }
            }
        }
        #endregion

        #endregion
    }
}
