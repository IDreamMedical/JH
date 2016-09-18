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
using JinHong.View.Dialogs;
using UniGuy.Report;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using UniGuy.Core.Extensions;
using UniGuy.Commands;

namespace JinHong.View
{
    /// <summary>
    /// 电信电话_电信收费清单.xaml
    /// </summary>
    public partial class BroadBand : UserControl
    {

        #region public Properties
        public BroadBandViewModel ViewModel { get; set; }
        #endregion

        #region Constructors

        public BroadBand()
        {
            InitializeComponent();
            ViewModel = new BroadBandViewModel(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }

        #endregion

        #region Event handlers

        private void listViewTelecomFeesInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                    ViewModel.SelectedBroadBandFee = row.BuildEntity<BroadBandFee>();
                    ViewModel.IsCanExecute = true;
                    ViewModel.Initialize();
                }
            }
        }



        #endregion

    }
}
