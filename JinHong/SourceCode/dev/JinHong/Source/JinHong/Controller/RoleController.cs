using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinHong.Interfaces.ViewModels;
using UniGuy.Commands;
using JinHong.ViewModel;
using JinHong.View;
using System.Windows;

namespace JinHong.Controller
{
    public class RoleController : AppBaseController, IRoleController
    {
        private readonly IRoleViewModel _roleViewModel;

        public RoleController(IRoleViewModel roleViewModel)
        {
            base.Initialize();

            LoadDataTemplate();
            // _roleViewModel = new RoleViewModel();
            Initialize();
        }

        private void LoadDataTemplate()
        {
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("/JinHong;component/DataTemplate/ViewDataTemplate.xaml", UriKind.Relative)
            });
        }



        public override void Initialize()
        {
            _roleViewModel.AddNewCommand = new DelegateCommand(OnAddNewCommand);
            _roleViewModel.EditCommand = new DelegateCommand(OnEditCommand);
            _roleViewModel.RemoveCommand = new DelegateCommand(OnRemoveCommand);
            _roleViewModel.ExportCommand = new DelegateCommand(OnExportCommand);

        }

        private void OnExportCommand()
        {


        }

        private void OnRemoveCommand()
        {

        }

        private void OnEditCommand()
        {

        }

        public override void Run()
        {
            throw new NotImplementedException();
        }

        public override void Shutdown()
        {
            throw new NotImplementedException();
        }


        #region Private


        private void OnAddNewCommand()
        {





        }
        #endregion



    }
}
