using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliveryService.Models
{
    public class JsonRequestListModel
    {
        public string Name { get; set; }
        public string TrackingCode { get; set; }
        public string Cost { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public string Date { get; set; }
    }
}