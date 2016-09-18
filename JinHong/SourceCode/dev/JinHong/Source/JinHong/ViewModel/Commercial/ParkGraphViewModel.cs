using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JinHong.Model;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 楼宇平面图
    /// </summary>
    public class ParkGraphViewModel : AbstractPageViewModel
    {
        #region Fields

        //  TODO...配置文件中配置默认文件名
        //const string LOCAL_IMAGE_NAME = "楼宇区域位置图示";
        readonly object _syncRoot = new object();

        string remoteImagePath;
        //  服务端图片路径
        //private string remoteImagePath = GlobalVariables.GetConfigurationSetting<string>("model.commercial.楼宇区域位置图示.remoteImageName");
        //  本地图片路径
        private readonly string localImageDirectory = Path.Combine(System.IO.Path.GetTempPath(), GlobalVariables.GetConfigurationSetting<string>("model.commercial.楼宇区域位置图示.localImageDirectory"));
        private string localImagePath;
        //  TODO

        //  TODO...图片放在服务端文件系统中(服务端保存所有使用过的图示文件), 本地有缓存

        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置
        /// </summary>
        /*
        public string RemoteImagePath
        {
            get { return remoteImagePath; }
            set
            {
                if (remoteImagePath != value)
                {
                    remoteImagePath = value;
                    OnPropertyChanged("RemoteImagePath");
                }
            }
        }*/

        /// <summary>
        /// 获得或者设置本地图片路径
        /// </summary>
        public string LocalImagePath
        {
            get { return localImagePath; }
            set
            {
                if (localImagePath != value)
                {
                    localImagePath = value;
                    OnPropertyChanged("LocalImagePath");
                }
            }
        }

        //  TODO

        #endregion

        #region Constructors

        public ParkGraphViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            SetLocalImagePath();
        }

        #endregion

        #region Methods

        public void SetLocalImagePath()
        {
            Task.Factory.StartNew(() =>
            {
                string sql = string.Format("SELECT Value FROM ConfigItem WHERE KKey='{0}'", "model.commercial.楼宇区域位置图示.remoteImageName");
                remoteImagePath = GlobalVariables.Smc.Scalar<string>(sql, null);

                if (string.IsNullOrEmpty(remoteImagePath))
                {
                    return;
                }
                string localImagePath = Path.Combine(localImageDirectory, new FileInfo(remoteImagePath).Name);

                if (File.Exists(localImagePath))
                {
                    //  对比hash码
                    string sql1 = string.Format("SELECT Value FROM ConfigItem WHERE KKey='{0}'", "model.commercial.楼宇区域位置图示.hash64");
                    string hash1 = GlobalVariables.Smc.Scalar<string>(sql1, null);
                    string hash2;
                    using (MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider())
                    {

                        //using (FileStream fs = new FileStream(localImagePath, FileMode.Open))
                        hash2 = Convert.ToBase64String(hashProvider.ComputeHash(File.ReadAllBytes(localImagePath)));
                    }
                    if (hash1 == hash2)
                        LocalImagePath = localImagePath;
                    else
                        RefreshImage(remoteImagePath, localImagePath);
                }
                else
                {
                    RefreshImage(remoteImagePath, localImagePath);
                }
            });
        }

        public void UploadImage(string localImagePath)
        {
            Task.Factory.StartNew(() =>
            {
                //  扩展名修改
                int index1 = remoteImagePath.LastIndexOf('.');
                int index2 = localImagePath.LastIndexOf('.');
                remoteImagePath = remoteImagePath.Substring(0, index1) + localImagePath.Substring(index2);
                string sql = string.Format("UPDATE ConfigItem SET Value='{1}' WHERE KKey='{0}'", "model.commercial.楼宇区域位置图示.remoteImageName", remoteImagePath);
                GlobalVariables.Smc.NonQuery(sql);
                GlobalVariables.Smc.UploadFile(remoteImagePath, localImagePath);

                //  写hash码
                string hash64;
                using (MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider())
                {
                    using (FileStream fs = new FileStream(localImagePath, FileMode.Open))
                        hash64 = Convert.ToBase64String(hashProvider.ComputeHash(fs));
                }
                sql = string.Format("SELECT count(*) FROM ConfigItem WHERE KKey='{0}'", "model.commercial.楼宇区域位置图示.hash64");
                if (GlobalVariables.Smc.Scalar<int>(sql) > 0)
                    sql = string.Format("UPDATE ConfigItem SET Value='{1}' WHERE KKey='{0}'", "model.commercial.楼宇区域位置图示.hash64", hash64);
                else
                    sql = string.Format("INSERT INTO ConfigItem (KKey, Value) VALUES('{0}', '{1}')", "model.commercial.楼宇区域位置图示.hash64", hash64);
                GlobalVariables.Smc.NonQuery(sql, null);

                LocalImagePath = localImagePath;
            });
        }

        public void RefreshImage(string remoteImagePath, string localImagePath)
        {
            QueryImage(remoteImagePath, localImagePath, null);
        }

        /// <summary>
        /// 查询图示
        /// </summary>
        /// <param name="actCompleted"></param>
        public void QueryImage(string remoteImagePath, string localImagePath, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    Task.Factory.StartNew(() =>
                    {
                        // 如果服务端的图片和本地不同,则查询并下载到LocalImagePath; 否则跳过
                        GlobalVariables.Smc.DownloadFile(remoteImagePath, localImagePath);
                        LocalImagePath = localImagePath;
                    });
                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        //  TODO

        #endregion
    }
}
