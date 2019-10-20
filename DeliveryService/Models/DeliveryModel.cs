using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DeliveryService.Models
{
    public class DeliveryModel
    {
        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_ID")]
        public int ID { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_DeliveryIssue")]
        public string DeliveryIssue { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_CreatedDateOnUTC")]
        public string CreatedDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_Img")]
        public string Img { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_Address")]
        public string Address { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_TrackingCode")]
        public string TrackingCode { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_NuncioOwnerName")]
        public string NuncioOwnerName { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_NuncioName")]
        public string NuncioName { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_NuncioTell")]
        public string NuncioTell { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_NuncioAddress")]
        public string NuncioAddress { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_DeliveryCreateDateOnUtc")]
        public Nullable<DateTime> DeliveryCreateDateOnUtc { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_SendNuncioDateOnUTC")]
        public Nullable<DateTime> SendNuncioDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_DeliverySeenDateOnUTC")]
        public Nullable<DateTime> DeliverySeenDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_NuncioAtStationDateOnUTC")]
        public Nullable<DateTime> NuncioAtStationDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Delivery_NuncioAtClientDateOnUTC")]
        public Nullable<DateTime> NuncioAtClientDateOnUTC { get; set; }
        public string Coordinates { get; set; }
        public string Status { get; set; }
    }
}
