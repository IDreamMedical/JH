using JinHong.Helper;
using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services;
using JinHong.View.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using UniGuy.Commands;
using UniGuy.Corek;
using UniGuy.Entity;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 收费项目VM
    /// </summary>
    public class FeeItemVM : BaseViewModel
    {


        #region Fields

        private static readonly Lazy<IFeeItemService> lazy = new Lazy<IFeeItemService>(() => new FeeItemService());
        private FeeItem _selectedFeeItem;




        #endregion

        #region Constructors

        public FeeItemVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion


        #region Properties


        public FeeItem SelectedFeeItem
        {
            get { return _selectedFeeItem; }
            set
            {
                if (_selectedFeeItem != value)
                {
                    _selectedFeeItem = value;

                    OnPropertyChanged("SelectedFeeItem");

                }
            }
        }

        public static IFeeItemService Service { get { return lazy.Value; } }



        #endregion


        #region Methods



        public void Initialize()
        {
            this.ModuleName = "费用项目管理";
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
            base.ExportToExcel(SourceTbl, ModuleName);
        }

        private void OnExportToExcelCommand()
        {
            base.ExportToPdf(SourceTbl, ModuleName);
        }

        private void OnAddNewCommand()
        {

            var dlg = new AddFeeItemDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.FeeItem = new FeeItem { Id = Guid.NewGuid().ToString(), Status = 0 };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();


        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelFeeItem(this.SelectedFeeItem.Id))
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
            var dlg = new AddFeeItemDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            dlg.ViewModel.FeeItem = this.SelectedFeeItem;
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
                        SourceTbl = Service.GetAllFeeItems();
                    }
                    else
                    {
                        SourceTbl = Service.GetFeeItemByName(name);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        #endregion

    }
}
