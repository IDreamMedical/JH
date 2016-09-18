using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UniGuy.Windows.ViewModels;

namespace JinHong.Models
{
    /// <summary>
    /// 界面显示的Model, 需要转换成DTO 在网络中传输  
    /// 界面Model<->传输对象（Contract Object Dto）<-> 持久化对象（PO）
    /// </summary>
    public class RoleModel : NotificationObject
    {
        private string _id;
        private string _name;
        private string _description;
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged(() => this.Id);
                }
            }


        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged(() => this.Name);
                }
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    RaisePropertyChanged(() => this.Description);
                }
            }
        }

    }
}
