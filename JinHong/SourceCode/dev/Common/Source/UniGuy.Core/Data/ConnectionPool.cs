#define AUTO_CLOSE  //  是否自动超时关闭连接

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;

namespace UniGuy.Core.Data
{
    /// <summary>
    /// 一个简单的连接池实现，没有连接生存时长限制，知道本类实例Dispose;
    /// TODO... Not tested
    /// </summary>
    /// <remarks>
    /// 使用方法：
    /// 1. 程序初始化时或者其他要访问指定数据库之前根据对应的连接串和连接方法创建连接池实例
    /// ConnectionPool<SqlConnection> cp = new ConnectionPool<SqlConnection>("[some sql connection string]");
    /// 2. 自定义数据库访问类中的数据库操作方法
    /// SqlConnection = cp.GetConnection();
    /// ...数据库操作
    /// cp.ReleaseConnection();
    /// 3. 程序关闭之前或者确保不再使用该连接后
    /// cp.Dispose();
    /// </remarks>
    public class ConnectionPool<C>: IDisposable where C:IDbConnection, new()
    {
        #region Fields
        /// <summary>
        /// 池中的连接
        /// </summary>
        private Stack<C> conns;

        /// <summary>
        /// 最大连接数
        /// </summary>
        private int maxSize = 20;

#if AUTO_CLOSE
        /// <summary>
        /// 每个连接1分钟废弃
        /// </summary>
        private int duration = 1000 * 60;
#endif
        /// <summary>
        /// 连接串
        /// </summary>
        private string connString;

#if AUTO_CLOSE
        Timer _timer;
        Dictionary<C, int> _idleTimes = new Dictionary<C, int>();
#endif
        #endregion

        #region Properties
        /// <summary>
        /// 获得最大连接数
        /// </summary>
        public int MaxSize
        {
            get { return maxSize; }
            private set { maxSize = value; }
        }
#if AUTO_CLOSE
        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }
#endif
        /// <summary>
        /// 获得连接串
        /// </summary>
        public string ConnString
        {
            get { return connString; }
            set { connString = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// 和lock类似，确保方法同步
        /// </summary>
        //[MethodImpl(MethodImplOptions.Synchronized)]
        public ConnectionPool(string connString)
        {
            ConnString = connString;

#if AUTO_CLOSE
            _timer = new Timer(new TimerCallback(obj =>
            {
                lock (this)
                {
                    foreach (C conn in _idleTimes.Keys)
                    {
                        int temp = _idleTimes[conn] += 1000;
                        if (temp >= duration)
                        {
                            conn.Close();
                            _idleTimes.Remove(conn);
                        }
                    }
                }
            }), null, 1000, 1000);
#endif
        }
        public ConnectionPool(string connString, int maxSize):this(connString)
        {
            MaxSize = maxSize;
        }
        public int Count
        {
            get { return conns == null ? 0 : conns.Count; }
        }
        #endregion

        #region Methods

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public C GetConnection()
        {
            C conn;
            lock (this)
            {
                if (Count == 0)
                {
                    if (conns == null)
                        conns = new Stack<C>();
                    if (Count < MaxSize)
                    {
                        conn = CreateConnection();
                        conns.Push(conn);
                    }
                    else
                    {
                        Monitor.Wait(this);
                    }
                }
#if AUTO_CLOSE
            gc0:
                conn = conns.Pop();
                if (conn.State != ConnectionState.Open)
                {
                    if (Count > 0)
                        goto gc0;
                    else
                        return GetConnection();
                }
                Debug.Assert(_idleTimes.ContainsKey(conn));
#else
                conn = conns.Pop();
#endif
                //  不再计时
                if (_idleTimes.ContainsKey(conn))
                    _idleTimes.Remove(conn);
            }
            
            return conn;
        }

        public void ReleaseConnection(C conn)
        {
            lock (this)
            {
                conns.Push(conn);
                Monitor.Pulse(this);

#if AUTO_CLOSE
                //  重新计时
                Debug.Assert(!_idleTimes.ContainsKey(conn));
                _idleTimes.Add(conn, 0);
#endif
            }
        }

        private C CreateConnection()
        {
            lock (this)
            {
                C newConn = new C();
                newConn.Open();
                return newConn;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            lock (this)
            {
                while (Count > 0)
                {
                    C conn = conns.Pop();
                    conn.Close();
                }
            }
        }

        #endregion

        ~ConnectionPool()
        {
            Dispose();
        }
    }
}
