using JinHong.Core;
using JinHong.ServiceContract;
using JinHong.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UniGuy.Entity;
using JinHong.Extensions;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 当前登录账户ViewModel
    /// </summary>
    public class AccountViewModel
    {

        #region Prop

        /// <summary>
        /// 登录账户Id
        /// </summary>
        public string AccountId { get; private set; }


        /// <summary>
        /// 登录账户Id
        /// </summary>
        public string AccountName { get; private set; }

        /// <summary>
        /// 职工信息
        /// </summary>
        public string EmployeeId { get; private set; }

        /// <summary>
        /// 职工信息
        /// </summary>
        public string EmployeeName { get; private set; }


        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; private set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; private set; }


        /// <summary>
        /// 可以访问的模块
        /// </summary>
        public List<Module> Modules { get; private set; }

        #endregion

        public AccountViewModel(string userId)
        {
            AccountId = userId;
            var mRow = GetUserInfo(userId);
            AccountName = mRow["UserName"].ToString();
            EmployeeId = mRow["EmployeeId"].ToString();
            EmployeeName = mRow["EmployeeName"].ToString();
            RoleId = mRow["RoleId"].ToString();
            RoleName = mRow["RoleName"].ToString();
            Modules = GetModules(userId);

        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        private DataRow GetUserInfo(string userId)
        {
            IUserService mUserService = new UserService();
            var mDataTbl = mUserService.GetUserInfo(userId);
            return null != mDataTbl && mDataTbl.Rows.Count > 0 ? mDataTbl.Rows[0] : default(DataRow);
        }


        private List<Module> GetModules(string userId)
        {
            var mModules = new List<Module>();
            IModuleService mModuleService = new ModuleService();
            var mDataTbl = mModuleService.GetModules(RoleId);
            foreach (DataRow item in mDataTbl.Rows)
            {
                mModules.Add(item.BuildEntity<Module>());
            }
            return mModules;
        }

    }
}
