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
using Microsoft.Win32;
using System.Diagnostics;
using UniGuy.Report;
using JinHong.View.Dialogs;

namespace JinHong.View
{
    /// <summary>
    /// 车位租赁信息表.xaml
    /// </summary>
    public partial class WpfRentalParking : UserControl
    {

        #region Public Prop

        public RentalParkingViewModel ViewModel { get; set; }

        #endregion

        #region Constructors

        public WpfRentalParking()
        {
            InitializeComponent();
            ViewModel = new RentalParkingViewModel(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }

        #endregion

        #region Methods

        #region Event handlers

        private void listViewParkingLotInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                    ViewModel.SelectedParkingLotRental = row.BuildEntity<ParkingLotRentalInfo>();
            }
        }


        //  上传图示, 只有具有权限的用户可用TODO...Set IsEnabled according to privileges...
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
                ViewModel.UploadImage(ofd.FileName);
            }
        }

        

        private void ParkingLotInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel.SelectedParkingLotRental == null)
                return;
            //ParkingLotInfoEditDialog dialog = new ParkingLotInfoEditDialog { Owner = Window.GetWindow(this) };//  TODO, 设置多个依赖属性, SocialUnit, LeasingInfo, ContractActivity集合等
            //dialog.SocialUnitInfo = GlobalVariables.Smc.Load<SocialUnitInfo>(ViewModel.SelectedLeasingStatusInfo.SocialUnitId);
            //dialog.ShowDialog();
            //  TODO
        }


        #endregion



        #endregion
    }
}
