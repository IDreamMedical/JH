using JinHong.Model;
using JinHong.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using JinHong.Extensions;
namespace JinHong.Helper
{
    /// <summary>
    /// 公司帮助类
    /// </summary>
    public class SocialUnitsHelper
    {

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<SocialUnitInfo> LoadSocialUnits()
        {
            var socialUnits = new ObservableCollection<SocialUnitInfo>();

            var dt = new SocialUnitService().GetAllSocialUnits();
            if (null != dt)
            {
                foreach (DataRow item in dt.Rows)
                {
                    socialUnits.Add(item.BuildEntity<SocialUnitInfo>());
                }
            }
            return socialUnits;
        }
    }
}
