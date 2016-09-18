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
using Microsoft.Win32;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 平面图.xaml
    /// </summary>
    public partial class 平面图 : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(ParkingLotGraphViewModel), typeof(平面图));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public ParkingLotGraphViewModel ViewModel
        {
            get { return (ParkingLotGraphViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public 平面图()
        {
            InitializeComponent();
        }

        #endregion

        private void buttonChangeFigure_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Jpg files (*.jpg)|*.jpg|Gif files (*.gif)|Png files (*.png)|*.jpg|*.jpg|All files (*.*)|*.*", //过滤文件类型
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,  //设定初始目录
                ShowReadOnly = true
            };
            if (ofd.ShowDialog(Window.GetWindow(this)).GetValueOrDefault())
            {
               // ViewModel.UploadImage(ofd.FileName);
            }

        }

        #region Methods

        //  TODO

        #endregion
    }
}
