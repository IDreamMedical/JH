using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 新增权限
    /// </summary>
    public partial class AddPrivilegeDialog : Window
    {
        private IList<Privilege> _privileges;
        /// <summary>
        /// 权限名
        /// </summary>
        public string PrivilegeName { get; set; }
        /// <summary>
        /// 新增权限,对数据库已有权限名进行验证,如果存在,则提示.
        /// </summary>
        /// <param employeeName="userList"></param>
        public AddPrivilegeDialog(IList<Privilege> privileges)
        {
            InitializeComponent();
            this._privileges = privileges;
        }
        /// <summary>
        /// 确认新增
        /// </summary>
        /// <param employeeName="sender"></param>
        /// <param employeeName="e"></param>
        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PrivilegeName))
            {
                MessageBox.Show("权限名不能为空!", "新增权限", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.txtName.Focus();
                return;
            }
            if (null != _privileges)
            {
                if (_privileges.FirstOrDefault(ac => ac.Name == PrivilegeName) != null)
                {
                    MessageBox.Show("权限名已经存在!", "新增权限", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.txtName.Focus();
                    return;
                }
            }
            //GlobalVariables.Smc.Insert<Role>(new Role() { Name = PrivilegeName,Description=this.txtDescription.Text.Trim() });
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
