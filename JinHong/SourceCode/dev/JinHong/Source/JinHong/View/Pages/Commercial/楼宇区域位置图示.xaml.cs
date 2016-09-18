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
    /// Interaction logic for 楼宇区域位置图示.xaml
    /// </summary>
    public partial class 楼宇区域位置图示 : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(ParkGraphViewModel), typeof(楼宇区域位置图示), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public ParkGraphViewModel ViewModel
        {
            get { return (ParkGraphViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public 楼宇区域位置图示()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region General
        private void SetLocalImagePath()
        {
            ViewModel.SetLocalImagePath();
        }

        #endregion


        #region Callbacks

        private static void ViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            楼宇区域位置图示 @this = (楼宇区域位置图示)d;
            ParkGraphViewModel viewModel = args.NewValue as ParkGraphViewModel;
            if (viewModel != null)
                @this.SetLocalImagePath();
        }

        #endregion


        //  上传图示, 只有具有权限的用户可用TODO...Set IsEnabled according to privileges...
        private void buttonChangeFigure_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Jpg文件(*.jpg)|*.jpg|Gif文件(*.gif)|*.gif|Png文件(*.png)|*.png|所有文件(*.*)|*.*", //过滤文件类型
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,  //设定初始目录
                ShowReadOnly = true
            };
            if (ofd.ShowDialog(Window.GetWindow(this)).GetValueOrDefault())
            {
                ViewModel.UploadImage(ofd.FileName);
            }
        }
        #endregion
    }
}
