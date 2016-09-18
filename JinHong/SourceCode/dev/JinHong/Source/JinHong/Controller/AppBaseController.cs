using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Commands;

namespace JinHong.Controller
{
    public class AppBaseController : BaseController, IAppController
    {
        public override void Initialize()
        {
            // throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }

        public override void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}
