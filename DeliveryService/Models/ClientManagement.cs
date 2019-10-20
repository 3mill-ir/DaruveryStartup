using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeliveryService.Models;
using DeliveryService.Areas.Admin3mill.Models;
using PagedList;
using System.Web.Configuration;


namespace DeliveryService.Models
{
    public class ClientManagement
    {
        public List<ClientModel> ListClient(int pageNumber, int pageSize, out int total)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var Objects = db.Clients.Where(u => u.isDeleted == false).OrderByDescending(y => y.CreatedDateOnUTC).Select(y => new { y.Id, y.Address, y.Status, y.FirstName, y.LastName, y.F_userId }).ToPagedList(pageNumber, pageSize);
                total = Objects.TotalItemCount;
                List<ClientModel> Result = new List<ClientModel>();
                foreach (var item in Objects)
                {
                    ClientModel InsertObject = new ClientModel();
                    InsertObject.ID = item.Id;
                    InsertObject.Address = item.Address;
                    InsertObject.FirstName = item.FirstName;
                    InsertObject.LastName = item.LastName;
                    InsertObject.Status = item.Status ?? default(bool);
                    InsertObject.UserName = Tools.F_UserName(item.F_userId);
                    Result.Add(InsertObject);
                }
                return Result;
            }
        }
        public string RegisterClient(string Tell, string Address, string FirstName, string LastName, string Password, string AndroidId)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                JsonResultModel response = new JsonResultModel();
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var FoundedUser = manager.FindByName(Tell);
                if (FoundedUser == null)
                {
                    UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var user = new ApplicationUser() { UserName = Tell };
                    var result = UserManager.Create(user, Password);
                    if (result.Succeeded)
                    {
                        UserManager.AddToRole(user.Id, "Client");
                        Client InsertObject = new Client();
                        InsertObject.Address = Address;
                        InsertObject.CreatedDateOnUTC = DateTime.Now;
                        InsertObject.F_userId = user.Id;
                        InsertObject.FirstName = FirstName;
                        InsertObject.isDeleted = false;
                        InsertObject.LastName = LastName;
                        InsertObject.Status = false;
                        InsertObject.AndroidId = AndroidId;
                        InsertObject.RndValue = Tools.TrackingCodeGenerator(6);
                        db.Clients.Add(InsertObject);
                        db.SaveChanges();
                        SMSIranianWebService.Send sms = new SMSIranianWebService.Send();
                        long[] rec = null;
                        byte[] status = null;
                        string[] ReplyNum = { user.UserName };
                        string ResultSMS = "کد اختصاصی شما جهت احراز هویت عبارت است از : \n" + InsertObject.RndValue;
                        sms.SendSms("9145195658", "9145195658", ReplyNum, WebConfigurationManager.AppSettings["SMSPannel"], ResultSMS, false, "", ref rec, ref status);
                        return Tools.GenerateJsonResponse("OK", "حساب کاربری شما به صورت موقت ثبت شد، یک کد تایید هویت برای شما ارسال خواهد گردید");
                    }
                }
                return Tools.GenerateJsonResponse("Iterative", "نام کاربری تکراری می باشد");
            }
        }


        public string AuthorizeClient(string Tell, string Password, string RndValue)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var FoundedUser = manager.Find(Tell, Password);
                if (FoundedUser != null)
                {
                    var client = db.Clients.FirstOrDefault(u => u.F_userId == FoundedUser.Id);
                    if(client.RndValue==RndValue)
                    {
                        client.Status = true;
                        db.SaveChanges();
                        return Tools.GenerateJsonResponse("OK", "حساب کاربری شما با موفقیت ثبت دائمی گردید");
                    }
                    else
                        return Tools.GenerateJsonResponse("Error", "کد 6 رقمی وارد شده صحیح نمی باشد !");
                }
                return Tools.GenerateJsonResponse("NOK", "خطا در تایید هویت !");
            }
        }

        public string EditClient(string Address, string FirstName, string LastName)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                string F_UserId = Tools.F_UserID();
                var EditObject = db.Clients.FirstOrDefault(u => u.F_userId == F_UserId && u.isDeleted == false && u.Status == true);
                if (EditObject != null)
                {
                    EditObject.Address = Address;
                    EditObject.EditedDateOnUTC = DateTime.Now;
                    EditObject.FirstName = FirstName;
                    EditObject.LastName = LastName;
                    db.SaveChanges();
                    return Tools.GenerateJsonResponse("OK", "حساب کاربری با موفقیت ویرایش شد");
                }
                return Tools.GenerateJsonResponse("NOK", "خطا در ویرایش حساب کاربری");
            }
        }


        public string GetSpecifics()
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                UserSpecificsModel us = new UserSpecificsModel();
                string F_UserId = Tools.F_UserID();
                var Object = db.Clients.FirstOrDefault(u => u.F_userId == F_UserId && u.isDeleted == false && u.Status == true);
                if (Object != null)
                {
                    us.Address = Object.Address;
                    us.FirstName = Object.FirstName;
                    us.LastName = Object.LastName;
                }
                else
                {
                    us.Address = "یافت نشد";
                    us.FirstName = "یافت نشد";
                    us.LastName = "یافت نشد";
                }
                dynamic collectionWrapper = new { Root = us };
                var output = Newtonsoft.Json.JsonConvert.SerializeObject(collectionWrapper);
                return output;
            }
        }


        public string GetRandomValue(string Tell, string Password)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var FoundedUser = manager.Find(Tell, Password);
                if (FoundedUser != null)
                {
                    var client = db.Clients.FirstOrDefault(u => u.F_userId == FoundedUser.Id && u.isDeleted == false && u.Status == true);
                    if (client != null)
                    {
                        SMSIranianWebService.Send sms = new SMSIranianWebService.Send();
                        long[] rec = null;
                        byte[] status = null;
                        string[] ReplyNum = { Tools.F_UserName() };
                        string ResultSMS = "کد اختصاصی شما جهت احراز هویت عبارت است از : \n" + client.RndValue;
                        sms.SendSms("9145195658", "9145195658", ReplyNum, WebConfigurationManager.AppSettings["SMSPannel"], ResultSMS, false, "", ref rec, ref status);
                        return Tools.GenerateJsonResponse("OK", "کد 6 رقمی با موفقیت ارسال گردید");
                    }
                    else
                        return Tools.GenerateJsonResponse("NOK", "خطا در دریافت کد 6 رقمی");
                }
                return Tools.GenerateJsonResponse("Error", "مشخصات وارد شده در سیستم ثبت نگردیده است !");
            }
        }

        public string ChangePasswordClient(string OldPassword, string NewPassword)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var FoundedUser = manager.Find(Tools.F_UserName(), OldPassword);
                var EditObject = db.Clients.FirstOrDefault(u => u.F_userId == FoundedUser.Id && u.isDeleted == false && u.Status == true);
                if (FoundedUser != null && EditObject != null)
                {
                    UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    IdentityResult result = UserManager.ChangePassword(FoundedUser.Id, OldPassword, NewPassword);
                    if (!result.Succeeded)
                        return Tools.GenerateJsonResponse("ChangePasswordError", "خطا در ویرایش رمز عبور");
                    else
                        return Tools.GenerateJsonResponse("Success", "رمز عبور با موفقیت ویرایش شد");
                }
                else
                    return Tools.GenerateJsonResponse("NoSuchUser", "حساب کاربری با این مشخصات یافت نشد");
            }
        }


        public string ChangeStatusClient(int ID)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var ChangeStatusObject = db.Clients.FirstOrDefault(u => u.Id == ID && u.isDeleted == false);
                if (ChangeStatusObject != null)
                {
                    ChangeStatusObject.Status = !ChangeStatusObject.Status;
                    db.SaveChanges();
                    return "OK";
                }
                return "NotFound";
            }
        }

        public string DeleteClient(int ID)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var DeleteObject = db.Clients.FirstOrDefault(u => u.Id == ID && u.isDeleted == false);
                if (DeleteObject != null)
                {
                    DeleteObject.isDeleted = true;
                    db.SaveChanges();
                    return "OK";
                }
                return "NotFound";
            }
        }


        public ClientModel DetailClient(int ID)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var DetailObject = db.Clients.FirstOrDefault(u => u.Id == ID && u.isDeleted == false);
                ClientModel Result = new ClientModel();
                if (DetailObject != null)
                {
                    Result.Address = DetailObject.Address;
                    Result.ID = DetailObject.Id;
                    Result.CreateDateOnUTC = DetailObject.CreatedDateOnUTC ?? default(DateTime);
                    if (DetailObject.EditedDateOnUTC != null)
                        Result.EditDateOnUTC = DetailObject.EditedDateOnUTC ?? default(DateTime);
                    Result.FirstName = DetailObject.FirstName;
                    Result.F_UserId = DetailObject.F_userId;
                    Result.LastName = DetailObject.LastName;
                    Result.Status = DetailObject.Status ?? default(bool);
                    Result.UserName = Tools.F_UserName(DetailObject.F_userId);
                }
                return Result;
            }
        }
    }
}