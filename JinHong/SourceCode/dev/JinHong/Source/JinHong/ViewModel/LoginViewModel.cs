using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UniGuy.Commands;
using UniGuy.Entity;

namespace JinHong.ViewModel
{
    public class LoginViewModel
    {

        private bool enableSystemUser = GlobalVariables.GetConfigurationSetting<bool>("login.systemLogin"); //  是否允许直接系统账户登陆, 在App.exe.config中, 主要用于测试, 发布后去掉该配置

        private LoginResult _loginResult;
        #region Prop

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>

        public string Password { get; set; }

        public Window ViewDialog
        {
            get;
            set;
        }


        #region Command

        /// <summary>
        /// 确定按钮
        /// </summary>
        public ICommand BtnOKCommand { get; set; }

        /// <summary>
        /// 取消按钮
        /// </summary>
        public ICommand BtnCancelCommand { get; set; }

        public LoginResult LoginResult { get { return _loginResult; } }
        #endregion
        #endregion


        #region Method

        public void Initialize()
        {

            if (GlobalVariables.GetConfigurationSetting<bool>("login.debugLogin")) //  是否自动填写系统用户名和密码
            {
                this.UserName = "sys";
                this.Password = "JinHong";
            }

            this.BtnOKCommand = new DelegateCommand(LoginOn);
            this.BtnCancelCommand = new DelegateCommand(Cancel);
        }


        private void LoginOn()
        {
            User user = null;
            bool success = false;
            string message = null;

            if (enableSystemUser && this.UserName == "sys")
            {
                user = new User(this.UserName, "JinHong");
                success = user.Verify(this.Password);
                if (!success)
                    message = "密码错误";
            }
            else
            {
                user = GlobalVariables.Smc.Load<User>(this.UserName);
                if (user == null)
                    message = "用户名不存在";
                else
                {
                    success = user.Verify(this.Password);
                    if (!success)
                        message = "密码错误";
                }
            }
            if (!success)
            {
                _loginResult = new LoginResult(success, message);
            }
        }

        private void Cancel()
        {


        }

        #endregion



    }

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
