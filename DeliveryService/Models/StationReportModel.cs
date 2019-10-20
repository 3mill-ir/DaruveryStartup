using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeliveryService.Models
{
    public class StationReportModel
    {
        public int ResponseId { get; set; }
        public int RequestId { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_Img")]
        public string Img { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_TrackingCode")]
        public string TrackingCode { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_CreatedDateOnUtc")]
        public DateTime CreateDateOnUtc { get; set; }
        public Nullable<DateTime> RejectDateOnUtc { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_Address")]
        public string Address { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_Price")]
        public double Price { get; set; }
        public string StationStatus { get; set; }
        public string ClientStatus { get; set; }
        public bool IsSetted { get; set; }
        public int F_StationId { get; set; }
    }
}