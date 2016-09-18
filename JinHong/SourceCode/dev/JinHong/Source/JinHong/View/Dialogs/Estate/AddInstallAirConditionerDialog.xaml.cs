using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;
using JinHong.Model;
using System;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using JinHong.ViewModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 新增ContractInfo
    /// </summary>
    public partial class AddInstallAirConditionerDialog : Window
    {

        #region Public  ViewModel
        public NewOrEditInstallAirViewModel ViewModel { get; private set; }
        #endregion

        #region Constructors


        public AddInstallAirConditionerDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditInstallAirViewModel();
            ViewModel.Initialize();
            this.DataContext = ViewModel;


        }


        #endregion



        private void comboBoxSocialUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(CurrentInstallAirRecord.SocialUnitId))
            //{
            //    DataView dv = SocialUnitTbl.DefaultView;
            //    dv.RowFilter = string.Format(" SocialUnitId='{0}'", CurrentInstallAirRecord.SocialUnitId);
            //    AvailableRooms = dv.ToTable();

            //}
        }

    }
}
