using JinHong.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JinHong.Services
{
    public abstract class ApplicationService : IApplicationService
    {

        private static readonly Lazy<ServiceMainClient> lazy =
new Lazy<ServiceMainClient>(() => new ServiceMainClient());

        public static ServiceMainClient ServiceInstance { get { return lazy.Value; } }

    }
}
