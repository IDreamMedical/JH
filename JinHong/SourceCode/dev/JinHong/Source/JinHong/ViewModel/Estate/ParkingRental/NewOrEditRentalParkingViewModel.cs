using JinHong.Model;
using JinHong.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UniGuy.Commands;
using JinHong.Extensions;
using System.Data;

namespace JinHong.ViewModel
{
    public class NewOrEditRentalParkingViewModel : NewOrEditViewModelBase
    {
        #region Fields
        private ParkingLotRentalInfo _parkingRental;

        private ObservableCollection<ParkingLotInfo> _parkingLots;
        private ObservableCollection<SocialUnitInfo> _socialUnits;


        private ObservableCollection<Area> _areas;
        #endregion

        #region Properties
        public ObservableCollection<ParkingLotInfo> ParkingLots
        {
            get { return _parkingLots; }
            set
            {

                if (_parkingLots != value)
                {
                    _parkingLots = value;
                    OnPropertyChanged("ParkingLots");

                }
            }
        }

        public ObservableCollection<SocialUnitInfo> SocialUnits
        {
            get { return _socialUnits; }
            set
            {
                if (_socialUnits != value)
                {
                    _socialUnits = value;
                    OnPropertyChanged("SocialUnits");

                }
            }
        }


        public ObservableCollection<Area> Areas
        {
            get { return _areas; }
            set
            {

                if (_areas != value)
                {
                    _areas = value;
                    OnPropertyChanged("Areas");

                }
            }
        }

        public ParkingLotRentalInfo ParkingRental
        {
            get { return _parkingRental; }
            set
            {
                if (_parkingRental != value)
                {
                    _parkingRental = value;
                    OnPropertyChanged("ParkingRental");
                }
            }
        }

        #endregion

        #region Method

        #region Public Method
        public void Initialize()
        {
            InitializeAreas();
            InitializeSocialUnits();
            InitializeParkingLots();
            this.BtnOKCommand = new DelegateCommand(CreateParkingRental);
            this.BtnCancelCommand = new DelegateCommand(Cancel);
        }
        #endregion

        #region Private Method

        private void CreateParkingRental()
        {

        }

        private void InitializeAreas()
        {
            Areas = new ObservableCollection<Area>();
            var dt = new AreaService().GetAllAreas();
            if (null != dt)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Areas.Add(item.BuildEntity<Area>());
                }
            }

        }

        private void InitializeSocialUnits()
        {
            SocialUnits = new ObservableCollection<SocialUnitInfo>();
            var dt = new SocialUnitService().GetAllSocialUnits();
            if (null != dt)
            {
                foreach (DataRow item in dt.Rows)
                {
                    SocialUnits.Add(item.BuildEntity<SocialUnitInfo>());
                }
            }


        }

        private void InitializeParkingLots()
        {
            ParkingLots = new ObservableCollection<ParkingLotInfo>();
            var dt = new ParkingLotService().GetAllParkingLots();
            if (null != dt)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ParkingLots.Add(item.BuildEntity<ParkingLotInfo>());
                }
            }

        }

        #endregion

        #endregion

    }
}
