using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Windows.View
{
    public interface IView
    {

        object DataContext { get; set; }
        void Close();

    }
}
