using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using UniGuy.Windows.ViewModels;
using UniGuy.Core.Message;
using UniGuy.Core.Helpers;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 某个页面的ViewModel抽象基类
    /// </summary>AbstractPageVM
    public abstract class AbstractPageViewModel : ViewModelBase
    {
        #region Fields

        private MainWindowViewModel parentVM;

        #endregion

        #region Properties

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

        #endregion

        #region Constructors

        public AbstractPageViewModel(MainWindowViewModel parentVM)
        {
            this.ParentVM = parentVM;
        }

        #endregion
    }
}
