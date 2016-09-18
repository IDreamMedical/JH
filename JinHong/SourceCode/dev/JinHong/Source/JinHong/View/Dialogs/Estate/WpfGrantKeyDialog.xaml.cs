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

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// WpfGrantKeyDialog.xaml 的交互逻辑 钥匙发放
    /// </summary>
    public partial class WpfGrantKeyDialog : Window
    {

        #region Public  ViewModel
        public NewOrEditGrantKeyViewModel ViewModel { get; private set; }
        #endregion
        public WpfGrantKeyDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditGrantKeyViewModel();
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }
    }
}
