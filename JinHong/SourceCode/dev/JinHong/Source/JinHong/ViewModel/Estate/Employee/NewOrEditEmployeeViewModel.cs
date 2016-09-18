using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using UniGuy.Commands;
using UniGuy.Corek;

namespace JinHong.ViewModel
{
    public class NewOrEditEmployeeViewModel : NewOrEditViewModelBase
    {
        #region Fields

        private EmployeeInfo _employee;

        private static readonly Lazy<IEmployeeService> lazy = new Lazy<IEmployeeService>(() => new EmployeeService());

        #endregion

        #region Properties

        public static IEmployeeService Service { get { return lazy.Value; } }

        public EmployeeInfo Employee
        {
            get { return _employee; }
            set
            {

                if (_employee != value)
                {
                    _employee = value;
                    OnPropertyChanged("Employee");
                }
            }
        }
       
        
        

        #endregion

        #region Methods



        public void Initialize()
        {
            this.BtnOKCommand = new DelegateCommand(CreateOrEditCheckIn);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);

        }

        #region Private Method

        private void CreateOrEditCheckIn()
        {
            var result = false;
            if (IsExist())
            {
                MessageBox.Show("该员工已存在！", "系统提示");
                return;
            }
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    result = Service.CreateEmployee(Employee);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateEmployee(Employee);

                    break;
                default:
                    break;
            }
            if (result)
            {
                MessageBox.Show("保存成功！", "系统提示");
                if (null != RefreshParentForm)
                {
                    RefreshParentForm.Invoke();
                }
                base.Cancel();
            }
            else
            {
                MessageBox.Show("保存失败！", "系统提示");
            }

        }


        /// <summary>
        /// 是否存在
        /// </summary>
        /// <returns></returns>
        private bool IsExist()
        {
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    return Service.HasEmployee(string.Empty, Employee.Id);
                case OperateModeEnum.Edit:
                    return Service.HasEmployee(Employee.Id, Employee.Name);
                default:
                    return false;
            }

        }


       
        #endregion

        #endregion

    }
}
