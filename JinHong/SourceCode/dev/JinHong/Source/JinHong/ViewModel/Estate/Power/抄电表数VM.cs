using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JinHong.Model;
using UniGuy.Core.DataStructures;
using UniGuy.Core.Extensions;

namespace JinHong.ViewModel
{
    public class 抄电表数VM : AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        private DataTable ammeterDataInfoTbl;


        //  年份
        private int year;
        //  月份(只有当前月份在有权限的情况下可编辑)
        private int month;

        //  指定年份月份的抄电表数, 由AmmeterDataInfo查询获得; 界面上拥有权限的用户每次修改一个值直接保存
        private ObservableCollection<ObservableTuple<string, double[]>> powerValues;    //  roomId + powerValue[12]

        //  TODO

        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置年份
        /// </summary>
        public int Year
        {
            get { return year; }
            set
            {
                if (year != value)
                {
                    year = value;
                    OnPropertyChanged("Year");
                    Query();
                }
            }
        }

        public DataTable AmmeterDataInfoTbl
        {
            get { return ammeterDataInfoTbl; }
            set
            {
                if (ammeterDataInfoTbl != value)
                {
                    ammeterDataInfoTbl = value;
                    OnPropertyChanged("AmmeterDataInfoTbl");

                }
            }
        }

        /// <summary>
        /// 获得或者设置月份
        /// </summary>
        public int Month
        {
            get { return month; }
            set
            {
                if (month != value)
                {
                    month = value;
                    OnPropertyChanged("Month");
                }
            }
        }

        /// <summary>
        /// 获得或者设置指定年份月份的抄电表数
        /// </summary>
        public ObservableCollection<ObservableTuple<string, double[]>> PowerValues
        {
            get { return powerValues; }
            set
            {
                if (powerValues != value)
                {
                    powerValues = value;
                    OnPropertyChanged("PowerValues");
                }
            }
        }

        //  TODO

        #endregion

        #region Constructors

        public 抄电表数VM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
        }

        #endregion

        #region Methods

        public void Query()
        {
            Query(null);
        }

        public void Query(Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    // 查询AmmeterDataInfo
                    string sql = string.Format("SELECT * FROM AmmeterDataInfo WHERE Year={0}", Year);
                    DataSet ds = GlobalVariables.Smc.Select(sql, null);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        AmmeterDataInfoTbl = ds.Tables[0];
                        ObservableCollection<ObservableTuple<string, double[]>> temp = new ObservableCollection<ObservableTuple<string, double[]>>();
                        //  查找所有Room
                        string[] roomIds = GlobalVariables.Smc.GetColumnData<RoomInfo>("Name");
                        temp.AddRangeUnique(roomIds.Select(s => new ObservableTuple<string, double[]>(s, null)));

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            string roomId = (string)row["RoomId"];
                            int month = Convert.ToInt32(row["Month"]);
                            double powerValue = row["PowerValue"] is DBNull ? double.NaN : Convert.ToDouble(row["PowerValue"]);
                            ObservableTuple<string, double[]> tuple = temp.FirstOrDefault(t => t.Item1 == roomId);
                            if (tuple.IsDefault())
                            {
                                tuple = new ObservableTuple<string, double[]> { Item1 = roomId };
                                temp.Add(tuple);
                            }
                            if (tuple.Item2 == null)
                                tuple.Item2 = new double[12];
                            tuple.Item2[month] = powerValue;
                        }
                        PowerValues = temp;
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        public void SavePowerValue(string roomId, int month, double powerValue)
        {
            Task.Factory.StartNew(() =>
            {
                AmmeterDataInfo adi = new AmmeterDataInfo { Id = Guid.NewGuid().ToString(), Year = Year, Month = month, PowerValue = powerValue, RoomId = roomId };
                double temp = GlobalVariables.Smc.Scalar<double>(string.Format("SELECT PowerValue FROM AmmeterDataInfo WHERE Year='{0}' AND Month='{1}'", Year, month), null);
                if (temp.IsDefault())
                {
                    if (!GlobalVariables.Smc.Insert<AmmeterDataInfo>(adi))
                        GlobalVariables.Smc.Update<AmmeterDataInfo>(adi);
                }
                else
                    GlobalVariables.Smc.Update<AmmeterDataInfo>(adi);
            });
        }

        //  TODO

        #endregion
    }
}
