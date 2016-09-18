using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Windows.ViewModels;

namespace JinHong.Models
{
    public class BulidingModel:NotificationObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
