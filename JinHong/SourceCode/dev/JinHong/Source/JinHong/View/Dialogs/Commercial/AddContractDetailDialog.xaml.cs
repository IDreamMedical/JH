using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;
using JinHong.Model;
using System;
using System.Data;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 添加ContractInfo
    /// </summary>
    public partial class AddContractDetailDialog : Window
    {

        #region Dependency properties

        public static readonly DependencyProperty CurrentContractInfoProperty = DependencyProperty.Register(
            "CurrentContractInfo", typeof(ContractInfo), typeof(AddContractDetailDialog));

        public static readonly DependencyProperty ContractInfoTblProperty = DependencyProperty.Register(
      "ContractInfoTbl", typeof(DataTable), typeof(AddContractDetailDialog));

        #endregion
        #region Fields
        private IList<ContractInfo> _contractInfos;

        //  TODO

        #endregion

        #region Properties

        public DataTable ContractInfoTbl
        {

            get { return (DataTable)GetValue(ContractInfoTblProperty); }
            set { SetValue(ContractInfoTblProperty, value); }

        }
        public ContractInfo CurrentContractInfo
        {
            get { return (ContractInfo)GetValue(CurrentContractInfoProperty); }
            set { SetValue(CurrentContractInfoProperty, value); }
        }
        /// <summary>
        /// ContractInfo名
        /// </summary>
        public string ContractInfoName { get; set; }

        #endregion

        #region Constructors


        public AddContractDetailDialog()
        {
            InitializeComponent();
            ContractInfoTbl = GlobalVariables.Smc.Select("select *from ContractInfo").Tables[0];

        }
        /// <summary>
        /// 添加ContractInfo,对数据库已有ContractInfo名进行验证,如果存在,则提示.
        /// </summary>
        /// <param employeeName="userList"></param>
        public AddContractDetailDialog(IList<ContractInfo> ContractInfos)
        {
            InitializeComponent();
            this._contractInfos = ContractInfos;
            CurrentContractInfo = new ContractInfo(Guid.NewGuid().ToString());

        }

        #endregion

        /// <summary>
        /// 确认添加
        /// </summary>
        /// <param employeeName="sender"></param>
        /// <param employeeName="e"></param>
        /// 
        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentContractInfo.Name))
            {
                MessageBox.Show("名称不能为空!", "添加合同", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (null != _contractInfos)
            {
                if (_contractInfos.FirstOrDefault(ac => ac.Name == ContractInfoName) != null)
                {
                    MessageBox.Show("名称已经存在!", "添加合同", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            //GlobalVariables.Smc.Insert<ContractInfo>(new ContractInfo()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = ContractInfoName,
            //    Description = this.txtDescription.Text.Trim()
            //});
            DialogResult = true;
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param employeeName="sender"></param>
        /// <param employeeName="e"></param>
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
