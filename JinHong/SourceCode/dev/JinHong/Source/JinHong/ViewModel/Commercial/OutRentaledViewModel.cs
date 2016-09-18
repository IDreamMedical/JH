using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 退租的。
    /// </summary>
    public class OutRentaledViewModel : INotifyPropertyChanged
    {
        #region Fields

        private string id;
        private string socalUnitName;


        private string buliding;

        private int area;

        #region ContractInfo

        private DateTime contractStartDate;
        private DateTime contractEndDate;

        private string contractNo;
        private DateTime terminateDate;
        private int isExpired;
        private double rentalFee;
        private double propertyManagementFee;

        private double parkingFee;

        private DateTime rentalTimeToDate;
        private DateTime parkingTimeToDate;

        private DateTime propertyManagementTimeToDate;





        #endregion

        #region WyStatus

        private string powerStatus;

        private string powerSureName;
        private string powerNotes;


        private string waterStatus;

        private string waterSureName;
        private string waterNotes;

        private string roomStatus;

        private string roomSureName;
        private string roomNotes;


        private string keyStatus;
        private string keySureName;
        private string keyNotes;

        private string airStatus;

        private string airSureName;
        private string airNotes;

        private string airCtrlStatus;

        private string airCtrlSureName;
        private string airCtrlNotes;

        private string parkingStatus;

        private string parkingSureName;

        private string parkingNotes;

        private string telStatus;

        private string telSureName;
        private string telNotes;



        private string invoiceStatus;

        private string invoiceSureName;

        private string invoiceNotes;



        private string otherStr;


        #endregion


        #region Signed

        private string unLeaseName;

        private string unLeaseSureName;
        private string unWYName;
        private string unWYSureName;

        private string unZSName;

        private string unZSSureName;

        private DateTime unLeaseDate;

        private DateTime wYDate;
        private DateTime zSDate;


        #endregion


        #endregion



        public OutRentaledViewModel()
        {

        }

        #region Properties

        #region Basic

        public string Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Buliding
        {
            get { return buliding; }
            set
            {
                if (buliding != value)
                {
                    buliding = value;
                    OnPropertyChanged("Buliding");
                }
            }
        }

        public int Area
        {
            get { return area; }
            set
            {
                if (area != value)
                {
                    area = value;
                    OnPropertyChanged("Area");
                }
            }
        }


        public string SocalUnitName
        {
            get { return socalUnitName; }
            set
            {
                if (socalUnitName != value)
                {
                    socalUnitName = value;
                    OnPropertyChanged("SocalUnitName");
                }
            }
        }
        #endregion


        #region ContractStatus

        public DateTime ContractStartDate
        {
            get { return contractStartDate; }
            set
            {
                if (contractStartDate != value)
                {
                    contractStartDate = value;
                    OnPropertyChanged("ContractStartDate");
                }
            }
        }

        public DateTime ContractEndDate
        {
            get { return contractEndDate; }
            set
            {
                if (contractEndDate != value)
                {
                    contractEndDate = value;
                    OnPropertyChanged("ContractEndDate");
                }
            }
        }

        public string ContractNo
        {
            get { return contractNo; }
            set
            {
                if (contractNo != value)
                {
                    contractNo = value;
                    OnPropertyChanged("ContractNo");
                }
            }
        }

        public double PropertyManagementFee
        {
            get { return propertyManagementFee; }
            set
            {
                if (propertyManagementFee != value)
                {
                    propertyManagementFee = value;
                    OnPropertyChanged("PropertyManagementFee");
                }
            }
        }
        public DateTime TerminateDate
        {
            get { return terminateDate; }
            set
            {
                if (terminateDate != value)
                {
                    terminateDate = value;
                    OnPropertyChanged("TerminateDate");
                }
            }
        }

        public int IsExpired
        {
            get { return isExpired; }
            set
            {
                if (isExpired != value)
                {
                    isExpired = value;

                    OnPropertyChanged("IsExpired");
                }
            }
        }
        public double RentalFee
        {
            get { return rentalFee; }
            set
            {
                if (rentalFee != value)
                {
                    rentalFee = value;
                    OnPropertyChanged("RentalFee");
                }
            }
        }

        public double ParkingFee
        {
            get { return parkingFee; }
            set
            {
                if (parkingFee != value)
                {
                    parkingFee = value;
                    OnPropertyChanged("ParkingFee");
                }
            }
        }

        public DateTime RentalTimeToDate
        {
            get { return rentalTimeToDate; }
            set
            {
                if (rentalTimeToDate != value)
                {
                    rentalTimeToDate = value;
                    OnPropertyChanged("RentalTimeToDate");
                }
            }
        }

        public DateTime ParkingTimeToDate
        {
            get { return parkingTimeToDate; }
            set
            {
                if (parkingTimeToDate != value)
                {
                    parkingTimeToDate = value;
                    OnPropertyChanged("ParkingTimeToDate");
                }
            }
        }

        public DateTime PropertyManagementTimeToDate
        {
            get { return propertyManagementTimeToDate; }
            set
            {
                if (propertyManagementTimeToDate != value)
                {
                    propertyManagementTimeToDate = value;
                    OnPropertyChanged("PropertyManagementTimeToDate");
                }
            }
        }

        #endregion


        #region WyStatus
        public string PowerStatus
        {
            get { return powerStatus; }
            set
            {
                if (powerStatus != value)
                {
                    powerStatus = value;
                    OnPropertyChanged("PowerStatus");
                }
            }
        }

        public string PowerSureName
        {
            get { return powerSureName; }
            set
            {
                if (powerSureName != value)
                {
                    powerSureName = value;
                    OnPropertyChanged("PowerSureName");
                }
            }
        }
        public string PowerNotes
        {
            get { return powerNotes; }
            set
            {
                if (powerNotes != value)
                {
                    powerNotes = value;
                    OnPropertyChanged("PowerNotes");
                }
            }
        }

        public string WaterStatus
        {
            get { return waterStatus; }
            set
            {
                if (waterStatus != value)
                {
                    waterStatus = value;
                    OnPropertyChanged("WaterStatus");
                }
            }
        }

        public string WaterSureName
        {
            get { return waterSureName; }
            set
            {
                if (waterSureName != value)
                {
                    waterSureName = value;
                    OnPropertyChanged("WaterSureName");
                }
            }
        }

        public string WaterNotes
        {
            get { return waterNotes; }
            set
            {
                if (waterNotes != value)
                {
                    waterNotes = value;
                    OnPropertyChanged("WaterNotes");
                }
            }
        }

        public string RoomStatus
        {
            get { return roomStatus; }
            set
            {
                if (roomStatus != value)
                {
                    roomStatus = value;
                    OnPropertyChanged("RoomStatus");
                }
            }
        }

        public string RoomSureName
        {
            get { return roomSureName; }
            set
            {
                if (roomSureName != value)
                {
                    roomSureName = value;
                    OnPropertyChanged("RoomSureName");
                }
            }
        }
        public string RoomNotes
        {
            get { return roomNotes; }
            set
            {
                if (roomNotes != value)
                {
                    roomNotes = value;
                    OnPropertyChanged("RoomNotes");
                }
            }
        }

        public string KeyStatus
        {
            get { return keyStatus; }
            set
            {
                if (keyStatus != value)
                {
                    keyStatus = value;
                    OnPropertyChanged("KeyStatus");
                }
            }
        }

        public string KeySureName
        {
            get { return keySureName; }
            set
            {
                if (keySureName != value)
                {
                    keySureName = value;
                    OnPropertyChanged("KeySureName");
                }
            }
        }

        public string KeyNotes
        {
            get { return keyNotes; }
            set
            {
                if (keyNotes != value)
                {
                    keyNotes = value;
                    OnPropertyChanged("KeyNotes");
                }
            }
        }

        public string AirStatus
        {
            get { return airStatus; }
            set
            {
                if (airStatus != value)
                {
                    airStatus = value;
                    OnPropertyChanged("AirStatus");
                }
            }
        }

        public string AirSureName
        {
            get { return airSureName; }
            set
            {
                if (airSureName != value)
                {
                    airSureName = value;
                    OnPropertyChanged("AirSureName");
                }
            }
        }

        public string AirNotes
        {
            get { return airNotes; }
            set
            {
                if (airNotes != value)
                {
                    airNotes = value;
                    OnPropertyChanged("AirNotes");
                }
            }
        }
        public string AirCtrlStatus
        {
            get { return airCtrlStatus; }
            set
            {
                if (airCtrlStatus != value)
                {
                    airCtrlStatus = value;
                    OnPropertyChanged("AirCtrlStatus");
                }
            }
        }

        public string AirCtrlSureName
        {
            get { return airCtrlSureName; }
            set
            {
                if (airCtrlSureName != value)
                {
                    airCtrlSureName = value;
                    OnPropertyChanged("AirCtrlSureName");
                }
            }
        }

        public string AirCtrlNotes
        {
            get { return airCtrlNotes; }
            set
            {
                if (airCtrlNotes != value)
                {
                    airCtrlNotes = value;
                    OnPropertyChanged("AirCtrlNotes");
                }
            }
        }
        public string ParkingStatus
        {
            get { return parkingStatus; }
            set
            {
                if (parkingStatus != value)
                {
                    parkingStatus = value;
                    OnPropertyChanged("ParkingStatus");
                }
            }
        }
        public string ParkingSureName
        {
            get { return parkingSureName; }
            set
            {
                if (parkingSureName != value)
                {
                    parkingSureName = value;
                    OnPropertyChanged("ParkingSureName");
                }
            }
        }

        public string ParkingNotes
        {
            get { return parkingNotes; }
            set
            {
                if (parkingNotes != value)
                {
                    parkingNotes = value;
                    OnPropertyChanged("ParkingNotes");
                }
            }
        }

        public string TelStatus
        {
            get { return telStatus; }
            set
            {
                if (telStatus != value)
                {
                    telStatus = value;
                    OnPropertyChanged("TelStatus");
                }
            }
        }

        public string TelSureName
        {
            get { return telSureName; }
            set
            {
                if (telSureName != value)
                {
                    telSureName = value;
                    OnPropertyChanged("TelSureName");
                }
            }
        }
        public string TelNotes
        {
            get { return telNotes; }
            set
            {
                if (telNotes != value)
                {
                    telNotes = value;
                    OnPropertyChanged("TelNotes");
                }
            }
        }
        public string InvoiceStatus
        {
            get { return invoiceStatus; }
            set
            {
                if (invoiceStatus != value)
                {
                    invoiceStatus = value;
                    OnPropertyChanged("InvoiceStatus");
                }
            }
        }
        public string InvoiceSureName
        {
            get { return invoiceSureName; }
            set
            {
                if (invoiceSureName != value)
                {
                    invoiceSureName = value;
                    OnPropertyChanged("InvoiceSureName");
                }
            }
        }
        public string InvoiceNotes
        {
            get { return invoiceNotes; }
            set
            {
                if (invoiceNotes != value)
                {
                    invoiceNotes = value;
                    OnPropertyChanged("InvoiceNotes");
                }
            }
        }
        public string OtherStr
        {
            get { return otherStr; }
            set
            {
                if (otherStr != value)
                {
                    otherStr = value;
                    OnPropertyChanged("OtherStr");
                }
            }
        }




        #endregion


        #region Signed

        public DateTime UnLeaseDate
        {
            get { return unLeaseDate; }
            set
            {
                if (unLeaseDate != value)
                {
                    unLeaseDate = value;
                    OnPropertyChanged("UnLeaseDate");
                }
            }
        }

        public DateTime WYDate
        {
            get { return wYDate; }
            set
            {
                if (wYDate != value)
                {
                    wYDate = value;
                    OnPropertyChanged("WYDate");
                }
            }
        }


        public DateTime ZSDate
        {
            get { return zSDate; }
            set
            {
                if (zSDate != value)
                {
                    zSDate = value;
                    OnPropertyChanged("ZSDate");
                }
            }
        }

        public string UnLeaseName
        {
            get { return unLeaseName; }
            set
            {
                if (unLeaseName != value)
                {
                    unLeaseName = value;
                    OnPropertyChanged("UnLeaseName");
                }
            }
        }

        public string UnLeaseSureName
        {
            get { return unLeaseSureName; }
            set
            {
                if (unLeaseSureName != value)
                {
                    unLeaseSureName = value;
                    OnPropertyChanged("UnLeaseSureName");
                }
            }
        }

        public string UnWYName
        {
            get { return unWYName; }
            set
            {
                if (unWYName != value)
                {
                    unWYName = value;
                    OnPropertyChanged("UnWYName");
                }
            }
        }

        public string UnWYSureName
        {
            get { return unWYSureName; }
            set
            {
                if (unWYName != value)
                {
                    unWYSureName = value;
                    OnPropertyChanged("UnWYName");
                }
            }
        }

        public string UnZSName
        {
            get { return unZSName; }
            set
            {
                if (unZSName != value)
                {
                    unZSName = value;
                    OnPropertyChanged("UnZSName");
                }
            }
        }

        public string UnZSSureName
        {
            get { return unZSSureName; }
            set
            {
                if (unZSSureName != value)
                {
                    unZSSureName = value;
                    OnPropertyChanged("UnZSSureName");
                }

            }
        }
        #endregion

        #endregion


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged

    }
}
