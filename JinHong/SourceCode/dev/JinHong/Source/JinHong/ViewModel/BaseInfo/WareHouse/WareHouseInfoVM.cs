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
using System.Windows.Threading;
using UniGuy.Commands;
using UniGuy.Corek;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 楼宇VM
    /// </summary>
    public class WareHouseInfoVM : BaseViewModel
    {



        #region private var

        private static readonly Lazy<IWareHouseService> lazy = new Lazy<IWareHouseService>(() => new WareHouseService());

        private WareHouseInfo _selectedWareHouseInfo;


        #endregion



        #region public var

        public static IWareHouseService Service { get { return lazy.Value; } }

        public WareHouseInfo SelectedWareHouseInfo
        {
            get { return _selectedWareHouseInfo; }
            set
            {
                if (_selectedWareHouseInfo != value)
                {
                    _selectedWareHouseInfo = value;
                    OnPropertyChanged("SelectedWareHouseInfo");

                }
            }
        }



        #endregion

        #region Constructors

        public WareHouseInfoVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion


        #region Methods




        #region Public Method
        public void Initialize()
        {
            this.ModuleName = "仓库管理列表";
            this.RefreshCommand = new DelegateCommand(OnRefreshCommand);
            this.EditCommand = new DelegateCommand(OnEditCommand, CanExecute);
            this.AddNewCommand = new DelegateCommand(OnAddNewCommand);
            this.RemoveCommand = new DelegateCommand(OnRemoveCommand, CanExecute);
            this.ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            this.ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);
            OnRefreshCommand();
        }
        #endregion


        #region Private Method
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
            var dlg = new AddOrEditWareHouseInfoDialog();
            dlg.ViewModel.WareHouse = new WareHouseInfo { Id = Guid.NewGuid().ToString() };
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();

        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelWareHouse(this.SelectedWareHouseInfo.Id))
            {
                MessageBox.Show("删除成功！", "系统提示");
                OnRefreshCommand();
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
            var dlg = new AddOrEditWareHouseInfoDialog();
            dlg.ViewModel.WareHouse = this.SelectedWareHouseInfo;
            dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();


        }

        public void Query(string roleName, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(roleName))
                    {
                        SourceTbl = Service.GetAllWareHouses();
                    }
                    else
                    {
                        SourceTbl = Service.GetWareHouseById(roleName);
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
