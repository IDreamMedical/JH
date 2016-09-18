using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Windows.ViewModels;

namespace JinHong.Models
{
    public class ParkingLotModel : ViewModelBase
    {

        private string id;
        private string relatedRoomId;
        private string locationDescription;
        private int status;


        private double lotWidth;

        private double lotLength;
        private double price;

        private string notes;


    }
}
