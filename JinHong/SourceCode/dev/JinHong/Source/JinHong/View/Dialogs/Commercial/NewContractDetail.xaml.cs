using JinHong.ViewModel;
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

namespace JinHong.View.Dialogs.Commercial
{
    /// <summary>
    /// NewOrEditContractDetail.xaml 的交互逻辑 添加租赁明细
    /// </summary>
    public partial class NewContractDetail : Window
    {


        #region Public Var

        public AddContractDetailVM ViewModel { get; set; }
        #endregion
        public NewContractDetail()
        {
            InitializeComponent();
            ViewModel = new AddContractDetailVM { ViewDialog = this };
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }

        private void cmbRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
