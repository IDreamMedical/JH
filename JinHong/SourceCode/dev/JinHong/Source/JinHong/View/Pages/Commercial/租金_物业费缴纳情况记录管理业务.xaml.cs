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
using System.Data;
using JinHong.Extensions;
using System.Windows.Threading;
using UniGuy.Core.Data;
using JinHong.Model;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 租金_物业费缴纳情况记录管理业务.xaml
    /// </summary>
    public partial class 租金_物业费缴纳情况记录管理业务 : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(租金_物业费缴纳情况记录管理业务VM), typeof(租金_物业费缴纳情况记录管理业务));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public 租金_物业费缴纳情况记录管理业务VM ViewModel
        {
            get { return (租金_物业费缴纳情况记录管理业务VM)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors


        public 租金_物业费缴纳情况记录管理业务()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Command handlers

        //  Add
        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO...根据权限
            e.CanExecute = ViewModel != null;
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO
            throw new NotImplementedException();
        }

        //  Remove
        private void Remove_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO...根据权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedEstateRentPriceInfo != null;
        }

        private void Remove_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO
            throw new NotImplementedException();
        }

        #endregion

        #region Event handlers

        private void buttonQuery_Click(object sender, RoutedEventArgs e)
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            ViewModel.Query(() => Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }

        private void listViewEstateRentPriceInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = e.AddedItems[0] as DataRow;
                if (row != null)
                    ViewModel.SelectedEstateRentPriceInfo = row.BuildEntity<EstateRentPriceInfo>();
            }
        }

        #endregion

        //  TODO

        #endregion
    }
}
