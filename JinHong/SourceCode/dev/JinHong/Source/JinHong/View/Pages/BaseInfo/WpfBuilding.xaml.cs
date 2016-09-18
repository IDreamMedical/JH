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
using System.Windows.Threading;
using UniGuy.Core.Data;
using JinHong.Model;
using System.Data;
using JinHong.Extensions;
using JinHong.View.Dialogs;
using System.Collections.ObjectModel;
using UniGuy.Commands;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 楼宇信息.xaml
    /// </summary>
    public partial class WpfBuilding : UserControl
    {

        #region Properties

        public BuildingInfoVM ViewModel { get; set; }


        #endregion


        #region Constructors

        public WpfBuilding()
        {
            InitializeComponent();
            ViewModel = new BuildingInfoVM(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }

        #endregion

        private void listViewBuildingInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                    ViewModel.SelectedBuilding = row.BuildEntity<BuildingInfo>();
                    ViewModel.IsCanExecute = true;
                    ViewModel.Initialize();
                }
            }
        }
    }
}
