using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using JinHong.Model;

namespace JinHong.ViewModel
{
    public class 租金_物业费缴纳情况记录管理业务VM:AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        //  查询到并显示的月租费信息;
        private DataTable estateRentPriceInfoTbl;
        //  当前浏览或编辑的月租费信息;
        private EstateRentPriceInfo selectedEstateRentPriceInfo;

        //  TODO

        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置查询到并显示的月租费信息
        /// </summary>
        public DataTable EstateRentPriceInfoTbl
        {
            get { return estateRentPriceInfoTbl; }
            set
            {
                if (estateRentPriceInfoTbl != value)
                {
                    estateRentPriceInfoTbl = value;
                    OnPropertyChanged("EstateRentPriceInfoTbl");
                }
            }
        }

        /// <summary>
        /// 获得或者设置当前浏览或编辑的月租费信息
        /// </summary>
        public EstateRentPriceInfo SelectedEstateRentPriceInfo
        {
            get { return selectedEstateRentPriceInfo; }
            set
            {
                if (selectedEstateRentPriceInfo != value)
                {
                    selectedEstateRentPriceInfo = value;
                    OnPropertyChanged("SelectedEstateRentPriceInfo");
                }
            }
        }

        //  TODO

        #endregion

        #region Constructors

        public 租金_物业费缴纳情况记录管理业务VM(MainWindowViewModel parentVM)
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
                        // 查询并设置EstateRentPriceInfoTbl
                        string sql = "SELECT * FROM EstateRentPriceInfo";
                        DataSet ds = GlobalVariables.Smc.Select(sql, null);
                        this.EstateRentPriceInfoTbl = ds == null ? null : ds.Tables[0];
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
