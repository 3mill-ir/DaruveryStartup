using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeliveryService.Models
{
    public class ClientModel
    {
        [Display(ResourceType = typeof(Resource.Resource), Name = "Client_ID")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public int ID { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Client_FirstName")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string FirstName { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Client_LastName")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string LastName { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Client_Address")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string Address { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Client_Tell")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string Tell { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Client_Status")]
        public bool Status { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Client_CreateDateOnUTC")]
        public DateTime CreateDateOnUTC { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Client_EditDateOnUTC")]
        public DateTime EditDateOnUTC { get; set; }
        public string F_UserId { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Client_UserName")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

    }
}