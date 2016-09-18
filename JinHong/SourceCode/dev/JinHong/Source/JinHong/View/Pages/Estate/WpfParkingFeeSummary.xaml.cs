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
using UniGuy.Core.Extensions;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 停车费统计表.xaml
    /// </summary>
    public partial class WpfParkingFeeSummary : UserControl
    {
        #region public Properties
        public ParkingFeeSummaryViewModel ViewModel { get; set; }
        #endregion

        #region Constructors

        public WpfParkingFeeSummary()
        {
            InitializeComponent();
            ViewModel = new ParkingFeeSummaryViewModel(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }

        #endregion
    }
}
