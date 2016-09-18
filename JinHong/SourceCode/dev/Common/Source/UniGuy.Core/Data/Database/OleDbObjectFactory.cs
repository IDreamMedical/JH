using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// OleDb数据库对象工厂
    /// </summary>
    public class OleDbObjectFactory: IDatabaseObjectFactory
    {
        #region IDatabaseObjectFactory 成员

        #region Connection
        public IDbConnection CreateConnection()
        {
            return new OleDbConnection();
        }

        public IDbConnection CreateConnection(string connectionString)
        {
            return new OleDbConnection(connectionString);
        }
        #endregion //   Connection

        #region Command
        public IDbCommand CreateCommand()
        {
            return new OleDbCommand();
        }
        public IDbCommand CreateCommand(string commandText)
        {
            return new OleDbCommand(commandText);
        }
        public IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            return new OleDbCommand(commandText, (OleDbConnection)connection);
        }
        #endregion //   Command

        #region Parameter
        public IDataParameter CreateParameter()
        {
            return new OleDbParameter();
        }
        public IDataParameter CreateParameter(string name, object value)
        {
            return new OleDbParameter(name, value);
        }
        public IDataParameter CreateParameter(string name, DbType dataType)
        {
            return new OleDbParameter(name, (OleDbType)dataType);
        }
        #endregion //   Parameter

        #region DataAdaptor
        public IDbDataAdapter CreateDataAdaptor()
        {
            return new OleDbDataAdapter();
        }
        public IDbDataAdapter CreateDataAdaptor(IDbCommand selectCommand)
        {
            return new OleDbDataAdapter((OleDbCommand)selectCommand);
        }
        public IDbDataAdapter CreateDataAdaptor(string selectCommandText, IDbConnection connection)
        {
            return new OleDbDataAdapter(selectCommandText, (OleDbConnection)connection);
        }
        public IDbDataAdapter CreateDataAdaptor(string selectCommandText, string connectionString)
        {
            return new OleDbDataAdapter(selectCommandText, connectionString);
        }
        #endregion //   DataAdaptor

        #endregion
    }
}
