//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeliveryService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Response
    {
        public int Id { get; set; }
        public string StationResponse { get; set; }
        public string ClientResponse { get; set; }
        public Nullable<int> F_StationId { get; set; }
        public Nullable<int> F_RequestId { get; set; }
        public Nullable<System.DateTime> CreatedDateOnUTC { get; set; }
        public Nullable<System.DateTime> SeenDateOnUTC { get; set; }
        public Nullable<System.DateTime> ProcessingDateOnUTC { get; set; }
        public Nullable<System.DateTime> WaitingForClientDateOnUTC { get; set; }
        public Nullable<System.DateTime> ClientResponseDateOnUTC { get; set; }
        public Nullable<System.DateTime> StationRejectDateOnUTC { get; set; }
        public string ResponseIssue { get; set; }
    
        public virtual Request Request { get; set; }
        public virtual Station Station { get; set; }
    }
}
