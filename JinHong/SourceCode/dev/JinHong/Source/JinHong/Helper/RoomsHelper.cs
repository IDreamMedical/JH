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
    /// 获取单元信息
    /// </summary>
    public static class RoomsHelper
    {

        public static ObservableCollection<RoomInfo> GetRooms(string buildingId)
        {
            var rooms = new ObservableCollection<RoomInfo>();
            var dt = new RoomService().GetRoomByBuildingId(buildingId);
            if (null != dt)
            {
                foreach (DataRow item in dt.Rows)
                {
                    rooms.Add(item.BuildEntity<RoomInfo>());
                }
            }

            return rooms;

        }
    }
}
