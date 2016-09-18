using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IGrantKeyService : IApplicationService
    {

        bool CreateGrantKey(GrantKey grantKey);
        bool UpdateGrantKey(GrantKey grantKey);

        bool DelGrantKey(string grantKeyId);

        bool HasGrantKey(string grantKeyId, string grantKeyName);

        DataTable GetGrantKeyByName(string companyName);

        DataTable GetAllGrantKeys();
    }
}
