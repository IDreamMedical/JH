using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UniGuy.Controls.Commands
{
    public interface ICommand
    {
        // Methods
        void Execute();
        object GetProperty(string propertyName);
        void SetProperty(string propertyName, object propertyValue);
    }
}
 

