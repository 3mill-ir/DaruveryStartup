using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DeliveryService.Models
{
    public class ClientRequestDetailModel
    {
        public ClientRequestDetailModel()
        {
            Responses = new List<ClientRequestResponsesModel>();
        }
        public List<ClientRequestResponsesModel> Responses { get; set; }
        public bool HaveNuncio { get; set; }

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

    public class ClientRequestResponsesModel
    {
        public int F_StationId { get; set; }
        public int ResponseId { get; set; }
        public string StationResponse { get; set; }
        public string StationOwnerName { get; set; }
        public string StationName { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_CreatedDateOnUtc")]
        public string CreateDateOnUtc { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_RejectDateOnUtc")]
        public string RejectDateOnUtc { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_SeenDateOnUTC")]
        public string SeenDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_ProcessingDateOnUTC")]
        public string ProcessingDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_WaitingForClientDateOnUTC")]
        public string WaitingForClientDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_ClientResponseDateOnUTC")]
        public string ClientResponseDateOnUTC { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "RequestManagment_ResponseIssue")]
        public string ResponseIssue { get; set; }
    }
}
