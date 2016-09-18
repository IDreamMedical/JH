using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Windows.ViewModels;
using JinHong.Model;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 入租预登记
    /// </summary>
    public class PreRentalVM : NewOrEditViewModelBase
    {

        #region private var
        private SocialUnitInfo _socialUnit;


        #endregion



        #region public  prop

        public SocialUnitInfo SocialUnit
        {
            get { return _socialUnit; }
            set
            {
                if (_socialUnit != value)
                {
                    _socialUnit = value;
                    OnPropertyChanged("SocialUnit");
                }
            }
        }
        #endregion

    }
}
