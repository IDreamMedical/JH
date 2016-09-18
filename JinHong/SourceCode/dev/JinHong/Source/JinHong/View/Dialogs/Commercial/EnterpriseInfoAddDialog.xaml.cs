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

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// EnterpriseInfoAddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class EnterpriseInfoAddDialog : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty EnterpriseInfoProperty = DependencyProperty.Register(
            "EnterpriseInfo", typeof(EnterpriseInfo), typeof(EnterpriseInfoAddDialog));

        public static readonly DependencyProperty AvailableBuildingIdsProperty = DependencyProperty.Register(
            "AvailableBuildIds", typeof(IEnumerable<string>), typeof(EnterpriseInfoAddDialog));

        public static readonly DependencyProperty AvailableRoomIdsProperty = DependencyProperty.Register(
            "AvailableRoomIds", typeof(IEnumerable<string>), typeof(EnterpriseInfoAddDialog));


        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public EnterpriseInfo EnterpriseInfo
        {
            get { return (EnterpriseInfo)GetValue(EnterpriseInfoProperty); }
            set { SetValue(EnterpriseInfoProperty, value); }
        }

        public IEnumerable<string> AvailableBuildingIds
        {
            get { return (IEnumerable<string>)GetValue(AvailableBuildingIdsProperty); }
            set { SetValue(AvailableBuildingIdsProperty, value); }
        }

        public IEnumerable<string> AvailableRoomIds
        {
            get { return (IEnumerable<string>)GetValue(AvailableRoomIdsProperty); }
            set { SetValue(AvailableRoomIdsProperty, value); }
        }

        #endregion

        #region Constructors

        public EnterpriseInfoAddDialog()
        {
            AvailableBuildingIds = GlobalVariables.Smc.GetColumnData<BuildingInfo>("Id");

            InitializeComponent();

            textBoxSocialUnitName.Focus();
        }

        #endregion

        #region Methods

        #region Event handlers

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private string oldBuildingId;
        private void comboBoxBuildingId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                buttonOK.IsEnabled = !string.IsNullOrEmpty(e.AddedItems[0] as string) && !string.IsNullOrEmpty(EnterpriseInfo.RoomId);
            else
                buttonOK.IsEnabled = false;
            //  TODO
        }

        private void comboBoxRoomId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
                buttonOK.IsEnabled = !string.IsNullOrEmpty(EnterpriseInfo.BuildingId) && !string.IsNullOrEmpty(e.AddedItems[0] as string);
            else
                buttonOK.IsEnabled = false;
        }
        

        private void comboBoxRoomId_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EnterpriseInfo.BuildingId != null && EnterpriseInfo.BuildingId != oldBuildingId)
            {
                oldBuildingId = EnterpriseInfo.BuildingId;
                DataSet ds = GlobalVariables.Smc.Select(string.Format("SELECT Id FROM RoomInfo Where BuildingId = '{0}'", EnterpriseInfo.BuildingId));
                if (ds != null)
                {
                    string[] roomIds = new string[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < roomIds.Length; i++)
                        roomIds[i] = ds.Tables[0].Rows[i][0] as string;
                    AvailableRoomIds = roomIds;
                }
            }
        }

        #endregion

        

        #endregion
    }
}
