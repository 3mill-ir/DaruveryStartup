using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliveryService.Models
{
    public class JsonResponseModel
    {
        public string Name { get; set; }
        public string Tell { get; set; }
        public string Address { get; set; }
        public string TrackingCode { get; set; }

    }
}