using System.Windows;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Text;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 修改用户密码
    /// </summary>
    public partial class ChangePasswordDialog : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty UserProperty
            = DependencyProperty.Register("User", typeof(User), typeof(ChangePasswordDialog));

        #endregion

        #region Properties
        public User User
        {
            get { return (User)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }
        #endregion

        #region Constructors
        public ChangePasswordDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        #region Event handlers
        /// <summary>
        /// 确认修改
        /// </summary>
        /// <param employeeName="sender"></param>
        /// <param employeeName="e"></param>
        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordBoxNew.Password))
            {
                MessageBox.Show("新密码不能为空!", "修改密码", MessageBoxButton.OK, MessageBoxImage.Warning);
                passwordBoxNew.Focus();
                passwordBoxNew.SelectAll();
                return;
            }
            if (passwordBoxNew.Password!=passwordBoxNewConfirmed.Password)
            {
                MessageBox.Show("两次密码输入不一致!", "修改密码", MessageBoxButton.OK, MessageBoxImage.Warning);
                passwordBoxNew.Focus();
                passwordBoxNew.SelectAll();
                return;
            }
            if (!User.Verify(passwordBoxOld.Password))
            {
                MessageBox.Show("原密码输入错误!", "修改密码", MessageBoxButton.OK, MessageBoxImage.Warning);
                passwordBoxOld.Focus();
                passwordBoxOld.SelectAll();
                return;
            }
            User.Password = passwordBoxNew.Password;
            GlobalVariables.Smc.Save<User>(User);
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

        #endregion

        #endregion
    }
}
