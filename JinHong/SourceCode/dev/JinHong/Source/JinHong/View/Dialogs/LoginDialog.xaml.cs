using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace JinHong.View.Dialogs
{
	/// <summary>
	/// 登陆窗体
	/// </summary>
	public partial class LoginDialog : Window
    {
        #region Dependency properties

        //  用户名
        public static readonly DependencyProperty UsernameProperty = DependencyProperty.Register(
            "Username", typeof(string), typeof(LoginDialog));

        //  密码
        public static readonly DependencyProperty PasswordProperty
            = DependencyProperty.Register("Password", typeof(string), typeof(LoginDialog));

        #endregion

        #region Fields

        public Func<string, string, LoginResult> LoginFunc = null;

        //  TODO

        #endregion

        #region Properties

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 初始化
        /// </summary>
        public LoginDialog()
		{
			this.InitializeComponent();

            textBoxUsername.SelectAll();
            textBoxUsername.Focus();
		}

        #endregion

        #region Methods

        #region Event handlers

        public void SetPassword(string password)
        {
            this.Password = passwordBox.Password;
            passwordBox.Password = password;
        }

        /// <summary>
        /// 取消登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void buttonCancel_Click(object sender, RoutedEventArgs e)
		{
            DialogResult = false;
		}
        /// <summary>
        /// 登陆并验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void buttonLogin_Click(object sender, RoutedEventArgs e)
		{
            if (LoginFunc == null)
                throw new ArgumentNullException();
            LoginResult result = LoginFunc(Username, Password);
            if (!result.Success)
            {
                MessageBox.Show(this, result.Message, "登陆", MessageBoxButton.OK);
                return;
            }
            DialogResult = true;
        }

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordBox.SelectAll();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.Password = passwordBox.Password;
        }

        #endregion

        #endregion

        #region Internal types

        public class LoginResult
        {
            public static LoginResult SuccessResult = new LoginResult { Success = true };
            public bool Success { get; set; }
            public string Message { get; set; }

            public LoginResult() { }
            public LoginResult(bool success, string message = null)
            {
                this.Success = success;
                this.Message = message;
            }
        }

        #endregion

       

        
    }
}