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
    public static class BuildingsHelper
    {
        public static ObservableCollection<BuildingInfo> InitializeBuildings()
        {
            var buildings = new ObservableCollection<BuildingInfo>();
            var dt = new BuildingService().GetAllBuildings();
            if (null != dt)
            {
                foreach (DataRow item in dt.Rows)
                {
                    buildings.Add(item.BuildEntity<BuildingInfo>());
                }
            }

            return buildings;

        }



    }
}
