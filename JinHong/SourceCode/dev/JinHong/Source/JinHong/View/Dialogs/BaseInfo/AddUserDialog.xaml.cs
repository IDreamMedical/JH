using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;
using System.ComponentModel;
using JinHong.Models;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 新增用户
    /// </summary>
    public partial class AddUserDialog : Window, INotifyPropertyChanged
    {

        #region Fields
        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    OnPropertyChanged("CurrentUser");
                }
            }
        }


        #endregion

        public AddUserDialog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 确认新增
        /// </summary>
        /// <param employeeName="sender"></param>
        /// <param employeeName="e"></param>
        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentUser.UserName))
            {
                MessageBox.Show("用户名不能为空!", "新增用户", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.textBoxUserName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(passwordBoxPassword.Password))
            {
                MessageBox.Show("密码不能为空!", "新增用户", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.passwordBoxPassword.Focus();
                return;
            }
            if (passwordBoxPassword.Password != passwordBoxPasswordConfirmed.Password)
            {
                MessageBox.Show("两次密码输入不一致!", "新增用户", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.passwordBoxPassword.Focus();
                return;
            }
            CurrentUser.Password = this.passwordBoxPassword.Password;
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {

            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {

                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }

    }
}
