using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{

    /// <summary>
    /// 企业首页信息，相当于病案首页
    /// </summary>
    public interface ICompanyIndexService : IApplicationService
    {
        void CreateCompanyIndex();
        void UpdateCompanyIndex();

        void DelCompanyIndex();

        DataSet GetCompanyIndexDetailName(string companyId);

        DataSet GetAllCompanys();
    }
}
