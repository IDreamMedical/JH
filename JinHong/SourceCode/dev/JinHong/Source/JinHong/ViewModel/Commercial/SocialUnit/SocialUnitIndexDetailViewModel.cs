using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 索引明细ViewModel
    /// </summary>
    public class SocialUnitContractViewModel : BaseViewModel
    {

        #region private var

        /// <summary>
        /// 基本信息
        /// </summary>
        private SocialUnitInfo _selectedSocialUnit;
        /// <summary>
        /// 合同
        /// </summary>
        private ContractInfo _selectedContract;


        /// <summary>
        /// 合同信息
        /// </summary>
        private DataTable _contractTbl;


        /// <summary>
        /// 租金缴纳明细
        /// </summary>
        private DataTable _feeTbl;

        #endregion
        public SocialUnitContractViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }
    }
}
