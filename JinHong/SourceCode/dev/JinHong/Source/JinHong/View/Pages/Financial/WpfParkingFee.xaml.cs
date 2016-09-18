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

namespace JinHong.View
{
    /// <summary>
    /// 停车费汇总的逻辑
    /// </summary>
    public partial class WpfParkingFee : UserControl
    {

        #region public Prop
        public ParkingFeeViewModel ViewModel { get; set; }
        #endregion

        #region Constructors
        public WpfParkingFee()
        {
            InitializeComponent();
            ViewModel = new ParkingFeeViewModel(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }
        #endregion

    }
}
