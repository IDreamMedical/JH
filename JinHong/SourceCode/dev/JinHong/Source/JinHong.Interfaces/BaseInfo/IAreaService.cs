using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IAreaService : IApplicationService
    {
        void CreateArea();
        void UpdateArea();

        void DelArea();

        DataTable GetAreaByName(string name);

        DataTable GetAllAreas();
    }
}
