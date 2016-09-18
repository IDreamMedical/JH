using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UniGuy.Core.Services
{
    /// <summary>
    /// 一个简单的日志服务类
    /// </summary>
    public class LogService
    {
        #region Fields

        private object _lock = new object();

        /// <summary>
        /// 记录日志采用的方法
        /// </summary>
        private Approach _logApproach = Approach.LocalFile;
        /// <summary>
        /// 日志文件名(从{BinDir}后记, LogApproach须包含LocalFile才生效)
        /// </summary>
        private string _logFilename;
        /// <summary>
        /// 日志记录委托方法(LogApproach须包含Delegate才生效)
        /// 一般是通过这个委托把日志写到指定数据库中
        /// </summary>
        private LogDelegate _logHandler;

        /// <summary>
        /// 是否手动清空缓存
        /// </summary>
        private bool _manualFlushing;
        //  日志项缓存
        private List<LogItem> _cache;
        // 日志项缓存激发Flush的阀值(此外外部程序应直接调用Flush方法)
        private int _maximumCacheItemCout=10;

        #endregion //   Fields

        #region Properties
        /// <summary>
        /// 获得或者设置记录日志采用的方法
        /// </summary>
        public Approach LogApproach
        {
            get { return _logApproach; }
            set { _logApproach = value; }
        }
        /// <summary>
        /// 获得或者设置日志记录委托方法
        /// </summary>
        public string LogFilename
        {
            get { return _logFilename; }
            set 
            {
                _logFilename = System.IO.Path.IsPathRooted(value)?value: AppDomain.CurrentDomain.BaseDirectory + value; 
            }
        }
        /// <summary>
        /// 获得或者设置日志记录委托方法
        /// </summary>
        public LogDelegate LogHandler
        {
            get { return _logHandler; }
            set { _logHandler = value; }
        }

        /// <summary>
        /// 设置是否手动清空缓存
        /// </summary>
        public bool ManualFlushing
        {
            get { return _manualFlushing; }
            set 
            {
                _manualFlushing = value;
                if (value)
                    _cache = new List<LogItem>(_maximumCacheItemCout);
            }
        }

        /// <summary>
        /// 获得或者设置日志项缓存激发Flush的阀值
        /// </summary>
        public int MaximumCacheCount
        {
            get { return _maximumCacheItemCout; }
            set
            {
                if (!_manualFlushing)
                    throw new Exception("MaximumCacheCount can only be set whhen Manual flushing is set.");
                if (value == 0)
                    throw new ArgumentException("MaximumCacheCount must be greater than 0.");
                _maximumCacheItemCout = value;  //  -1表示不设阀值，必须外部调用Flush()
            }
        }
        #endregion //   Properties

        #region Constructors
        public LogService() { }
        public LogService(string logFilename)
        {
            this.LogFilename = logFilename;
        }
        public LogService(LogDelegate logHandler)
        {
            this._logApproach = Approach.Delegate;
            this._logHandler = logHandler;
        }
        public LogService(string logFilename, LogDelegate logHandler)
        {
            this._logApproach = Approach.Both;
            this.LogFilename = logFilename;
            this._logHandler = logHandler;
        }
        #endregion //   Constructors

        #region Methdods
        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="strInfo"></param>
        public void LogInfo(string strInfo)
        {
            LogInfo(strInfo, null);
        }
        /// <summary>
        /// 记录信息以及其详情
        /// </summary>
        /// <param name="strInfo"></param>
        /// <param name="detail"></param>
        public void LogInfo(string strInfo, object detail)
        {
            Log(Rank.Info, strInfo, detail);
        }
        /// <summary>
        /// 记录警告
        /// </summary>
        /// <param name="strWarning"></param>
        public void LogWarning(string strWarning)
        {
            LogWarning(strWarning, null);
        }
        /// <summary>
        /// 记录警告及其详情
        /// </summary>
        /// <param name="strWarning"></param>
        /// <param name="detail"></param>
        public void LogWarning(string strWarning, object detail)
        {
            Log(Rank.Warning, strWarning, detail);
        }
        /// <summary>
        /// 记录错误
        /// </summary>
        /// <param name="strError"></param>
        public void LogError(string strError)
        {
            LogError(strError, null);
        }
        /// <summary>
        /// 记录错误及其详情
        /// </summary>
        /// <param name="strError"></param>
        /// <param name="detail"></param>
        public void LogError(string strError, object detail)
        {
            Log(Rank.Error, strError, detail);
        }
        /// <summary>
        /// 记录一般日志消息
        /// </summary>
        /// <param name="msg"></param>
        public void Log(string msg)
        {
            Log(msg, null);
        }
        /// <summary>
        /// 记录一般日志消息及其详情
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="detail"></param>
        public void Log(string msg, object detail)
        {
            Log(Rank.Other, msg, detail);
        }
        /// <summary>
        /// 记录指定等级的日志消息及其详情
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="msg"></param>
        /// <param name="detail"></param>
        public void Log(Rank rank, string msg, object detail)
        {
            Log(DateTime.Now, rank, msg, detail);
        }
        /// <summary>
        /// 记录某个时间发生的执行等级的日志消息及其详情
        /// </summary>
        /// <param name="time"></param>
        /// <param name="rank"></param>
        /// <param name="msg"></param>
        /// <param name="detail"></param>
        public void Log(DateTime time, Rank rank, string msg, object detail)
        {
            if (_manualFlushing)
            {
                lock (_lock)
                    _cache.Add(new LogItem { Time = time, Rank = rank, Msg = msg, Detail = detail });
                if (_maximumCacheItemCout!=-1 && _cache.Count >= _maximumCacheItemCout)
                    Flush();
            }
            else
            {
                new Action(delegate()
                    {
                        InnerLog(time, rank, msg, detail);
                    }).BeginInvoke(null, null);
            }
        }

        /// <summary>
        /// 清空缓存(达到阀值自动调用，其他情况外部调用)
        /// </summary>
        public void Flush()
        {
            lock (_lock)
            {
                while (_cache.Count > 0)
                {
                    new Action(delegate(){InnerLog(_cache[0]);}).BeginInvoke(null, null);
                    _cache.Remove(_cache[0]);
                }
            }
        }

        private void InnerLog(LogItem li)
        {
            InnerLog(li.Time, li.Rank, li.Msg, li.Detail);
        }
        private void InnerLog(DateTime time, Rank rank, string msg, object detail)
        {
            if ((LogApproach & Approach.LocalFile) != 0)
            {
                lock (_lock)
                {
#if DEBUG
                    try
                    {
#endif
                        //  保证存在目录
                        FileInfo fi = new FileInfo(LogFilename);
                        if (!fi.Directory.Exists)
                            fi.Directory.Create();
                        //  system.log为默认日志文件名
                        File.AppendAllText(LogFilename,
                                string.Format("{0}\t{1}\t{2}\t{3}{4}", time.ToString(@"yyyy-MM-dd HH\:mm\:ss"), rank, msg, detail, Environment.NewLine));
#if DEBUG
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message + ex.StackTrace);
                    }
#endif
                }
            }
            if ((LogApproach & Approach.Delegate) != 0)
            {
                lock (_lock)
                {
                    if (LogHandler == null)
                        throw new ArgumentNullException("Log handler can not be null.");
                    LogHandler(time, rank, msg, detail);
                }
            }
        }
        #endregion //   Methods

        #region Internal types
        /// <summary>
        /// 日志项等级
        /// </summary>
        public enum Rank
        {
            Other=0,
            Info=1,
            Warning=2,
            Error=3
        }

        /// <summary>
        /// 日志记录方法
        /// </summary>
        [Flags]
        public enum Approach
        {
            LocalFile=1,  //  本地日志文件
            Delegate=2,    //  委托方法
            Both = LocalFile|Delegate
        }
        /// <summary>
        /// 记录日志的委托类型
        /// </summary>
        /// <param name="time"></param>
        /// <param name="rank"></param>
        /// <param name="msg"></param>
        /// <param name="detail"></param>
        public delegate void LogDelegate(DateTime time, Rank rank, string msg, object detail);
#if DOT_NET_35
#else
        public delegate void Action();
#endif

        internal class LogItem
        {
            public DateTime Time{get;set;}
            public Rank Rank { get; set; }
            public string Msg { get; set; }
            public object Detail { get; set; }
        }
        #endregion //   Internal types
    }
}
