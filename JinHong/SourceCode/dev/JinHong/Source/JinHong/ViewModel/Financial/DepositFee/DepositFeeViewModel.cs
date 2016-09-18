using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using JinHong.Model;
using JinHong.ServiceContract;
using JinHong.Services;
using JinHong.Helper;
using System.Windows;
using System.Windows.Threading;
using UniGuy.Commands;
using JinHong.View.Dialogs;
using UniGuy.Corek;

namespace JinHong.ViewModel
{

    /// <summary>
    /// 押金
    /// </summary>
    public class DepositFeeViewModel : BaseViewModel
    {
        #region private  Fields



        /// <summary>
        /// 选中 的押金记录
        /// </summary>
        private DepositFeeInfo _selectedDepositFee;

        private static readonly Lazy<IDepositFeeService> lazy = new Lazy<IDepositFeeService>(() => new DepositFeeService());

        #endregion

        #region public  Prop

        public static IDepositFeeService Service { get { return lazy.Value; } }

        public DepositFeeInfo SelectedDepositFeeInfo
        {
            get { return _selectedDepositFee; }
            set
            {
                if (_selectedDepositFee != value)
                {
                    _selectedDepositFee = value;
                    OnPropertyChanged("SelectedDepositFeeInfo");
                }
            }
        }



        #endregion


        #region Constructors

        public DepositFeeViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion

        #region Methods


        public void Initialize()
        {
            this.ModuleName = "停车费收费汇总";
            this.RefreshCommand = new DelegateCommand(OnRefreshCommand);
            this.EditCommand = new DelegateCommand(OnEditCommand, CanExecute);
            this.AddNewCommand = new DelegateCommand(OnAddNewCommand);
            this.RemoveCommand = new DelegateCommand(OnRemoveCommand, CanExecute);
            this.ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            this.ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);
            OnRefreshCommand();

        }

        private void OnExportToPdfCommand()
        {
            base.ExportToPdf(SourceTbl, ModuleName);
        }

        private void OnExportToExcelCommand()
        {
            base.ExportToExcel(SourceTbl, ModuleName);

        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelDepositFee(this._selectedDepositFee.Id))
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
            var dlg = new AddDepositFeesDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.DepositFee = new  DepositFeeInfo { Id = Guid.NewGuid().ToString() };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();
        }

        private void OnEditCommand()
        {
            var dlg = new EditDepositFeesDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            dlg.ViewModel.DepositFee = this.SelectedDepositFeeInfo;
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();
        }

        private void OnRefreshCommand()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            Query(WhereName, () => Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }

        public void Query(string name, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        SourceTbl = Service.GetAllDepositFees();
                    }
                    else
                    {
                        SourceTbl = Service.GetDepositFeeByName(name);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        #endregion
    }
}
