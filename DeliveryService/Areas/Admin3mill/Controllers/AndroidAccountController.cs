using DeliveryService.Areas.Admin3mill.Models;
using DeliveryService.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Security;

namespace DeliveryService.Areas.Admin3mill.Controllers
{
    [Authorize]
    public class AndroidAccountController : ApiController
    {
        UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        [HttpPost]
        [AllowAnonymous]
        public async Task<string> Login(string Tell, string Password,string AndroidId)
        {
            JsonResultModel response = new JsonResultModel();
            if (ModelState.IsValid)
            {
                var user = UserManager.Find(Tell, Password);
                using (DeliveryService.Models.DBEntities db = new DBEntities())
                {
                    if (user != null)
                    {
                        var client = db.Clients.FirstOrDefault(u => u.F_userId == user.Id);
                        if (client.Status == true)
                        {
                            await SignInAsync(user, false);
                            FormsAuthentication.SetAuthCookie(user.UserName, false);
                            if(client.AndroidId!=AndroidId)
                            {
                                client.AndroidId = AndroidId;
                                db.SaveChanges();
                            }
                            return Tools.GenerateJsonResponse("OK", "شما با موفقیت وارد سیستم شدید");
                        }
                        return Tools.GenerateJsonResponse("NOK", "کد تایید هویت تاکنون ثبت نگردیده");
                    }
                    return Tools.GenerateJsonResponse("WrongUserPass", "نام کاربری یا رمز عبور اشتباه است");
                }
            }
            return Tools.GenerateJsonResponse("Error", "خطا در پردازش عملیات");
        }

        [HttpPost]
        [AllowAnonymous]
        public string AndroidRegisterClient(string Tell, string Address, string FirstName, string LastName, string Password, string AndroidId)
        {
            ClientManagement sm = new ClientManagement();
            return sm.RegisterClient(Tell, Address, FirstName, LastName, Password, AndroidId);
        }

        [HttpPost]
        [AllowAnonymous]
        public string AndroidAuthorizeClient(string Tell, string Password, string RndValue)
        {
            ClientManagement sm = new ClientManagement();
            return sm.AuthorizeClient(Tell, Password, RndValue);
        }

        [HttpPost]
        public string AndroidEditClient(string Address, string FirstName, string LastName)
        {
            ClientManagement sm = new ClientManagement();
            return sm.EditClient(Address, FirstName, LastName);
        }

        [HttpPost]
        public string AndroidChangePassword(string OldPassword, string NewPassword)
        {
            ClientManagement sm = new ClientManagement();
            return sm.ChangePasswordClient(OldPassword, NewPassword);
        }

        [HttpGet]
        public string AndroidLogOut()
        {
            AuthenticationManager.SignOut();
            return Tools.GenerateJsonResponse("OK", "شما با موفقیت از حساب کاربری خود خارج شدید ");
        }

        [HttpPost]
        [AllowAnonymous]
        public string GetRandomValue(string Tell, string Password)
        {
            ClientManagement sm = new ClientManagement();
            return sm.GetRandomValue(Tell,Password);
        }

        [AllowAnonymous]
        [HttpPost]
        public string ForgottenPassword(string Tell)
        {
            Tools.SendSmsToWithText(Tell, "رمز عبور جدید شما جهت ورود به سیستم : \n" + Tools.ForgottenPassword(Tell));
            return Tools.GenerateJsonResponse("OK", "رمز عبور جدید شما به شماره همراه ثبت شده شما در سیستم ارسال خواهد شد");
        }

        [HttpPost]
        public string GetSpecifics()
        {
            ClientManagement sm = new ClientManagement();
            return sm.GetSpecifics();
        }

        #region Helper
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        #endregion
    }
}
