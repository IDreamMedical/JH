using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IBuildingService : IApplicationService
    {

        bool CreateBuilding(BuildingInfo building);
        bool UpdateBuilding(BuildingInfo building);

        bool DelBuilding(string buildingId);

        bool HasBuilding(string buildingId, string buildingName);

        DataTable GetBuildingByName(string name);

        DataTable GetAllBuildings();
    }
}
