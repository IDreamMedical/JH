using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using UniGuy.Commands;
using UniGuy.Corek;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 停车费用信息
    /// </summary>
    public class NewOrEditParkingFeeViewModel : NewOrEditViewModelBase
    {

        #region private var
        private ParkingFeesInfo _parkingFee;



        private static readonly Lazy<IParkingFeeService> lazy = new Lazy<IParkingFeeService>(() => new ParkingFeeService());




        #endregion


        #region Public Prop


        public ParkingFeesInfo ParkingFee
        {
            get { return _parkingFee; }
            set
            {
                _parkingFee = value;
                if (_parkingFee != value)
                {
                    _parkingFee = value;
                    OnPropertyChanged("ParkingFee");
                }
            }
        }

        public static IParkingFeeService Service { get { return lazy.Value; } }

        #endregion

        #region Public  Method


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.BtnOKCommand = new DelegateCommand(CreateOrEditRole);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);
        }
        #endregion

        #region Private Method

        private void CreateOrEditRole()
        {
            var result = false;
           
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    result = Service.CreateParkingFee(ParkingFee);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateParkingFee(ParkingFee);

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



        #endregion


    }
}
