using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JinHong.Model;

namespace JinHong.ViewModel
{
    public class FireViewModel : AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        //  消防设备表
        private DataTable fireFightingEquipmentInfoTbl;
        //  当前选中消防设备
        private FireFightingEquipmentInfo selectedFireFightingEquipmentInfo;
        private string _whereName;

        //  TODO

        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置消防设备表
        /// </summary>
        public DataTable FireFightingEquipmentInfoTbl
        {
            get { return fireFightingEquipmentInfoTbl; }
            set
            {
                if (fireFightingEquipmentInfoTbl != value)
                {
                    fireFightingEquipmentInfoTbl = value;
                    OnPropertyChanged("FireFightingEquipmentInfoTbl");
                }
            }
        }

        public string WhereName
        {
            get { return _whereName; }
            set
            {
                if (_whereName != value)
                {
                    _whereName = value;
                    OnPropertyChanged("WhereName");
                }
            }
        }

        /// <summary>
        /// 获得或者设置当前选中消防设备
        /// </summary>
        public FireFightingEquipmentInfo SelectedFireFightingEquipmentInfo
        {
            get { return selectedFireFightingEquipmentInfo; }
            set
            {
                if (selectedFireFightingEquipmentInfo != value)
                {
                    selectedFireFightingEquipmentInfo = value;
                    OnPropertyChanged("SelectedFireFightingEquipmentInfo");
                }
            }
        }

        //  TODO

        #endregion

        #region Constructors

        public FireViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion

        #region Methods

        public void Query(Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    Task.Factory.StartNew(() =>
                    {
                        // 查询并设置FireFightingEquipmentInfoTbl
                        string sql = "SELECT * FROM FireFightingEquipmentInfo";
                        DataSet dsTemp = GlobalVariables.Smc.Select(sql);
                        if (dsTemp != null && dsTemp.Tables.Count > 0)
                            FireFightingEquipmentInfoTbl = dsTemp.Tables[0];
                    });
                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        public void Query(string queryStr, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    Task.Factory.StartNew(() =>
                    {
                        // 查询并设置FireFightingEquipmentInfoTbl
                        string sql = string.Format("SELECT * FROM FireFightingEquipmentInfo where BuildingName like '%{0}%'", queryStr);
                        DataSet dsTemp = GlobalVariables.Smc.Select(sql);
                        if (dsTemp != null && dsTemp.Tables.Count > 0)
                            FireFightingEquipmentInfoTbl = dsTemp.Tables[0];
                    });
                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }



        #endregion
    }
}
