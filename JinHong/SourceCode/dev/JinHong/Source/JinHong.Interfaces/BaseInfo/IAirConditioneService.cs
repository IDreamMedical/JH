using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IAirConditioneService : IApplicationService
    {
        bool CreateAirConditioner(AirConditioner airConditioner);
        bool UpdateAirConditioner(AirConditioner airConditioner);

        bool DelAirConditioner(string airConditionerId);

        bool HasAirConditioner(string airConditionerId, string airConditionerName);


        DataTable GetAirConditionerByName(string name);

        DataTable GetAllAirConditioners();
    }
}
