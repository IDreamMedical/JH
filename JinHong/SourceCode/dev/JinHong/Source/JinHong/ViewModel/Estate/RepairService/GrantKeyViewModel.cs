using JinHong.Helper;
using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services;
using JinHong.View.Dialogs;
using System;
using System.Collections.Generic;
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
    /// 钥匙发放ViewModel
    /// </summary>
    public class GrantKeyViewModel : BaseViewModel
    {
        #region Fields
        //  当前选中电信费信息
        private GrantKey _selectedGrantKey;
        private static readonly Lazy<IGrantKeyService> lazy = new Lazy<IGrantKeyService>(() => new GrantKeyService());

        #endregion

        #region Properties

        public static IGrantKeyService Service { get { return lazy.Value; } }

        /// <summary>
        /// 获得或者设置当前选中电信费信息
        /// </summary>
        public GrantKey SelectedGrantKey
        {
            get { return _selectedGrantKey; }
            set
            {
                if (_selectedGrantKey != value)
                {
                    _selectedGrantKey = value;
                    OnPropertyChanged("SelectedGrantKey");
                }
            }
        }
        #endregion

        #region Constructors

        public GrantKeyViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            // WhereDate = DateTime.Today;
        }

        #endregion

        #region Methods



        public void Initialize()
        {
            this.ModuleName = "空调安装记录登记表";
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

            var dlg = new WpfGrantKeyDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.GrantKey = new GrantKey { Id = Guid.NewGuid().ToString() };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();


        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelGrantKey(this.SelectedGrantKey.Id))
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
            var dlg = new WpfGrantKeyDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            dlg.ViewModel.GrantKey = this.SelectedGrantKey;
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
                        SourceTbl = Service.GetAllGrantKeys();
                    }
                    else
                    {
                        SourceTbl = Service.GetGrantKeyByName(name);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        #endregion
    }
}
