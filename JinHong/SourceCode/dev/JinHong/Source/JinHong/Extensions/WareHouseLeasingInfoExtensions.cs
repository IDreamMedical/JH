using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinHong.Model;
using System.Data;
using UniGuy.Core.Data;

namespace JinHong.Extensions
{
    public static class WareHouseLeasingInfoExtensions
    {
        /// <summary>
        /// 入租
        /// </summary>
        /// <param name="this"></param>
        public static void Add(this WareHouseLeasingInfo @this)
        {
            GlobalVariables.Smc.Insert<WareHouseLeasingInfo>(@this);
            string sql = string.Format("SELECT * FROM WareHouseLeasingStatusInfo WHERE WareHouseId='{0}'", @this.WareHouseId);
            DataSet ds = GlobalVariables.Smc.Select(sql);
            WareHouseLeasingStatusInfo lsi = null;
            if(ds!=null && ds.Tables!=null && ds.Tables[0].Rows.Count>0)
            {
                lsi = EntityBuilder.BuildEntity<WareHouseLeasingStatusInfo>(ds.Tables[0], 0);
                if(lsi.SocialUnitId!=null)
                    throw new Exception("The warehouse is already leased out.");
                lsi.LeasingInfoId = @this.Id;
                //lsi.PhoneNo = @this.PhoneNo;
                lsi.SocialUnitId = @this.SocialUnitId;
                lsi.SocialUnitName = @this.SocialUnitName;
            }
            else
            {
                lsi = new WareHouseLeasingStatusInfo(Guid.NewGuid().ToString()){ WareHouseId = @this.WareHouseId, LeasingInfoId = @this.Id,
                     SocialUnitId = @this.SocialUnitId, SocialUnitName = @this.SocialUnitName};
            }
            GlobalVariables.Smc.Save2<WareHouseLeasingStatusInfo>(lsi);
        }

        /// <summary>
        /// 退租
        /// </summary>
        /// <param name="this"></param>
        public static void Cancel(this WareHouseLeasingInfo @this)
        {
            GlobalVariables.Smc.Update<WareHouseLeasingInfo>(@this);
            string sql = string.Format("SELECT * FROM WareHouseLeasingStatusInfo WHERE WareHouseId='{0}'", @this.WareHouseId);
            DataSet ds = GlobalVariables.Smc.Select(sql);
            if (ds == null || ds.Tables == null || ds.Tables[0].Rows.Count <= 0)
                throw new Exception("Not leasing to cancel.");
            WareHouseLeasingStatusInfo lsi = EntityBuilder.BuildEntity<WareHouseLeasingStatusInfo>(ds.Tables[0], 0);
            lsi.LeasingInfoId = null;
            lsi.PhoneNo = null;
            lsi.SocialUnitId = null;
            lsi.SocialUnitName = null;
        }
    }
}
