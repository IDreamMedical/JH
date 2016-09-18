using JinHong.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JinHong.Model;

namespace JinHong.Services
{
    public class AirConditionerService : ApplicationService, IAirConditioneService
    {

        private string resultSql = string.Empty;
        private const string SelectById = "SELECT * FROM AirConditioner";
        public bool CreateAirConditioner(AirConditioner airConditioner)
        {

            return ServiceInstance.Insert(airConditioner);
        }

        public bool UpdateAirConditioner(AirConditioner airConditioner)
        {
            return ServiceInstance.Update(airConditioner);
        }

        public bool DelAirConditioner(string airConditionerId)
        {
            return ServiceInstance.Delete<AirConditioner>(airConditionerId);
        }

        public bool HasAirConditioner(string airConditionerId, string airConditionerName)
        {
            throw new NotImplementedException();
        }
        public DataTable GetAirConditionerByName(string name)
        {
            string selectById = SelectById + " where Name like '%{0}%'";
            resultSql = string.Format(selectById, name);
            var ds = ServiceInstance.Select(resultSql, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }

        public DataTable GetAllAirConditioners()
        {
            var ds = ServiceInstance.Select(SelectById, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
        }


    }
}
