using System;
using System.Collections.Generic;
//  using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Data.Common;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// Oracle数据库对象工厂
    /// </summary>
    public class OracleObjectFactory: IDatabaseObjectFactory
    {
        #region IDatabaseObjectFactory 成员

        #region Connection
        public IDbConnection CreateConnection()
        {
            return new OracleConnection();
        }

        public IDbConnection CreateConnection(string connectionString)
        {
            return new OracleConnection(connectionString);
        }
        #endregion //   Connection

        #region Command
        public IDbCommand CreateCommand()
        {
            return new OracleCommand();
        }
        public IDbCommand CreateCommand(string commandText)
        {
            return new OracleCommand(commandText);
        }
        public IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            return new OracleCommand(commandText, (OracleConnection)connection);
        }
        #endregion //   Command

        #region Parameter
        public IDataParameter CreateParameter()
        {
            return new OracleParameter();
        }
        public IDataParameter CreateParameter(string name, object value)
        {
            return new OracleParameter(name, value);
        }
        public IDataParameter CreateParameter(string name, DbType dataType)
        {
            return new OracleParameter(name, (OracleType)dataType);
        }
        #endregion //   Parameter

        #region DataAdaptor
        public IDbDataAdapter CreateDataAdaptor()
        {
            return new OracleDataAdapter();
        }
        public IDbDataAdapter CreateDataAdaptor(IDbCommand selectCommand)
        {
            return new OracleDataAdapter((OracleCommand)selectCommand);
        }
        public IDbDataAdapter CreateDataAdaptor(string selectCommandText, IDbConnection connection)
        {
            return new OracleDataAdapter(selectCommandText, (OracleConnection)connection);
        }
        public IDbDataAdapter CreateDataAdaptor(string selectCommandText, string connectionString)
        {
            return new OracleDataAdapter(selectCommandText, connectionString);
        }
        #endregion //   DataAdaptor

        #endregion
    }
}
