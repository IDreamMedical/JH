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

namespace JinHong.View
{
    /// <summary>
    /// 入住登记表
    /// </summary>
    public partial class WpfCheckIn : UserControl
    {
        #region public Properties
        public CheckInViewModel ViewModel { get; set; }
        #endregion

        #region Constructors

        public WpfCheckIn()
        {
            InitializeComponent();
            ViewModel = new CheckInViewModel(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }

        #endregion


    }
}
