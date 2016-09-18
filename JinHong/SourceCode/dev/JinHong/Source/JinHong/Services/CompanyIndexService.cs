using JinHong.ServiceContract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public class CompanyIndexService : ApplicationService, ICompanyIndexService
    {
        private string resultSql = string.Empty;
        private const string SelectById = "SELECT * FROM BuildingInfo";


        public bool CreateCompany()
        {
            throw new NotImplementedException();
        }

        public bool UpdateCompany()
        {
            throw new NotImplementedException();
        }

        public bool DelCompany()
        {
            throw new NotImplementedException();
        }

        public DataTable GetCompanyByName(string name)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAllCompanys()
        {
            var ds = ServiceInstance.Select(SelectById, null);
            return ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();

        }

        public void CreateCompanyIndex()
        {
            throw new NotImplementedException();
        }

        public void UpdateCompanyIndex()
        {
            throw new NotImplementedException();
        }

        public void DelCompanyIndex()
        {
            throw new NotImplementedException();
        }

        public DataSet GetCompanyIndexDetailName(string companyId)
        {
            throw new NotImplementedException();
        }

        DataSet ICompanyIndexService.GetAllCompanys()
        {
            throw new NotImplementedException();
        }
    }
}
