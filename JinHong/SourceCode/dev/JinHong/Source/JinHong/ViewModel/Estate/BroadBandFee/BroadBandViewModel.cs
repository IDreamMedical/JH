using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JinHong.Model;
using JinHong.Services;
using UniGuy.Commands;
using System.Windows.Threading;
using JinHong.Helper;
using System.Windows;
using UniGuy.Corek;
using JinHong.ServiceContract;
using JinHong.View.Dialogs;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 宽带费用清单
    /// </summary>
    public class BroadBandViewModel : BaseViewModel
    {
        #region Fields
        /// <summary>
        /// 选中的宽带费用
        /// </summary>
        private BroadBandFee _selectedBroadBandFee;

        private static readonly Lazy<IBroadBandService> lazy = new Lazy<IBroadBandService>(() => new BroadBandService());

        #endregion

        #region Properties

        public static IBroadBandService Service { get { return lazy.Value; } }

        /// <summary>
        /// 获得或者设置当前选中电信费信息
        /// </summary>
        public BroadBandFee SelectedBroadBandFee
        {
            get { return _selectedBroadBandFee; }
            set
            {
                if (_selectedBroadBandFee != value)
                {
                    _selectedBroadBandFee = value;
                    OnPropertyChanged("SelectedBroadBandFee");
                }
            }
        }




        #endregion

        #region Constructors

        public BroadBandViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            // WhereDate = DateTime.Today;
        }

        #endregion

        #region Methods



        public void Initialize()
        {
            this.ModuleName = "电信电话_电信收费清单";
            this.RefreshCommand = new DelegateCommand(OnRefreshCommand);
            this.EditCommand = new DelegateCommand(OnEditCommand, CanExecute);
            this.AddNewCommand = new DelegateCommand(OnAddNewCommand);
            this.RemoveCommand = new DelegateCommand(OnRemoveCommand, CanExecute);
            this.ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            this.ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);
            OnRefreshCommand();

        }

        private void OnRefreshCommand()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            Query(WhereName, () => Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));

        }

        private void OnExportToPdfCommand()
        {
            base.ExportToPdf(SourceTbl, ModuleName);
        }

        private void OnExportToExcelCommand()
        {
            base.ExportToExcel(SourceTbl, ModuleName);
        }

        private void OnAddNewCommand()
        {

            var dlg = new AddBroadBandFeeDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.BroadBandFee = new BroadBandFee { Id = Guid.NewGuid().ToString() };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();


        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelBroadBandFee(this.SelectedBroadBandFee.Id))
            {
                MessageBox.Show("删除成功！", "系统提示");
            }
            else
            {
                MessageBox.Show("删除失败！", "系统提示");
            }

        }

        public override bool CanExecute()
        {
            return this.IsCanExecute;
        }

        private void OnEditCommand()
        {
            var dlg = new AddBroadBandFeeDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            dlg.ViewModel.BroadBandFee = this.SelectedBroadBandFee;
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();

        }

        private void Query(string name, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        SourceTbl = Service.GetAllBroadBandFees();
                    }
                    else
                    {
                        SourceTbl = Service.GetBroadBandFeeByName(name);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        #endregion



    }
}
