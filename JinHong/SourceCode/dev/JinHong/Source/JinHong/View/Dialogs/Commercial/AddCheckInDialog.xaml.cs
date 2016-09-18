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
    /// 入住登记表
    /// </summary>
    public partial class AddCheckInDialog : Window
    {
        #region Public  ViewModel
        public NewOrEditCheckInViewModel ViewModel { get; set; }
        #endregion


        #region Constructors

        public AddCheckInDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditCheckInViewModel();
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }

        #endregion

        #region Methods

        #region Event handlers


        private string oldBuildingId;
        private string oldSocialUnitId;
        private void comboBoxBuildingId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (e.AddedItems.Count > 0)
            //    buttonOK.IsEnabled = !string.IsNullOrEmpty(LeasingInfo.RoomId);
            //else
            //    buttonOK.IsEnabled = false;
            ////  TODO
        }

        private void comboBoxRoomId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (e.AddedItems.Count > 0)
            //    buttonOK.IsEnabled = !string.IsNullOrEmpty(LeasingInfo.BuildingId);
            //else
            //    buttonOK.IsEnabled = false;
        }


        private void comboBoxRoomId_GotFocus(object sender, RoutedEventArgs e)
        {
            //if (LeasingInfo.BuildingId != null && LeasingInfo.BuildingId != oldBuildingId)
            //{
            //    oldBuildingId = LeasingInfo.BuildingId;
            //    DataView dv = AvailableBuildingIdTbl.DefaultView;
            //    dv.RowFilter = string.Format("BuildingId='{0}'", LeasingInfo.BuildingId);
            //    AvailableRoomIdTbl = dv.ToTable();
            //    //var roomRows = AvailableBuildingIdTbl.Select(string.Format("BuildingId='{0}'", LeasingInfo.BuildingId));
            //    //if (roomRows != null && roomRows.Length > 0)
            //    //{
            //    //    string[] roomIds = new string[roomRows.Length];
            //    //    for (int i = 0; i < roomIds.Length; i++)
            //    //        roomIds[i] = roomRows[i][1] as string;
            //    //    AvailableRoomIds = roomIds;
            //    //}
            //}
        }

        #endregion



        #endregion

        private void comboBoxBuildingId_GotFocus(object sender, RoutedEventArgs e)
        {
//            if (LeasingInfo.SocialUnitId != null && LeasingInfo.SocialUnitId != oldSocialUnitId)
//            {
//                oldSocialUnitId = LeasingInfo.SocialUnitId;
//                DataSet ds = GlobalVariables.Smc.Select(string.Format(@"select a.BuildingId, e.Name as BuildingName  ,a.RoomId, c.Name as  RoomName  from ContractDetail  a  
//INNER JOIN ContractInfo  b  on  a.ContractId =b.Id 
//INNER JOIN RoomInfo  c   on  a.RoomId =c.Id 
//INNER join  BuildingInfo  e on  a.BuildingId=e.Id
//where b .SocialUnitId='{0}'", LeasingInfo.SocialUnitId));
//                if (ds != null)
//                {
//                    AvailableBuildingIdTbl = ds.Tables[0];
//                    //string[] buildingIds = new string[ds.Tables[0].Rows.Count];
//                    //for (int i = 0; i < buildingIds.Length; i++)
//                    //    buildingIds[i] = ds.Tables[0].Rows[i][0] as string;
//                    //AvailableBuildingIds = buildingIds;
//                }
//            }

        }

    }
}
