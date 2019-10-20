using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeliveryService.Models
{
    public class StationDetailModel
    {
        public bool HaveNuncio { get; set; }
        public int F_StationId { get; set; }
        public int ResponseId { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_CreatedDateOnUtc")]
        public Nullable<DateTime> CreateDateOnUtc { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_RejectDateOnUtc")]
        public Nullable<DateTime> RejectDateOnUtc { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_SeenDateOnUTC")]
        public Nullable<DateTime> SeenDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_ProcessingDateOnUTC")]
        public Nullable<DateTime> ProcessingDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_WaitingForClientDateOnUTC")]
        public Nullable<DateTime> WaitingForClientDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_ClientResponseDateOnUTC")]
        public Nullable<DateTime> ClientResponseDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_ResponseIssue")]
        public string ResponseIssue { get; set; }

        /// <summary>
        /// Request Part
        /// </summary>

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_TrackingCode")]
        public string TrackingCode { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_Img")]
        public string Img { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_Address")]
        public string Address { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_Price")]
        public double Price { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_Status")]
        public string Status { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_Coordinates")]
        public string Coordinates { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_RequestCreatedDateOnUtc")]
        public Nullable<DateTime> RequestCreateDateOnUtc { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_IsSetted")]
        public bool IsSetted { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_RequestId")]
        public int RequestId { get; set; }

        /// <summary>
        /// Delivery Part
        /// </summary>

        [Display(ResourceType = typeof(Resource.Resource), Name = "DeliveryManagment_DeliveryCreateDateOnUtc")]
        public Nullable<DateTime> DeliveryCreateDateOnUtc { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "DeliveryManagment_SendNuncioDateOnUTC")]
        public Nullable<DateTime> SendNuncioDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "DeliveryManagment_DeliverySeenDateOnUTC")]
        public Nullable<DateTime> DeliverySeenDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "DeliveryManagment_NuncioAtStationDateOnUTC")]
        public Nullable<DateTime> NuncioAtStationDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "DeliveryManagment_NuncioAtClientDateOnUTC")]
        public Nullable<DateTime> NuncioAtClientDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "DeliveryManagment_DeliveryIssue")]
        public string DeliveryIssue { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "DeliveryManagment_NuncioOwnerCompleteName")]
        public string NuncioOwnerCompleteName { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "DeliveryManagment_NuncioName")]
        public string NuncioName { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "DeliveryManagment_NuncioTell")]
        public string NuncioTell { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "DeliveryManagment_NuncioAddress")]
        public string NuncioAddress { get; set; }

    }
}