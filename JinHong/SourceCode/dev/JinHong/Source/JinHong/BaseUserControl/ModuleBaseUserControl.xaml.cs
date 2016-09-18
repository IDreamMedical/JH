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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JinHong.BaseUserControl
{
    /// <summary>
    /// BaseUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ModuleBaseUserControl : UserControl
    {

        #region Field
        private string _moduleName = "锦宏简明管理系统";

        public string ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }

        #endregion

        public ModuleBaseUserControl()
        {
            InitializeComponent();
        }
    }
}
