using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JinHong.Controller
{
    public interface IController
    {

        /// <summary>
        /// Initializes the module controller.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Run the module controller.
        /// </summary>
        void Run();

        /// <summary>
        /// Shutdown the module controller.
        /// </summary>
        void Shutdown();

    }
}
