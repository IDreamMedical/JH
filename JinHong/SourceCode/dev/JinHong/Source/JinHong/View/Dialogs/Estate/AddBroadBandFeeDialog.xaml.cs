using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;
using JinHong.Model;
using System;
using System.ComponentModel;
using JinHong.ViewModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 添加宽带费用
    /// </summary>
    public partial class AddBroadBandFeeDialog : Window
    {


        #region Public  ViewModel
        public NewOrEditBroadBandFeeViewModel ViewModel { get; set; }
        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <param employeeName="userList"></param>
        public AddBroadBandFeeDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditBroadBandFeeViewModel();
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }
        
       
       

        private void comboBoxSocialUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
//            if (!string.IsNullOrEmpty(ViewModel.BroadBandFee.SocialUnitId))
//            {
//                try
//                {
//                    var ds = GlobalVariables.Smc.Select(string.Format(@"select a.*from  RoomInfo a 
//                                                                        INNER join ContractDetail b  on a.Id=b.RoomId
//                                                                        INNER join  ContractInfo  c  on  b.ContractId=c.Id
//                                                                        where    c.SocialUnitId='{0}' and  a.Id not in 
//                                                                        (SELECT  RoomId from  TelecomFeesInfo where SocialUnitId='{0}' )",
//                                                                         CurrentTelecomFeesInfo.SocialUnitId));
//                     = ds == null ? null : ds.Tables[0];
//                }
//                catch (Exception)
//                {

//                    throw;
//                }

//            }
        }
    }
}
