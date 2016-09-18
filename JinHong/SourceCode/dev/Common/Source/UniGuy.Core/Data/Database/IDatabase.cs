using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using System.Data.Common;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// 数据库接口
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// 获得是否连接成功
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        bool IsConnected(string connectionString);

        /// <summary>
        /// 数据库异常委托方法
        /// </summary>
        DatabaseExceptionDelegate DatabaseExceptionHandler { get; set; }

        IDbTransaction BeginTransaction(string connectionString, out string message);
        IDbTransaction BeginTransaction(string connectionString);
        bool CommitTransaction(IDbTransaction tran, out string message);
        bool CommitTransaction(IDbTransaction tran);
        bool RollbackTransaction(IDbTransaction tran, out string message);
        bool RollbackTransaction(IDbTransaction tran);

        /// <summary>
        /// 类似Select，不过返回IDataReader, 可能能降低空间复杂度
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <remarks>
        /// The returned IDataReader should be disposed after use.
        /// </remarks>
        IDataReader Read(string connectionString, string sql, SqlParameterDictionary parameters, out string message);
        IDataReader Read(string connectionString, string sql, SqlParameterDictionary parameters);

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        DataSet Select(string connectionString, string sql, SqlParameterDictionary parameters, out string message);
        DataSet Select(string connectionString, string sql, SqlParameterDictionary parameters);

        /// <summary>
        /// 执行查询语句返回单个值
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        object Scalar(string connectionString, string sql, SqlParameterDictionary parameters, out string message);
        object Scalar(string connectionString, string sql, SqlParameterDictionary parameters);

        /// <summary>
        /// 执行非查询语句
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        int NonQuery(string connectionString, string sql, SqlParameterDictionary parameters, out string message);
        int NonQuery(string connectionString, string sql, SqlParameterDictionary parameters);
        int NonQuery(IDbTransaction tran, string sql, SqlParameterDictionary parameters, out string message);
        int NonQuery(IDbTransaction tran, string sql, SqlParameterDictionary parameters);
    }

    /// <summary>
    /// 定义数据库异常委托
    /// </summary>
    /// <param name="ex"></param>
    public delegate void DatabaseExceptionDelegate(Exception ex, IDbCommand command);
}
