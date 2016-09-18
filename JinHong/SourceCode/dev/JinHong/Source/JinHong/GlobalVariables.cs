using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using UniGuy.Core.Services;
using JinHong.Helper;

namespace JinHong
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public static class GlobalVariables
    {
        #region Fields

        //  日志文件
        const string LOGFILE = @"JinHong.log";

        public static LogService Ls = new LogService(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LOGFILE));

        //  应用程序设置
        public static ApplicationSettings AppSettings = null;   //new ApplicationSettings();
        //  应用程序状态信息
        public static readonly ApplicationStatusInfo AppStatusInfo = new ApplicationStatusInfo();

        //  WebService客户端
        public static ServiceMainClient Smc = new ServiceMainClient();

        public static ExportFileHelper ExportHelper = new ExportFileHelper();

        #endregion

        #region Constructors

        static GlobalVariables()
        {
            //  TODO
        }

        #endregion

        #region Methods

        #region Configuration
        public static T GetConfigurationSetting<T>(string key, T defaultValue = default(T))
        {
            string temp = ConfigurationManager.AppSettings[key];
            if (temp != null)
                return (T)Convert.ChangeType(temp, typeof(T));
            return defaultValue;
        }

        public static string GetConfigurationSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        #endregion

        #endregion
    }
}
