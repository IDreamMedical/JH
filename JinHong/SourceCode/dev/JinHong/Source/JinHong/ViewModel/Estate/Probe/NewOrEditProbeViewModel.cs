using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Commands;

namespace JinHong.ViewModel
{
    public class NewOrEditProbeViewModel : NewOrEditViewModelBase
    {

        #region private Var
        private MonitorProbe _monitorProbe;
        #endregion

        #region Public  Var

        /// <summary>
        /// 探头
        /// </summary>
        public MonitorProbe MonitorProbe
        {
            get { return _monitorProbe; }
            set
            {
                if (_monitorProbe != value)
                {
                    _monitorProbe = value;
                    OnPropertyChanged("MonitorProbe");
                }
            }
        }
        #endregion


        #region Method

        #region Public Method
        public void Initialize()
        {
            this.BtnOKCommand = new DelegateCommand(CreateMonitorProbe);
            this.BtnCancelCommand = new DelegateCommand(Cancel);
        }
        #endregion

        #region Private Method

        private void CreateMonitorProbe()
        {

        }




        #endregion

        #endregion

    }
}
