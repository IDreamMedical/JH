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
using JinHong.View.Dialogs;
using System.Windows.Threading;
using System.Data;
using JinHong.Extensions;
using JinHong.Model;

namespace JinHong.View
{
    /// <summary>
    /// 安装空调设备
    /// </summary>
    public partial class WpfInstallAir : UserControl
    {
        #region Public  ViewModel
        public InstallAirViewModel ViewModel { get; set; }
        #endregion

        #region Constructors

        public WpfInstallAir()
        {
            InitializeComponent();
            ViewModel = new InstallAirViewModel(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }

        #endregion

        #region Methods


     


    

        private void listViewInstallAirRecordTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                    ViewModel.SelectedInstallAir = row.BuildEntity<InstallAirRecord>();
            }
        }


        #endregion
    }
}
