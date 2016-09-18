using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services;
using Microsoft.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Commands;
using UniGuy.Corek;

namespace JinHong.ViewModel
{
    public class NewOrEditParkingViewModel : NewOrEditViewModelBase
    {

        #region private Field

        private ParkingLotInfo _parking;



        private static readonly Lazy<IParkingLotService> lazy = new Lazy<IParkingLotService>(() => new ParkingLotService());

        #endregion

        #region Public Prop


        public ParkingLotInfo Parking
        {
            get { return _parking; }
            set
            {

                if (_parking != value)
                {
                    _parking = value;
                    OnPropertyChanged("Parking");
                }
            }
        }

        public static IParkingLotService Service { get { return lazy.Value; } }

        #endregion

        #region Public  Method


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.BtnOKCommand = new DelegateCommand(CreateOrEditParking);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);
        }
        #endregion

        #region Private Method

        private void CreateOrEditParking()
        {
            var result = false;
            if (IsExist())
            {
                MessageBox.Show("该车位已存在！", "系统提示");
                return;
            }
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    result = Service.CreateParkingLot(Parking);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateParkingLot(Parking);

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
                    return Service.HasParking(string.Empty, Parking.LocationDescription);
                case OperateModeEnum.Edit:
                    return Service.HasParking(Parking.Id, Parking.LocationDescription);
                default:
                    return false;
            }

        }


        #endregion
    }
}
