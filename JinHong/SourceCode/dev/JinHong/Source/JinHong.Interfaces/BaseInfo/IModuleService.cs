using JinHong.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace JinHong.ServiceContract
{
    public interface IModuleService : IApplicationService
    {

        bool CreateModule(Module module);


        bool UpdateModule(Module module);

        bool DelModule(string moduleId);


        bool HasModule(string moduleId, string moduleName);



        DataTable GetModuleByName(string moduleName);

        DataTable GetAllModules();


        DataTable GetModules(string userId);
    }
}
