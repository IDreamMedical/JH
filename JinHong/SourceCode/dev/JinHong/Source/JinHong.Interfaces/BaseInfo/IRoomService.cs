using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JinHong.Model;

namespace JinHong.ServiceContract
{
    public interface IRoomService : IApplicationService
    {
        bool CreateRoom(RoomInfo room);
        bool UpdateRoom(RoomInfo room);

        bool DelRoom(string roomId);

        bool HasRoom(string roomId, string buildingId, string roomName);

        DataTable GetRoomByName(string name);

        DataTable GetAllRooms();

        DataTable GetRoomByBuildingId(string buildingId);
    }
}
