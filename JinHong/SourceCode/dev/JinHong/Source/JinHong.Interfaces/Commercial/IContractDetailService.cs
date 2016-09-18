using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IContractDetailService : IApplicationService
    {

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="contractDetail"></param>
        /// <returns></returns>
        bool CreateContractDetail(ContractDetail contractDetail);
        /// <summary>
        /// 删除Room
        /// </summary>
        /// <param name="contractDetailId"></param>
        /// <returns></returns>
        bool DelContractDetail(string contractDetailId);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="buildingId"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        bool HasContractDetail(string  contractId,string buildingId,string roomId);

    }
}
