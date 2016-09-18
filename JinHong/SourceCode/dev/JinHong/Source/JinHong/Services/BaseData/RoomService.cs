using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinHong.ServiceContract;
using System.Data;
using JinHong.Model;
namespace JinHong.Services
{
    public class RoomService : ApplicationService, IRoomService
    {

        private string resultSql = string.Empty;
        private const string baseSqlStr = "select   a.*,b.Name as BuildingName from  RoomInfo  a  join BuildingInfo  b  on  a.BuildingId=b.Id ";

        public DataTable GetRoomByName(string roomName)
        {
            string resultSql = string.Empty;

            resultSql = string.Format(baseSqlStr + " where a.Name like '%{0}%'", roomName);

            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

        public DataTable GetAllRooms()
        {
            var ds = ServiceInstance.Select(baseSqlStr, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }
        public bool CreateRoom(RoomInfo room)
        {
            return ServiceInstance.Insert(room);
        }

        public bool UpdateRoom(RoomInfo room)
        {
            return ServiceInstance.Update<RoomInfo>(room);
        }

        public bool DelRoom(string roomId)
        {
            return ServiceInstance.Delete<RoomInfo>(roomId);
        }


        public bool HasRoom(string id, string buildingId, string name)
        {
            resultSql = string.Empty;
            if (string.IsNullOrEmpty(id))
            {
                resultSql = string.Format(baseSqlStr + " where  a.name='{0}' and a.BuildingId='{1}'", name, buildingId);
            }
            else
            {
                resultSql = string.Format(baseSqlStr + " where  a. id!='{0}' and  a. name='{1}' and a. BuildingId='{1}'", id, name, buildingId);
            }

            var ds = ServiceInstance.Select(resultSql);
            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;

        }




        public DataTable GetRoomByBuildingId(string buildingId)
        {
            resultSql = string.Format(baseSqlStr + " where a.BuildingId='{0}'", buildingId);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }

    }
}
