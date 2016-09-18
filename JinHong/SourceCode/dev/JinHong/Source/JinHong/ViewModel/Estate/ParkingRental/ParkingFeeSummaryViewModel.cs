using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JinHong.Model;
using UniGuy.Commands;
using System.Windows.Threading;
using JinHong.ServiceContract;
using JinHong.Services;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 停车费统计表 包括如下：
    /// a.
    ///     1、一段时间内已收停车费汇总
    ///     2、一段时间内未收停车费汇总
    /// b. 一段时间某个人的停车费汇总；
    /// c  一段时间内一家单位的停车费汇总；
    /// </summary>

    public class ParkingFeeSummaryViewModel : BaseViewModel
    {
        #region  private Fields
        private static readonly Lazy<IParkingFeeService> lazy = new Lazy<IParkingFeeService>(() => new ParkingFeeService());

        /// <summary>
        /// 开始日期
        /// </summary>
        private DateTime _startDate;
        /// <summary>
        /// 结束日期
        /// </summary>
        private DateTime _endDate;



        #endregion

        #region public Prop

        public static IParkingFeeService Service { get { return lazy.Value; } }



        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }



        #endregion

        #region Constructors

        public ParkingFeeSummaryViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            StartDate = DateTime.Now.Date.AddDays(-7);
            EndDate = DateTime.Now.Date;
        }

        #endregion

        #region Methods

        #region Public
        public void Initialize()
        {
            this.ModuleName = "停车费汇总表";
            this.RefreshCommand = new DelegateCommand(OnRefreshCommand);
            this.ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            this.ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);
            OnRefreshCommand();
        }

        #endregion

        #region private methods
        /// <summary>
        /// 导出pdf
        /// </summary>
        private void OnExportToPdfCommand()
        {
            base.ExportToPdf(base.SourceTbl, this.ModuleName);
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        private void OnExportToExcelCommand()
        {
            base.ExportToExcel(base.SourceTbl, this.ModuleName);
        }



        private void OnRefreshCommand()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            Query(WhereName, () => Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }
        #endregion

      

        public void Query(string whereName,Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(whereName))
                    {
                        SourceTbl = Service.GetParkingFeeDetail(string.Empty);
                    }
                    else
                    {
                        SourceTbl = Service.GetParkingFeeDetail(whereName);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }


        #endregion
    }
}
