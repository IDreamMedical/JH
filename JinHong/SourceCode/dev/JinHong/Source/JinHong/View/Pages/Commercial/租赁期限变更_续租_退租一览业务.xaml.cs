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

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 租赁期限变更_续租_退租一览业务.xaml
    /// </summary>
    public partial class 租赁期限变更_续租_退租一览业务 : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(RentalLogViewModel), typeof(租赁期限变更_续租_退租一览业务));

        #endregion

        #region Fields

        private string _moduleName = "租赁期限变更_续租_退租一览业务";


        #endregion

        #region Properties

        public RentalLogViewModel ViewModel
        {
            get { return (RentalLogViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public 租赁期限变更_续租_退租一览业务()
        {
            InitializeComponent();
        }

        #endregion


        #region Methods

        #region Event handlers
        private void View_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void View_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Edit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Register_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void Register_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void buttonQuery_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listViewLeasingInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LeasingInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //  TODO...根据权限View
            //LeasingStatusViewDialog dialog = new LeasingStatusViewDialog { Owner = Window.GetWindow(this) };//  TODO, 设置多个依赖属性, SocialUnit, LeasingInfo, ContractActivity集合等
            //dialog.SocialUnitInfo = GlobalVariables.Smc.Load<SocialUnitInfo>(ViewModel.SelectedLeasingStatusInfo.SocialUnitId);
            //dialog.ShowDialog();
        }
        #endregion

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.LeaseTbl1, _moduleName);

        }


        #endregion

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.LeaseTbl1, _moduleName);

        }
    }
}
