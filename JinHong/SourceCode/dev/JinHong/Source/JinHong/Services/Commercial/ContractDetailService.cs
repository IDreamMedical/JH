using JinHong.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public class ContractDetailService : ApplicationService, IContractDetailService
    {
        public bool CreateContractDetail(Model.ContractDetail contractDetail)
        {
            throw new NotImplementedException();
        }

        public bool DelContractDetail(string contractDetailId)
        {
            throw new NotImplementedException();
        }

        public bool HasContractDetail(string contractId, string buildingId, string roomId)
        {
            throw new NotImplementedException();
        }
    }
}
