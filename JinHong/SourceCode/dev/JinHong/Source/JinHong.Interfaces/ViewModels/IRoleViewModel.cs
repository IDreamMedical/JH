using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using UniGuy.Windows.ViewModels;

namespace JinHong.Interfaces.ViewModels
{
    /// <summary>
    /// 角色ViewModle 接口
    /// </summary>
    public interface IRoleViewModel : IViewModel
    {
        ICommand AddNewCommand { get; set; }
        ICommand RemoveCommand { get; set; }
        ICommand ExportCommand { get; set; }
        ICommand EditCommand { get; set; }


    }
}
