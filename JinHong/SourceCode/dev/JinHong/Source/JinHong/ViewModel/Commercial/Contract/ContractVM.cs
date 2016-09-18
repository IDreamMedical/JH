using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using JinHong.Model;
using System.Data;
using System.Threading.Tasks;
using UniGuy.Core.Extensions;
using UniGuy.Commands;
using JinHong.Helper;
using JinHong.Services;
using JinHong.ServiceContract;
using System.Windows;
using JinHong.View.Dialogs;
using UniGuy.Corek;
using System.Windows.Threading;


namespace JinHong.ViewModel
{
    public class ContractVM : BaseViewModel
    {





        #region private var

        private static readonly Lazy<IContractService> lazy = new Lazy<IContractService>(() => new ContractService());

        private ContractInfo _selectedContract;



        #endregion

        #region Public  var
        public static IContractService Service { get { return lazy.Value; } }


        public ContractInfo SelectedContract
        {
            get { return _selectedContract; }
            set
            {
                if (_selectedContract != value)
                {
                    _selectedContract = value;
                    OnPropertyChanged("SelectedContract");
                }
            }
        }
        #endregion


        #region Events

        #endregion

        #region Fields
        const string REMOTE_CONTRACT_DIR = "Contracts";











        #endregion


        #region Constructors

        public ContractVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {

        }

        #endregion



        #region Methods
        public void Initialize()
        {
            this.ModuleName = "合同管理列表";
            this.RefreshCommand = new DelegateCommand(OnRefreshCommand);
            this.EditCommand = new DelegateCommand(OnEditCommand, CanExecute);
            this.AddNewCommand = new DelegateCommand(OnAddNewCommand);
            this.RemoveCommand = new DelegateCommand(OnRemoveCommand, CanExecute);
            this.ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            this.ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);
        }



        public void Save(ContractInfo contractInfo, ObservableCollection<ContractDetail> contractDetails, string fileName)
        {
            if (contractInfo == null)
            {
                return;
            }
            contractInfo.LeaseType = 0;
            if (GlobalVariables.Smc.Insert<ContractInfo>(contractInfo))
            {
                RentalFeesInfo rfi = new RentalFeesInfo()
                {
                    Id = Guid.NewGuid().ToString(),
                    Amount = 0,
                    SocialUnitId = contractInfo.SocialUnitId,
                    SocialUnitName = contractInfo.SocialUnitName,
                    TimeFrom = contractInfo.EffectiveDate.Value,
                    TimeTo = contractInfo.EffectiveDate.Value.AddMonths(3).AddDays(-1)
                };
                PropertyManagementFeesInfo pmfi = new PropertyManagementFeesInfo()
                {
                    Id = Guid.NewGuid().ToString(),
                    Amount = 0,
                    SocialUnitId = contractInfo.SocialUnitId,
                    SocialUnitName = contractInfo.SocialUnitName,
                    TimeFrom = contractInfo.EffectiveDate.Value,
                    TimeTo = contractInfo.EffectiveDate.Value.AddMonths(3).AddDays(-1)

                };
                foreach (var item in contractDetails)
                {
                    GlobalVariables.Smc.Insert<ContractDetail>(item);
                    GlobalVariables.Smc.NonQuery(string.Format("UPDATE RoomInfo  set Status=1  where  id='{0}'", item.RoomId));
                    rfi.Amount = rfi.Amount + item.MonthRentalFee * 3;
                    pmfi.Amount = pmfi.Amount + item.MonthPropManageFee * 3;

                }
                GlobalVariables.Smc.Insert<PropertyManagementFeesInfo>(pmfi);
                GlobalVariables.Smc.Insert<RentalFeesInfo>(rfi);
                ContactRecords cr = new ContactRecords()
                {
                    Id = Guid.NewGuid().ToString(),
                    ContractId = contractInfo.Id,
                    Status = "0",
                    FileName = fileName
                };
                GlobalVariables.Smc.Insert<ContactRecords>(cr);
                GlobalVariables.Smc.UploadFile(contractInfo.SocialUnitName + ".doc", cr.FileName);
            }


        }



        /// <summary>
        /// 获取租赁明细
        /// </summary>
        /// <returns></returns>
        public DataTable GetContractDetailTbl()
        {
            string sql = string.Format(
        @" select a.Id,a.RoomId,a.BuildingId,a.Area,a.Notes,a.DayPropManageFee,
          a.MonthPropManageFee,a.DayRentalFee,a.MonthRentalFee 
          from   ContractDetail a   INNER join   ContractInfo b   on   a.ContractId  =b.Id  where  b.Id='{0}'   ", SelectedContract.Id);
            DataSet dsTemp = GlobalVariables.Smc.Select(sql);
            return dsTemp == null ? null : dsTemp.Tables[0];
        }


        #region Private Method


        private void OnExportToPdfCommand()
        {
            base.ExportToExcel(this.SourceTbl, this.ModuleName);
        }
        private void OnExportToExcelCommand()
        {
            base.ExportToPdf(this.SourceTbl, this.ModuleName);
        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelContract(this._selectedContract.Id))
            {
                MessageBox.Show("删除成功！", "系统提示");
            }
            else
            {
                MessageBox.Show("删除失败！", "系统提示");
            }
        }

        private void OnAddNewCommand()
        {
            var dlg = new NewOrEditContract();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.Contract = new ContractInfo
            {
                Id = Guid.NewGuid().ToString(),
                Status = 0,
                ContractNo = new GenerateCodeHelper().CreateReportCode()
            };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();
        }

        private void OnEditCommand()
        {
            var dlg = new NewOrEditContract();
            dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            dlg.ViewModel.Contract = this.SelectedContract;
            dlg.ShowDialog();
        }

        private void OnRefreshCommand()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            Query(WhereName, () => Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }


        public override bool CanExecute()
        {
            return IsCanExecute;
        }


        private void Query(string whereStr, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(whereStr))
                    {
                        SourceTbl = Service.GetAllContracts();
                    }
                    else
                    {
                        SourceTbl = Service.GetContractByWhereStr(whereStr);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        #endregion


        #endregion



    }
}
