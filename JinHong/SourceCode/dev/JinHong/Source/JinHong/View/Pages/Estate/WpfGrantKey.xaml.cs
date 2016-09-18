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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JinHong.View
{
    /// <summary>
    /// GrantKey.xaml 的交互逻辑
    /// </summary>
    public partial class WpfGrantKey : UserControl
    {
        #region public Properties
        public GrantKeyViewModel ViewModel { get; set; }
        #endregion

        public WpfGrantKey()
        {
            InitializeComponent();
            ViewModel = new GrantKeyViewModel(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }

        private void listViewGrantKeyRecordTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
