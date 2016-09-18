using System;
using System.Collections.Generic;
// using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// 数据库对象工厂接口
    /// </summary>
    public interface IDatabaseObjectFactory
    {
        //  创建Connection
        IDbConnection CreateConnection();
        //  创建Connection
        IDbConnection CreateConnection(string connectionString);

        //  创建Command
        IDbCommand CreateCommand();
        IDbCommand CreateCommand(string commandText);
        IDbCommand CreateCommand(string commandText, IDbConnection connection);

        //  创建Parameter
        IDataParameter CreateParameter();
        IDataParameter CreateParameter(string name, object value);
        IDataParameter CreateParameter(string name, DbType dataType);

        //  创建DataAdaptor
        IDbDataAdapter CreateDataAdaptor();
        IDbDataAdapter CreateDataAdaptor(IDbCommand selectCommand);
        IDbDataAdapter CreateDataAdaptor(string selectCommandText, IDbConnection connection);
        IDbDataAdapter CreateDataAdaptor(string selectCommandText, string connectionString);
    }
}
