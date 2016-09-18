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
using System.Collections.ObjectModel;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for WpfProbe.xaml
    /// </summary>
    public partial class WpfProbe : UserControl
    {
        #region public Properties
        public ProbeViewModel ViewModel { get; set; }
        #endregion

        #region Constructors
        public WpfProbe()
        {
            InitializeComponent();
            ViewModel = new ProbeViewModel(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }
        #endregion

        #region Event handlers

        private void listViewMonitorProbeTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                    ViewModel.SelectedMonitorProbe = row.BuildEntity<MonitorProbe>();
                    ViewModel.IsCanExecute = true;
                    ViewModel.Initialize();
                }
            }
        }

        #endregion

    }
}
