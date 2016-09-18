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
using Microsoft.Win32;
using UniGuy.Report;
using System.Diagnostics;

namespace JinHong.View
{
    /// <summary>
    /// 交纳押金
    /// </summary>
    public partial class WpfDepositFee : UserControl
    {

        #region public Prop
        public DepositFeeViewModel ViewModel { get; set; }
        #endregion
        #region Constructors


        public WpfDepositFee()
        {
            InitializeComponent();
            ViewModel = new DepositFeeViewModel(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }

        #endregion


        #region Event handlers


        private void listViewDepositFeesInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                    ViewModel.SelectedDepositFeeInfo = row.BuildEntity<DepositFeeInfo>();
            }
        }

        #endregion

    }
}
