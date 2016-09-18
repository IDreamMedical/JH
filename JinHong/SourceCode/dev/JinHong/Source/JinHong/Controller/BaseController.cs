using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JinHong.Controller
{
    public abstract class BaseController : IController
    {

        public abstract void Initialize();
        public abstract void Run();

        public abstract void Shutdown();

    }
}
