#define CONCURRENT
//  原来的数据库访问设计是串行的, 应该是并行的以提高异步查询性能, 据说反复new Connection不怎么影响性能
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using UniGuy.Core.Data;
using System.Diagnostics;

namespace UniGuy.Core.Data
{
    [Obsolete("请使用UniGuy.Core.Data.Database.GeneralDatabase")]
    public class Database
    {
        #region Basic
        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static bool Test(OleDbConnection conn)
        {
            if (conn == null)
                return false;

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                return true;
            }
            catch (OleDbException ex)
            {
                OnDatabaseException(ex);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        #endregion

        #region Fields
        /// <summary>
        /// OleDb连接列表
        /// </summary>
        private static Dictionary<string, OleDbConnection> conns;

#if !CONCURRENT
        private static readonly object _syncRoot = new object();
#endif
        #endregion

        #region Properties
        /// <summary>
        /// 获得或者设置OleDb连接列表
        /// </summary>
        public static Dictionary<string, OleDbConnection> Conns
        {
            get { return conns; }
            set { conns = value; }
        }
        #endregion

        #region Delegates
        /// <summary>
        /// 定义数据库异常委托
        /// </summary>
        /// <param name="ex"></param>
        public delegate void DatabaseExceptionDelegate(OleDbException ex, OleDbCommand command);
        /// <summary>
        /// 数据库异常委托方法
        /// </summary>
        public static DatabaseExceptionDelegate DatabaseExceptionHandler;

        /// <summary>
        /// 定义数据库字段包装委托
        /// </summary>
        /// <param name="fieldName"></param>
        public delegate void NameEncapsulationDelegate(string connectionString, ref string fieldName);
        /// <summary>
        /// 数据库字段包装委托处理方法
        /// </summary>
        public static NameEncapsulationDelegate NameEncapsulationHandler;

        /// <summary>
        /// 定义数据库字段类型通用描述枚举转换为特定字段类型名称委托
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="dbt">数据库字段类型通用描述枚举</param>
        /// <param name="typeName">特定字段类型名称</param>
        public delegate void SpecialDbTypeToTypeNameDelegate(string connectionString, DBType dbt, ref string typeName, ref bool needSize);
        /// <summary>
        /// 数据库字段类型通用描述枚举转换为特定字段类型名称委托处理方法
        /// </summary>
        public static SpecialDbTypeToTypeNameDelegate SpecialDbTypeToTypeNameHandler;
        #endregion

        #region Ctor
        /// <summary>
        /// 静态构造器
        /// </summary>
        static Database()
        {
            //  设置默认数据库字段包装委托处理方法
            NameEncapsulationHandler = new NameEncapsulationDelegate(delegate(string connectionString, ref string fieldName)
            {
                /*
                OleDbConnection conn = GetConnection(connectionString);
                if (conn != null)
                {
                    switch (conn.Provider)
                    {
                        case "SQLOLEDB":
                                fieldName = string.Format("[{0}]", fieldName);
                                break;
                        case "OraOLEDB.Oracle.1":
                            //  TODO
                            break;
                        //  TODO
                        default:
                            break;
                    }
                }*/
            });
            //  设置默认数据库字段类型通用描述枚举转换为特定字段类型名称委托处理方法
            SpecialDbTypeToTypeNameHandler = new SpecialDbTypeToTypeNameDelegate(delegate(string connectionString, DBType dbt, ref string typeName, ref bool needSize)
            {
                OleDbConnection conn = GetConnection(connectionString);
                if (conn != null)
                {
                    switch (conn.Provider)
                    {
                        case "SQLOLEDB":
                            needSize = false;
                            switch (dbt)
                            {
                                case DBType.Date:
                                case DBType.Time:
                                    typeName = "datetime";
                                    break;
                                case DBType.Int32:
                                    typeName = "int";
                                    break;
                                case DBType.Single:
                                    typeName = "float";
                                    break;
                                case DBType.Decimal:
                                case DBType.String:
                                case DBType.Default:
                                default:
                                    typeName = "varchar";
                                    needSize = true;
                                    break;
                            }
                            break;
                        case "OraOLEDB.Oracle.1":
                            needSize = false;
                            switch (dbt)
                            {
                                case DBType.Date:
                                case DBType.Time:
                                case DBType.Int32:
                                case DBType.Single:
                                case DBType.Decimal:
                                case DBType.String:
                                case DBType.Default:
                                default:
                                    typeName = "varchar2";
                                    needSize = true;
                                    break;
                            }
                            //  TODO
                            break;
                        //  TODO
                        default:
                            break;
                    }
                }
            });
        }
        #endregion

        #region Methods
        #region Common
        /// <summary>
        /// 获得连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
#if CONCURRENT
        public static OleDbConnection GetConnection(string connectionString)
        {
            try
            {
                return new OleDbConnection(connectionString);
            }
            catch (OleDbException ex)
            {
                OnDatabaseException(ex);
                return null;
            }
        }
#else
        public static OleDbConnection GetConnection(string connectionString)
        {
            lock (_syncRoot)    //  使用了共享资源conn
            {
                if (conns == null)
                    conns = new Dictionary<string, OleDbConnection>();
                OleDbConnection connection = null;
                if (conns.ContainsKey(connectionString))
                {
                    connection = conns[connectionString];
                    Debug.Assert(connection.State == ConnectionState.Closed);
                }
                else
                {
                    try
                    {
                        conns.Add(connectionString, connection = new OleDbConnection(connectionString));
                    }
                    catch (OleDbException ex)
                    {
                        OnDatabaseException(ex);
                        return null;
                    }
                }
                return connection;
            }
        }
#endif

        /// <summary>
        /// 获得提供程序名称
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static string GetProvider(string connectionString)
        {
            OleDbConnection conn = GetConnection(connectionString);
            return conn == null ? null : conn.Provider;
        }

        /// <summary>
        /// 获得是否连接成功
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool IsConnected(string connectionString)
        {
            return Test(GetConnection(connectionString));
        }
        /// <summary>
        /// 创建命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// Deprecated, 原来的写法, sql中的参数格式需要用"?"表示
        [Obsolete]
        public static OleDbCommand CreateCommand2(string connectionString, string sql, SqlParameterDictionary parameters)
        {
            OleDbCommand comm = new OleDbCommand();
            comm.Connection = GetConnection(connectionString);
            comm.CommandText = sql;
            if (parameters != null)
                foreach (string key in parameters.Keys)
                    comm.Parameters.Add(new OleDbParameter(key, parameters[key] ?? DBNull.Value));  //  如果null不转换为DBNull.Value, 会使用Default, 也就是数据库该列设定的默认值
            return comm;
        }

        private static void SqlizeCommand(OleDbCommand comm, string sql, SqlParameterDictionary parameters)
        {
            int lastCommentPos = sql.IndexOf("--");
            int s = 0;
        odc0:
            s = sql.IndexOf("{", s);

            if (s >= 0)
            {
                //  这个部分判断是否参数在注释中, 如果在则跳过;
                if (lastCommentPos != -1 && s > lastCommentPos)
                {
                    int lastNewLine = sql.IndexOfAny(new char[] { '\r', '\n' }, lastCommentPos);
                    if (lastNewLine == -1)
                        goto odc1;
                    lastCommentPos = sql.IndexOf("--", lastNewLine);
                    if (s < lastNewLine)
                    {
                        s = lastNewLine;
                        goto odc0;
                    }
                }

                int e = sql.IndexOf("}", s);
                if (e >= 0)
                {
                    string key = sql.Substring(s + 1, e - s - 1);
                    //  其实还应该判断key命名是否符合参数命名规范 TODO
                    if (!parameters.ContainsKey(key))
                        throw new Exception("Key not found in parameters");
                    comm.Parameters.Add(new OleDbParameter(key, parameters[key] ?? DBNull.Value));  //  如果null不转换为DBNull.Value, 会使用Default, 也就是数据库该列设定的默认值
                    sql = sql.Remove(s, e - s + 1);
                    sql = sql.Insert(s, "?");
                    //  调整lastCommentPos位置
                    if (lastCommentPos != -1)
                        lastCommentPos -= e - s;
                    s++;
                    goto odc0;
                }
            }
        odc1:
            comm.CommandText = sql;
        }
        /// <summary>
        /// 创建命令,上面方法的替代方法,这里的Sql需要用{arg}来取代上面的?,并且parameters会自动扩展，暂时未用,需要的时候调整上去
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// 参数用"{参数名}"的格式表示, sql中的参数必须是parameters中存在的key; 但问题是如果sql中包含sql注释"--"开头, 则该行注释后面的参数不应该被替换
        public static OleDbCommand CreateCommand(string connectionString, string sql, SqlParameterDictionary parameters)
        {
            OleDbCommand comm = new OleDbCommand();
            comm.Connection = GetConnection(connectionString);

            SqlizeCommand(comm, sql, parameters);
            return comm;
        }
        public static OleDbCommand CreateCommand(OleDbTransaction tran, string sql, SqlParameterDictionary parameters)
        {
            OleDbCommand comm = new OleDbCommand{Connection = tran.Connection, Transaction = tran};

            SqlizeCommand(comm, sql, parameters);
            return comm;
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        /// <example>
        /// using(IDbTransaction tran = Database.BeginTransaction(...))
        /// {
        ///     Other Nonquery methods with transaction set...
        ///     if(!Database.CommitTransaction(tran))
        ///         Database.RollbackTransaction(tran);
        /// }
        /// </example>
        public static OleDbTransaction BeginTransaction(string connectionString, out string message)
        {
            message = null;
            OleDbConnection conn = GetConnection(connectionString);
#if !CONCURRENT
            lock (comm.Connection) // 避免共享资源commConnection冲突
            {
#endif
            try
            {
                return conn.BeginTransaction();
            }
            catch (OleDbException ex)
            {
                message = ex.Message;
                return null;
            }
#if !CONCURRENT
            }
#endif
        }
        public static OleDbTransaction BeginTransaction(string connectionString)
        {
            string message;
            return BeginTransaction(connectionString, out message);
        }

        public static bool CommitTransaction(OleDbTransaction tran, out string message)
        {
            message = null;
#if !CONCURRENT
            lock (comm.Connection) // 避免共享资源commConnection冲突
            {
#endif
            try
            {
                tran.Commit();
                return true;
            }
            catch (OleDbException ex)
            {
                message = ex.Message;
                return false;
            }
#if !CONCURRENT
            }
#endif
        }
        public static bool CommitTransaction(OleDbTransaction tran)
        {
            string message;
            return CommitTransaction(tran, out message);
        }

        public static bool RollbackTransaction(OleDbTransaction tran, out string message)
        {
            message = null;
#if !CONCURRENT
            lock (comm.Connection) // 避免共享资源commConnection冲突
            {
#endif
            try
            {
                tran.Rollback();
                return true;
            }
            catch (OleDbException ex)
            {
                message = ex.Message;
                return false;
            }
#if !CONCURRENT
            }
#endif
        }
        public static bool RollbackTransaction(OleDbTransaction tran)
        {
            string message;
            return RollbackTransaction(tran, out message);
        }

        /// <summary>
        /// 类似Select，不过返回IDataReader, 可能能降低空间复杂度
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <remarks>
        /// <example>
        /// using(IDataReader = Database.Read(...))
        /// {
        ///     //  TODO
        /// }
        /// </example>
        /// </remarks>
        public static IDataReader Read(string connectionString, string sql, SqlParameterDictionary parameters, out string message)
        {
            message = null;

            DataSet resultDataSet = new DataSet();
            using (OleDbCommand comm = CreateCommand(connectionString, sql, parameters))
            {
#if !CONCURRENT
                lock (comm.Connection) // 避免共享资源commConnection冲突
                {
#endif
                try
                {
                    if (comm.Connection.State == ConnectionState.Closed)
                        comm.Connection.Open();

                    return comm.ExecuteReader();
                }
                catch (OleDbException ex)
                {
                    message = ex.Message;
                    OnDatabaseException(ex, comm);
                    return null;
                }
                finally
                {
                    if (comm.Connection.State == ConnectionState.Open)
                        comm.Connection.Close();
                }
#if !CONCURRENT
                }
#endif
            }
        }
        public static IDataReader Read(string connectionString, string sql, SqlParameterDictionary parameters)
        {
            string message;
            return Read(connectionString, sql, parameters, out message);
        }

        /// <summary>执行查询语句</summary>
        /// <param name="strSql">sql</param>
        /// <returns>数据集</returns>
        public static DataSet Select(string connectionString, string sql, SqlParameterDictionary parameters, out string message)
        {
            message = null;

            DataSet resultDataSet = new DataSet();
            using (OleDbCommand comm = CreateCommand(connectionString, sql, parameters))
            {
#if !CONCURRENT
                lock (comm.Connection) // 避免共享资源commConnection冲突
                {
#endif
                try
                {
                    if (comm.Connection.State == ConnectionState.Closed)
                        comm.Connection.Open();

                    new OleDbDataAdapter(comm).Fill(resultDataSet);
                    return resultDataSet;
                }
                catch (OleDbException ex)
                {
                    message = ex.Message;
                    OnDatabaseException(ex, comm);
                    return null;
                }
                finally
                {
                    if (comm.Connection.State == ConnectionState.Open)
                        comm.Connection.Close();
                }
#if !CONCURRENT
                }
#endif
            }
        }

        public static DataSet Select(string connectionString, string sql, SqlParameterDictionary parameters)
        {
            string message;
            return Select(connectionString, sql, parameters, out message);
        }
        
        /// <summary>执行返回单一值的查询语句</summary>
        /// <param name="strSql">sql</param>
        /// <returns>数据集</returns>
        public static object Scalar(string connectionString, string sql, SqlParameterDictionary parameters, out string message)
        {
            message = null;

            using (OleDbCommand comm = CreateCommand(connectionString, sql, parameters))
            {
#if !CONCURRENT
                lock (comm.Connection) // 避免共享资源commConnection冲突
                {
#endif
                    try
                    {
                        if (comm.Connection.State == ConnectionState.Closed)
                            comm.Connection.Open();
                        return comm.ExecuteScalar();
                    }
                    catch (OleDbException ex)
                    {
                        message = ex.Message;
                        OnDatabaseException(ex, comm);
                        return null;
                    }
                    finally
                    {
                        if (comm.Connection.State == ConnectionState.Open)
                            comm.Connection.Close();
                    }
#if !CONCURRENT
                }
#endif
            }
        }

        public static object Scalar(string connectionString, string sql, SqlParameterDictionary parameters)
        {
            string message;
            return Scalar(connectionString, sql, parameters, out message);
        }
        
        public static int NonQuery(OleDbTransaction tran, string sql, SqlParameterDictionary parameters, out string message)
        {
            message = null;

            using (OleDbCommand comm = CreateCommand(tran, sql, parameters))
            {
#if !CONCURRENT
                lock (comm.Connection) // 避免共享资源commConnection冲突
                {
#endif
                    try
                    {
                        if (comm.Connection.State == ConnectionState.Closed)
                            comm.Connection.Open();

                        int i = comm.ExecuteNonQuery();
                        return i;
                    }
                    catch (OleDbException ex)
                    {
                        message = ex.Message;
                        OnDatabaseException(ex, comm);
                        return -1;
                    }
                    finally
                    {
                        if (comm.Connection.State == ConnectionState.Open)
                            comm.Connection.Close();
                    }
#if !CONCURRENT
                }
#endif
            }
        }
        public static int NonQuery(OleDbTransaction tran, string sql, SqlParameterDictionary parameters)
        {
            string message = null;
            return NonQuery(tran, sql, parameters, out message);
        }

        /// <summary>执行普通Sql命令</summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns>改变行数</returns>
        public static int NonQuery(string connectionString, string sql, SqlParameterDictionary parameters, out string message)
        {
            message = null;

            using (OleDbCommand comm = CreateCommand(connectionString, sql, parameters))
            {
#if !CONCURRENT
                lock (comm.Connection) // 避免共享资源commConnection冲突
                {
#endif
                    try
                    {
                        if (comm.Connection.State == ConnectionState.Closed)
                            comm.Connection.Open();

                        int i = comm.ExecuteNonQuery();
                        return i;
                    }
                    catch (OleDbException ex)
                    {
                        message = ex.Message;
                        OnDatabaseException(ex, comm);
                        return -1;
                    }
                    finally
                    {
                        if (comm.Connection.State == ConnectionState.Open)
                            comm.Connection.Close();
                    }
#if !CONCURRENT
                }
#endif
            }
        }
        public static int NonQuery(string connectionString, string sql, SqlParameterDictionary parameters)
        {
            string message = null;
            return NonQuery(connectionString, sql, parameters, out message);
        }
        #endregion

        #region Delegate Invokers
        /// <summary>
        /// 数据库异常委托调用器
        /// </summary>
        /// <param name="ex">数据库异常</param>
        public static void OnDatabaseException(OleDbException ex)
        {
            OnDatabaseException(ex, null);
        }
        /// <summary>
        /// 数据库异常委托调用器,和原来相比新增了Command参数,这样外部捕获代码可以直接拿到sql语句等信息,方便外部调试
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="command"></param>
        public static void OnDatabaseException(OleDbException ex, OleDbCommand command)
        {
            if (DatabaseExceptionHandler != null)
                DatabaseExceptionHandler(ex, command);
        }
        /// <summary>
        /// 数据库字段包装委托调用器
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="fieldName">数据库字段名</param>
        /// <returns>包装后的数据库字段</returns>
        public static void OnNameEncapsulate(string connectionString, ref string fieldName)
        {
            if (NameEncapsulationHandler != null)
                NameEncapsulationHandler(connectionString, ref fieldName);
        }
        /// <summary>
        /// 数据类型通用到数据库特异转换委托调用器
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="dbt">通用数据类型枚举</param>
        /// <param name="typeName">数据库特异数据类型描述字符串</param>
        public static void OnSpecialDbTypeToTypeName(string connectionString, DBType dbt, ref string typeName, ref bool needSize)
        {
            if (SpecialDbTypeToTypeNameHandler != null)
                SpecialDbTypeToTypeNameHandler(connectionString, dbt, ref typeName, ref needSize);
        }
        #endregion
        #endregion
    }
}
