using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeliveryService.Models
{
    public class StationModel
    {
        [Display(ResourceType = typeof(Resource.Resource), Name = "Station_ID")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public int ID { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Station_FirstName")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string FirstName { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Station_LastName")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string LastName { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Station_StationName")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string StationName { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Station_Tell")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string Tell { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Station_Address")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string Address { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Station_Coordinate")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string Coordinates { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Station_Status")]
        public bool Status { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Station_CreateDateOnUTC")]
        public DateTime CreateDateOnUTC { get; set; }
        [Display(ResourceType = typeof(Resource.Resource), Name = "Station_EditDateOnUTC")]
        public DateTime EditDateOnUTC { get; set; }
        public string F_UserId { get; set; }

        [Display(ResourceType = typeof(Resource.Resource), Name = "Station_UserName")]
        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        [StringLength(100, ErrorMessage = "رمز عبور باید حداقل 8 کاراکتر باشد", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource.Resource), ErrorMessageResourceName = "View_ValidationError")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار رمز عبور یکسان نمی باشند !")]
        public string ConfirmPassword { get; set; }





        [StringLength(100, ErrorMessage = "رمز عبور باید حداقل 8 کاراکتر باشد", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string OldPassword { get; set; }

        [StringLength(100, ErrorMessage = "رمز عبور باید حداقل 8 کاراکتر باشد", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "رمز عبور و تکرار رمز عبور یکسان نمی باشند !")]
        public string ConfirmNewPassword { get; set; }

    }
}