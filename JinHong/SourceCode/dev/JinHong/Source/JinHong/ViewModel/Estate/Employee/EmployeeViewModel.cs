using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JinHong.Model;
using JinHong.Services.Estate;
using JinHong.ServiceContract;
using UniGuy.Commands;
using System.Windows.Threading;
using UniGuy.Corek;
using JinHong.View.Dialogs;
using JinHong.Helper;
using System.Windows;

namespace JinHong.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {
         #region Fields
        /// <summary>
        /// 选中的EmployeeInfo
        /// </summary>
        private EmployeeInfo _selectedEmployee;
        /// <summary>
        /// 服务
        /// </summary>
        private static readonly Lazy<IEmployeeService> _lazy = new Lazy<IEmployeeService>(() => new EmployeeService());


        

        #endregion

        #region Properties

        public static IEmployeeService Service { get { return _lazy.Value; } }

        /// <summary>
        /// 获得或者设置当前选中电信费信息
        /// </summary>
        public EmployeeInfo SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (_selectedEmployee != value)
                {
                    _selectedEmployee = value;
                    OnPropertyChanged("SelectedEmployee");
                }
            }
        }
        #endregion

        #region Constructors

        public EmployeeViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
          
        }

        #endregion

        #region Methods



        public void Initialize()
        {
            this.ModuleName = "职工名单表";
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

            var dlg = new WpfEmployeeDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.Employee = new EmployeeInfo { Id = Guid.NewGuid().ToString() };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();


        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelEmployee(this.SelectedEmployee.Id))
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
            var dlg = new WpfEmployeeDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            dlg.ViewModel.Employee = this.SelectedEmployee;
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
                        SourceTbl = Service.GetAllEmployees();
                    }
                    else
                    {
                        SourceTbl = Service.GetEmployeeByName(name);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        #endregion
    }
}
