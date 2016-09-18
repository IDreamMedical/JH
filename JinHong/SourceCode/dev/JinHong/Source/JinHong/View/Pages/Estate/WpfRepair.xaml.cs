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
using System.Windows.Threading;
using JinHong.View.Dialogs;
using JinHong.Model;
using JinHong.Extensions;


namespace JinHong.View
{
    /// <summary>
    /// 维修记录.xaml
    /// </summary>
    public partial class WpfRepair : UserControl
    {
        #region public Properties
        public RepairViewModel ViewModel { get; set; }
        #endregion

        #region Constructors

        public WpfRepair()
        {
            InitializeComponent();
            ViewModel = new RepairViewModel(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }

        #endregion

        private void listViewRepairRecordTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
