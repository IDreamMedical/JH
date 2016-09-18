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
using UniGuy.Core.DataStructures;

namespace JinHong.View
{
    /// <summary>
    ///  抄电表数.xaml
    /// </summary>
    public partial class WpfPower : UserControl
    {


        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(抄电表数VM), typeof(WpfPower));

        #endregion

        #region Fields
        private string _moduleName = "抄电表数";

        //  TODO

        #endregion

        #region Properties

        public 抄电表数VM ViewModel
        {
            get { return (抄电表数VM)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors


        public WpfPower()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void dataGridPowerValues_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int month = e.Column.DisplayIndex - 1;
            ObservableTuple<string, double[]> temp = e.Row.DataContext as ObservableTuple<string, double[]>;
            double powerValue;
            if (double.TryParse(((TextBox)e.EditingElement).Text, out powerValue))
                ViewModel.SavePowerValue(temp.Item1, month, powerValue);
        }

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {

            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.AmmeterDataInfoTbl, _moduleName);
        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.AmmeterDataInfoTbl, _moduleName);

        }

        //  TODO

        #endregion
    }
}
