using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JinHong.ViewModel;
using System.Windows.Threading;
using UniGuy.Core.Data;
using JinHong.Model;
using System.Data;
using JinHong.Extensions;
using JinHong.View.Dialogs;
using UniGuy.Entity;
using System.Collections.ObjectModel;
using UniGuy.Core;
using UniGuy.Commands;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 角色.xaml
    /// </summary>
    public partial class UserInfoManagement : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(UserInfoVM), typeof(UserInfoManagement), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public UserInfoVM ViewModel
        {
            get { return (UserInfoVM)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        #region Properties


        #endregion


        //  TODO

        #endregion

        #region Constructors

        public UserInfoManagement()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region General

        public void Query()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            ViewModel.Query(ViewModel.WhereName, () => Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }



        private void Initialize()
        {
            ViewModel.RefreshCommand = new DelegateCommand(OnRefreshCommand);

            ViewModel.EditCommand = new DelegateCommand(OnEditCommand, CanExecute);
            ViewModel.AddNewCommand = new DelegateCommand(OnAddNewCommand);
            ViewModel.RemoveCommand = new DelegateCommand(OnRemoveCommand, CanExecute);
            ViewModel.ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            ViewModel.ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);
        }


        private void OnRefreshCommand()
        {
            this.Query();
        }
        private void OnExportToPdfCommand()
        {
            ViewModel.ExportToPdf(ViewModel.SourceTbl, _moduleName);
        }

        private void OnExportToExcelCommand()
        {
            ViewModel.ExportToExcel(ViewModel.SourceTbl, _moduleName);

        }

        private void OnAddNewCommand()
        {
            AddUserDialog ard = new AddUserDialog()
            {
                Owner = Window.GetWindow(this),
                CurrentUser = new User
                {
                    Id = Guid.NewGuid().ToString()
                }
            };
            if (ard.ShowDialog().GetValueOrDefault())
            {
                if (ViewModel.HasUser(string.Empty, ard.CurrentUser.UserName))
                {
                    MessageBox.Show("该用户已存在！", "系统提示");
                    return;
                }
                ard.CurrentUser.Status = 0;
                GlobalVariables.Smc.Insert<User>(ard.CurrentUser);

                Query();
            }
        }

        private void OnRemoveCommand()
        {
            if (ViewModel.SelectedUser != null)
            {
                if (MessageBox.Show("确定删除？ ", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    ViewModel.SelectedUser.Status = 1;
                    GlobalVariables.Smc.Update<User>(ViewModel.SelectedUser);
                }
                this.Query();
            }
        }

        private bool CanExecute()
        {
            return ViewModel.IsCanExecute;
        }

        private void OnEditCommand()
        {
           
        }

        #endregion

        #region Callbacks

        private static void ViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            UserInfoManagement @this = (UserInfoManagement)d;
            UserInfoVM viewModel = args.NewValue as UserInfoVM;
            if (viewModel != null)
            {
                @this.Query();
                @this.Initialize();
            }
        }

        #endregion

        #region Command handlers



        private string _moduleName = "用户管理";


        #endregion

        #region Event handlers

        private void listViewOnDutyEmployeeInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                {
                    ViewModel.SelectedUser = row.BuildEntity<User>();
                    ViewModel.IsCanExecute = true;
                    this.Initialize();
                }
            }
        }




        //  TODO

        #endregion

        #endregion
    }
}
