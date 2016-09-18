using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Windows.ViewModels;

namespace JinHong.Models
{
    public class UserModel : NotificationObject
    {

        private string _id;


        private string _name;


        private string _passWord;



        private string _confirmPassWord;

        public string Id
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    RaisePropertyChanged(() => Id);
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        public string PassWord
        {
            get { return _passWord; }
            set
            {

                if (value != _passWord)
                {
                    _passWord = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }
        public string ConfirmPassWord
        {
            get { return _confirmPassWord; }
            set
            {
                if (value != _confirmPassWord)
                {
                    _confirmPassWord = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }


    }
}
