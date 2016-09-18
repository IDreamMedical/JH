using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IContractService : IApplicationService
    {

        bool CreateContract(ContractInfo contract);
        bool UpdateContract(ContractInfo contract);

        bool DelContract(string contractId);

        /// <summary>
        /// 是否存在合同了
        /// </summary>
        /// <param name="socialUnitId"></param>
        /// <returns></returns>
        bool HasContract(string socialUnitId);

        /// <summary>
        /// 检索公司名称
        /// </summary>
        /// <param name="whereStr">可以是合同编号 或者公司名称</param>
        /// <returns></returns>
        DataTable GetContractByWhereStr(string whereStr);

        DataTable GetAllContracts();
    }
}
