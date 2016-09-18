using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinHong.Model;
using System.Data;
using UniGuy.Core.Data;

namespace JinHong.Extensions
{
    public static class LeasingInfoExtensions
    {
        /// <summary>
        /// 入租
        /// </summary>
        /// <param name="this"></param>
        public static void Add(this CheckIn @this)
        {

            GlobalVariables.Smc.Insert<CheckIn>(@this);
            //string sql = string.Format("SELECT * FROM LeasingInfo WHERE RoomId='{0}'", @this.RoomId);
            //DataSet ds = GlobalVariables.Smc.Select(sql);
            //LeasingStatusInfo lsi = null;
            //if(ds!=null && ds.Tables!=null && ds.Tables[0].Rows.Count>0)
            //{
            //    lsi = EntityBuilder.BuildEntity<LeasingStatusInfo>(ds.Tables[0], 0);
            //    if(lsi.SocialUnitId!=null)
            //        throw new Exception("The room is already leased out.");
            //    lsi.LeasingInfoId = @this.Id;
            //    lsi.PhoneNo = @this.PhoneNo;
            //    lsi.SocialUnitId = @this.SocialUnitId;
            //    lsi.SocialUnitName = @this.SocialUnitName;
            //}
            //else
            //{
            //    lsi = new LeasingStatusInfo(Guid.NewGuid().ToString()){ BuildingId = @this.BuildingId, LeasingInfoId = @this.Id,
            //         PhoneNo = @this.PhoneNo, RoomId = @this.RoomId, SocialUnitId = @this.SocialUnitId, SocialUnitName = @this.SocialUnitName};
            //}
            //GlobalVariables.Smc.Save2<LeasingStatusInfo>(lsi);
        }

        /// <summary>
        /// 退租
        /// </summary>
        /// <param name="this"></param>
        public static void Cancel(this CheckIn @this)
        {
            GlobalVariables.Smc.Update<CheckIn>(@this);
            string sql = string.Format("SELECT * FROM LeasingStatusInfo WHERE RoomId='{0}'", @this.RoomId);
            DataSet ds = GlobalVariables.Smc.Select(sql);
            if (ds == null || ds.Tables == null || ds.Tables[0].Rows.Count <= 0)
                throw new Exception("Not leasing to cancel.");
            LeasingStatusInfo lsi = EntityBuilder.BuildEntity<LeasingStatusInfo>(ds.Tables[0], 0);
            lsi.LeasingInfoId = null;
            lsi.PhoneNo = null;
            lsi.RoomId = null;
            lsi.SocialUnitId = null;
            lsi.SocialUnitName = null;
        }
    }
}
