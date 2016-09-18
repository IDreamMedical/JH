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
using System.Windows.Shapes;
using System.Data;

using JinHong.Model;
using System.ComponentModel;
using JinHong.ViewModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// LeaseAddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddRoomDialog : Window
    {

        public NewOrEditRoomViewModel ViewModel { get; private set; }

        #region Constructors

        public AddRoomDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditRoomViewModel { ViewDialog = this };
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }

        #endregion

        #region Methods

        #region Event handlers

        private void comboBoxBuildingId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                btnOK.IsEnabled = e.AddedItems[0] != null;
            else
                btnCancel.IsEnabled = false;
        }
        #endregion



        #endregion


    }
}
