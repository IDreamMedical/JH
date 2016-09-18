using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// Sql数据库对象工厂
    /// </summary>
    public class SqlObjectFactory: IDatabaseObjectFactory
    {
        #region IDatabaseObjectFactory 成员

        #region Connection
        public IDbConnection CreateConnection()
        {
            return new SqlConnection();
        }

        public IDbConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
        #endregion //   Connection

        #region Command
        public IDbCommand CreateCommand()
        {
            return new SqlCommand();
        }
        public IDbCommand CreateCommand(string commandText)
        {
            return new SqlCommand(commandText);
        }
        public IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            return new SqlCommand(commandText, (SqlConnection)connection);
        }
        #endregion //   Command

        #region Parameter
        public IDataParameter CreateParameter()
        {
            return new SqlParameter();
        }
        public IDataParameter CreateParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }
        public IDataParameter CreateParameter(string name, DbType dataType)
        {
            return new SqlParameter(name, (SqlType)dataType);
        }
        #endregion //   Parameter

        #region DataAdaptor
        public IDbDataAdapter CreateDataAdaptor()
        {
            return new SqlDataAdapter();
        }
        public IDbDataAdapter CreateDataAdaptor(IDbCommand selectCommand)
        {
            return new SqlDataAdapter((SqlCommand)selectCommand);
        }
        public IDbDataAdapter CreateDataAdaptor(string selectCommandText, IDbConnection connection)
        {
            return new SqlDataAdapter(selectCommandText, (SqlConnection)connection);
        }
        public IDbDataAdapter CreateDataAdaptor(string selectCommandText, string connectionString)
        {
            return new SqlDataAdapter(selectCommandText, connectionString);
        }
        #endregion //   DataAdaptor

        #endregion
    }
}
