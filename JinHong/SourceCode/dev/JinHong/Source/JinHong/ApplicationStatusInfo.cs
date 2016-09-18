using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Entity;
using UniGuy.Windows.Model;

namespace JinHong
{
    /// <summary>
    /// 应用程序运行时参数, 一般是不持久化的内容
    /// </summary>
    public class ApplicationStatusInfo:AbstractApplicationStatusInfo
    {
        #region Fields

        //  登录用户
        private User user;
        //  登录时间
        private DateTime loginTime;

        #endregion

        #region Properties
        /// <summary>
        /// 获得或者设置登陆的账户
        /// </summary>
        public User User
        {
            get { return user; }
            set
            {
                if (user != value)
                {
                    user = value;
                    OnPropertyChanged("User");
                    LoginTime = DateTime.Now;
                }
            }
        }
        /// <summary>
        /// 获得或者设置登陆的时间
        /// </summary>
        public DateTime LoginTime
        {
            get { return loginTime; }
            set
            {
                if (loginTime != value)
                {
                    loginTime = value;
                    OnPropertyChanged("LoginTime");
                }
            }
        }

        #endregion

        //  TODO
    }
}
