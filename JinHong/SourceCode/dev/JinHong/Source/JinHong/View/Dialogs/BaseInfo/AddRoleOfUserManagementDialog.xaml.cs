using JinHong.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JinHong.View
{
    /// <summary>
    /// AddRoleOfUserManagementDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddRoleOfUserManagementDialog : Window
    {
        public AddRoleOfUserManagementDialog()
        {
            InitializeComponent();
            this.DataContext = new AddRoleOfUserManagementDialogVM(null)
            {
                ViewDialog=this
            };
        }
    }
}
