#define CONCURRENT
//  原来的数据库访问设计是串行的, 应该是并行的以提高异步查询性能, 据说反复new Connection不怎么影响性能
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Reflection;
using UniGuy.Core.Data;
using System.Diagnostics;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// 基于System.Data.OracleClient的数据库访问辅助类
    /// </summary>
    public class OracleDatabase:IDatabase
    {
        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public bool Test(OracleConnection conn)
        {
            if (conn == null)
                return false;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                return true;
            }
            catch (OracleException ex)
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

        #region Fields
        /// <summary>
        /// Oracle连接列表
        /// </summary>
#if !CONCURRENT
        private Dictionary<string, OracleConnection> conns;
        private static readonly object _syncRoot = new object();
#endif
        #endregion

        #region Properties
        public DatabaseExceptionDelegate DatabaseExceptionHandler { get; set; }
#if !CONCURRENT
        /// <summary>
        /// 获得或者设置Oracle连接列表
        /// </summary>
        public Dictionary<string, OracleConnection> Conns
        {
            get { return conns; }
            set { conns = value; }
        }
#endif
        #endregion

        #region Delegates

        /// <summary>
        /// 定义数据库字段包装委托
        /// </summary>
        /// <param name="fieldName"></param>
        public delegate void NameEncapsulationDelegate(string connectionString, ref string fieldName);
        /// <summary>
        /// 数据库字段包装委托处理方法
        /// </summary>
        public NameEncapsulationDelegate NameEncapsulationHandler;

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
        public SpecialDbTypeToTypeNameDelegate SpecialDbTypeToTypeNameHandler;
        #endregion

        #region Ctor
        /// <summary>
        /// 静态构造器
        /// </summary>
        public OracleDatabase()
        {
           
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
        private OracleConnection GetConnection(string connectionString)
        {
            try
            {
                return new OracleConnection(connectionString);
            }
            catch (OracleException ex)
            {
                OnDatabaseException(ex);
                return null;
            }
        }
#else
        private OracleConnection GetConnection(string connectionString)
        {
            if (conns == null)
                conns = new Dictionary<string, OracleConnection>();
            OracleConnection connection = null;
            if (conns.ContainsKey(connectionString))
            {
                connection = conns[connectionString];
                Debug.Assert(connection.State == ConnectionState.Closed);
            }
            else
            {
                try
                {
                    conns.Add(connectionString, connection = new OracleConnection(connectionString));
                }
                catch (OracleException ex)
                {
                    OnDatabaseException(ex);
                    return null;
                }
            }
            return connection;
        }
#endif

        /// <summary>
        /// 获得是否连接成功
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public bool IsConnected(string connectionString)
        {
            return Test(GetConnection(connectionString));
        }
        /// <summary>
        /// 创建命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// Depricated
        public OracleCommand CreateCommand2(string connectionString, string sql, SqlParameterDictionary parameters)
        {
            OracleCommand comm = new OracleCommand();
            comm.Connection = GetConnection(connectionString);
            comm.CommandText = sql;
            if (parameters != null)
                foreach (string key in parameters.Keys)
                    comm.Parameters.Add(new OracleParameter(key, parameters[key] ?? DBNull.Value));
            return comm;
        }

        private void SqlizeCommand(OracleCommand comm, string sql, SqlParameterDictionary parameters)
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
                    comm.Parameters.Add(new OracleParameter(key, parameters[key] ?? DBNull.Value));
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
        public OracleCommand CreateCommand(string connectionString, string sql, SqlParameterDictionary parameters)
        {
            OracleCommand comm = new OracleCommand();
            comm.Connection = GetConnection(connectionString);

            SqlizeCommand(comm, sql, parameters);
            return comm;
        }
        public OracleCommand CreateCommand(IDbTransaction tran, string sql, SqlParameterDictionary parameters)
        {
            OracleCommand comm = new OracleCommand();
            comm.Connection = (OracleConnection)tran.Connection;
            comm.Transaction = (OracleTransaction)tran;

            SqlizeCommand(comm, sql, parameters);
            return comm;
        }

        public IDbTransaction BeginTransaction(string connectionString, out string message)
        {
            message = null;
            OracleConnection conn = GetConnection(connectionString);
#if !CONCURRENT
            lock (comm.Connection) // 避免共享资源commConnection冲突
            {
#endif
            try
            {
                return conn.BeginTransaction();
            }
            catch (OracleException ex)
            {
                message = ex.Message;
                return null;
            }
#if !CONCURRENT
            }
#endif
        }
        public IDbTransaction BeginTransaction(string connectionString)
        {
            string message;
            return BeginTransaction(connectionString, out message);
        }

        public bool CommitTransaction(IDbTransaction tran, out string message)
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
            catch (OracleException ex)
            {
                message = ex.Message;
                return false;
            }
#if !CONCURRENT
            }
#endif
        }
        public bool CommitTransaction(IDbTransaction tran)
        {
            string message;
            return CommitTransaction(tran, out message);
        }

        public bool RollbackTransaction(IDbTransaction tran, out string message)
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
            catch (OracleException ex)
            {
                message = ex.Message;
                return false;
            }
#if !CONCURRENT
            }
#endif
        }
        public bool RollbackTransaction(IDbTransaction tran)
        {
            string message;
            return RollbackTransaction(tran, out message);
        }

        public IDataReader Read(string connectionString, string sql, SqlParameterDictionary parameters, out string message)
        {
            message = null;

            DataSet resultDataSet = new DataSet();
            using (OracleCommand comm = CreateCommand(connectionString, sql, parameters))
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
                catch (OracleException ex)
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
        public IDataReader Read(string connectionString, string sql, SqlParameterDictionary parameters)
        {
            string message;
            return Read(connectionString, sql, parameters, out message);
        }

        /// <summary>执行查询语句</summary>
        /// <param name="strSql">sql</param>
        /// <returns>数据集</returns>
        public DataSet Select(string connectionString, string sql, SqlParameterDictionary parameters, out string message)
        {
            message = null;

            DataSet resultDataSet = new DataSet();
            using (OracleCommand comm = CreateCommand(connectionString, sql, parameters))
            {
#if !CONCURRENT
                lock (comm.Connection) // 避免共享资源commConnection冲突
                {
#endif
                try
                {
                    if (comm.Connection.State == ConnectionState.Closed)
                        comm.Connection.Open();

                    new OracleDataAdapter(comm).Fill(resultDataSet);
                    return resultDataSet;
                }
                catch (OracleException ex)
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

        public DataSet Select(string connectionString, string sql, SqlParameterDictionary parameters)
        {
            string message;
            return Select(connectionString, sql, parameters, out message);
        }
        
        /// <summary>执行返回单一值的查询语句</summary>
        /// <param name="strSql">sql</param>
        /// <returns>数据集</returns>
        public object Scalar(string connectionString, string sql, SqlParameterDictionary parameters, out string message)
        {
            message = null;

            using (OracleCommand comm = CreateCommand(connectionString, sql, parameters))
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
                catch (OracleException ex)
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

        public object Scalar(string connectionString, string sql, SqlParameterDictionary parameters)
        {
            string message;
            return Scalar(connectionString, sql, parameters, out message);
        }

        public int NonQuery(IDbTransaction tran, string sql, SqlParameterDictionary parameters, out string message)
        {
            message = null;

            using (OracleCommand comm = CreateCommand(tran, sql, parameters))
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
                catch (OracleException ex)
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
        public int NonQuery(IDbTransaction tran, string sql, SqlParameterDictionary parameters)
        {
            string message = null;
            return NonQuery(tran, sql, parameters, out message);
        }

        /// <summary>执行普通Sql命令</summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns>改变行数</returns>
        public int NonQuery(string connectionString, string sql, SqlParameterDictionary parameters, out string message)
        {
            message = null;

            using (OracleCommand comm = CreateCommand(connectionString, sql, parameters))
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
                catch (OracleException ex)
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
        public int NonQuery(string connectionString, string sql, SqlParameterDictionary parameters)
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
        public void OnDatabaseException(OracleException ex)
        {
            OnDatabaseException(ex, null);
        }
        /// <summary>
        /// 数据库异常委托调用器,和原来相比添加了Command参数,这样外部捕获代码可以直接拿到sql语句等信息,方便外部调试
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="command"></param>
        public void OnDatabaseException(OracleException ex, OracleCommand command)
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
        public void OnNameEncapsulate(string connectionString, ref string fieldName)
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
        public void OnSpecialDbTypeToTypeName(string connectionString, DBType dbt, ref string typeName, ref bool needSize)
        {
            if (SpecialDbTypeToTypeNameHandler != null)
                SpecialDbTypeToTypeNameHandler(connectionString, dbt, ref typeName, ref needSize);
        }
        #endregion
        #endregion
    }
}
