using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JinHong.Model;
using System.Configuration;

namespace JinHong.ViewModel
{
   /// <summary>
   /// 车位平面图
   /// </summary>
    public class ParkingLotGraphViewModel:AbstractPageViewModel
    {
        #region Fields

        //  TODO...配置文件中配置默认文件名
        //const string LOCAL_IMAGE_NAME = "楼宇区域位置图示";
        readonly object _syncRoot = new object();

        //  本地图片路径
        private string localImagePath = GlobalVariables.GetConfigurationSetting<string>("model.commercial.平面图.imageName");

        //  TODO

        #endregion

        #region Properties

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

        public ParkingLotGraphViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// 查询平面图
        /// </summary>
        /// <param name="actCompleted"></param>
        public void Query(Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    Task.Factory.StartNew(() =>
                    {
                        // 如果服务端的图片和本地不同,则查询并下载到LocalImagePath; 否则跳过
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
