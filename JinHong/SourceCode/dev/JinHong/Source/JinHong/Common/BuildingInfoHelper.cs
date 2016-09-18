using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JinHong
{
    public class BuildingInfoHelper
    {
        /// <summary>
        /// 获取可用楼号
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBuildings()
        {
            var ds = GlobalVariables.Smc.Select("SELECT *from BuildingInfo where Status=0");
            return ds == null ? null : ds.Tables[0];
        }

    }
}
