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
using System.Threading.Tasks;

namespace JinHong.View
{
    /// <summary>
    /// 企业首页信息。主要包括企业基本信息和一些租赁，缴费信息
    /// </summary>
    public partial class WpfCompanyIndex : UserControl
    {

        #region Properties

        public SocialUnitIndexViewModel ViewModel { get; set; }
        #endregion

        #region Constructors

        public WpfCompanyIndex()
        {
            InitializeComponent();
            ViewModel = new SocialUnitIndexViewModel(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }

        #endregion

        #region Event handlers

        private void listViewLeasingStatusInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                    ViewModel.SelectedSocialUnit = row.BuildEntity<SocialUnitInfo>();
            }
        }

        #endregion
    }
}

