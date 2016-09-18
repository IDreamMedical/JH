using JinHong.Helper;
using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services;
using JinHong.View.Dialogs;
using Microsoft.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using UniGuy.Commands;
using UniGuy.Corek;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 楼宇VM
    /// </summary>
    public class BuildingInfoVM : BaseViewModel
    {

        #region Fields

        private BuildingInfo _selectedBuilding;
        private static readonly Lazy<IBuildingService> lazy = new Lazy<IBuildingService>(() => new BuildingService());


        #endregion

        #region Properties


        public BuildingInfo SelectedBuilding
        {
            get { return _selectedBuilding; }
            set
            {
                if (_selectedBuilding != value)
                {
                    _selectedBuilding = value;
                    OnPropertyChanged("SelectedBuilding");

                }
            }
        }
        public static IBuildingService Service { get { return lazy.Value; } }



        #endregion

        #region Constructors

        public BuildingInfoVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion

        #region Methods

        private void Query(string name, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        SourceTbl = Service.GetAllBuildings();
                    }
                    else
                    {
                        SourceTbl = Service.GetBuildingByName(name);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        #region Methods



        public void Initialize()
        {
            this.ModuleName = "楼宇管理列表";
            this.RefreshCommand = new DelegateCommand(OnRefreshCommand);
            this.EditCommand = new DelegateCommand(OnEditCommand, CanExecute);
            this.AddNewCommand = new DelegateCommand(OnAddNewCommand);
            this.RemoveCommand = new DelegateCommand(OnRemoveCommand, CanExecute);
            this.ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            this.ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);
            this.SelectItemChangedCommand = new DelegateCommand<DataRow>(OnSelectItemChangedCommand); 
            OnRefreshCommand();

        }

        /// <summary>
        /// 行改变的时候
        /// </summary>
        private void OnSelectItemChangedCommand(DataRow row)
        {
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

            var dlg = new AddBuildingInfoDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.Building = new BuildingInfo { Id = Guid.NewGuid().ToString(), Status = "0" };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();


        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelBuilding(this.SelectedBuilding.Id))
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
            var dlg = new AddBuildingInfoDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            dlg.ViewModel.Building = this.SelectedBuilding;
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();

        }



        #endregion


        #endregion

    }
}
