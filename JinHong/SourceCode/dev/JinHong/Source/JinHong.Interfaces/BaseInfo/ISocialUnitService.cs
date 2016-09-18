using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface ISocialUnitService : IApplicationService
    {
        bool CreateSocialUnit(SocialUnitInfo socialUnit);
        bool UpdateSocialUnit(SocialUnitInfo socialUnit);

        bool DelSocialUnit(string socialUnitId);

        bool HasSocialUnit(string socialUnitId, string socialUnitName);

        bool HasContract(string socialUnitId);

        DataTable GetSocialUnitById(string socialUnitName);


        DataTable GetAllSocialUnits();



        /// <summary>
        /// 获取企业首页信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        DataSet GetSocialUnitDetail(string companyId);



    }
}
