using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IUserService : IApplicationService
    {

        void CreateUser();
        void UpdateUser();

        void DelUser();

        DataTable GetUserByName(string name);

        DataTable GetAllUsers();
        bool HasUser(string id, string name);


        DataTable GetUserInfo(string userId);

    }
}
