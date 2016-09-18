using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniGuy.Windows.ViewModels;

namespace JinHong.ViewModel
{
    public class StatusBarViewModel: ViewModelBase
    {
        private MainWindowViewModel parentVM;

        public MainWindowViewModel ParentVM
        {
            get { return parentVM; }
            protected set
            {
                if (parentVM != value)
                {
                    parentVM = value;
                    OnPropertyChanged("ParentVM");
                }
            }
        }

        public StatusBarViewModel(MainWindowViewModel parentVM)
        {
            this.ParentVM = parentVM;
        }

        //  TODO... 引用MainWindowViewModel,etc; 运行状态信息, 用户登录信息等
    }
}
