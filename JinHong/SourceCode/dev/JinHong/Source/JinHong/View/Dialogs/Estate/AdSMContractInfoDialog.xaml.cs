using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;
using JinHong.Model;
using System;
using System.Data;
using System.Collections.ObjectModel;
using JinHong.Extensions;
using JinHong.ViewModel;
using Microsoft.Win32;
using System.ComponentModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 新增ContractInfo
    /// </summary>
    public partial class AdSMContractInfoDialog : Window, INotifyPropertyChanged
    {

        #region Fields


        private SecurityManagementContractInfo currentSMContractInfo;


        private DataTable availableSocialUnits;

        private DataTable availableRooms;

        #endregion


        public AdSMContractInfoDialog()
        {
            InitializeComponent();
            CurrentSMContractInfo = new SecurityManagementContractInfo() { Id = Guid.NewGuid().ToString() };
        }

        #region Properties


        public SecurityManagementContractInfo CurrentSMContractInfo
        {
            get { return currentSMContractInfo; }
            set
            {
                if (currentSMContractInfo != value)
                {
                    currentSMContractInfo = value;
                    OnPropertyChanged("CurrentSMContractInfo");
                }
            }
        }
        public DataTable AvailableSocialUnits
        {
            get { return availableSocialUnits; }
            set
            {
                if (availableSocialUnits != value)
                {
                    availableSocialUnits = value;
                    OnPropertyChanged("AvailableSocialUnits");
                }
            }
        }




        public DataTable AvailableRooms
        {
            get { return availableRooms; }
            set
            {
                if (availableRooms != value)
                {

                    availableRooms = value;
                    OnPropertyChanged("AvailableRooms");
                }
            }

        }





        #endregion

        #region Event handlers
        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentSMContractInfo.FilePath))
            {
                MessageBox.Show("协议文件不能为空!", "新增安全协议", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            GlobalVariables.Smc.Insert<SecurityManagementContractInfo>(CurrentSMContractInfo);
            var temp = CurrentSMContractInfo.FilePath.LastIndexOf('.');
            var fileExtesion = CurrentSMContractInfo.FilePath.Substring(temp, CurrentSMContractInfo.FilePath.Length - temp);
            string remoteFileName = CurrentSMContractInfo.SocialUnitName + fileExtesion;
            GlobalVariables.Smc.UploadFile(remoteFileName, CurrentSMContractInfo.FilePath);
            DialogResult = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {

            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {

                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        private void comboBoxSocialUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (!string.IsNullOrEmpty(CurrentSMContractInfo.SocialUnitId))
            {
                string sql = string.Format(@"select c.Id ,c.Name,c.Area from  ContractDetail   a 
                                INNER join  ContractInfo  b  on a.ContractId=b.Id
                                inner join RoomInfo  c on c.Id=a.RoomId
                                where b.SocialUnitId='{0}'", CurrentSMContractInfo.SocialUnitId);
                var ds = GlobalVariables.Smc.Select(sql, null);
                AvailableRooms = ds == null ? null : ds.Tables[0];

            }

        }

        private void comboBoxRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentSMContractInfo.RoomId))
            {

                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                    CurrentSMContractInfo.Area = row["Area"] + "";

            }

        }

        private void AddFile_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CurrentSMContractInfo != null && !String.IsNullOrEmpty(CurrentSMContractInfo.RoomId);
        }

        private void AddFile_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "doc files (*.doc)|*.doc|docx files (*.docx)|*.docx|All files (*.*)|*.*", //过滤文件类型
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,  //设定初始目录
                ShowReadOnly = true
            };
            if (ofd.ShowDialog().GetValueOrDefault())
            {
                CurrentSMContractInfo.FilePath = ofd.FileName;
            }

        }


    }
}
