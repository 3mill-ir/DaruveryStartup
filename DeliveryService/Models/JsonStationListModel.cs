using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliveryService.Models
{
    public class JsonStationListModel
    {
        public string Name { get; set; }
        public string  Lat { get; set; }
        public string Long { get; set; }
        public string Tell { get; set; }
        public string Address { get; set; }
    }
}