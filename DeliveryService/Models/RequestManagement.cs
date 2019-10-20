using DeliveryService.Areas.Admin3mill.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PagedList;
using System.Configuration;
using System.Web.Configuration;

namespace DeliveryService.Models
{
    public class RequestManagement
    {
        public string AndroidListRequests()
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                List<JsonRequestListModel> Result = new List<JsonRequestListModel>();
                string F_UserId = Tools.F_UserID();
                var client = db.Clients.FirstOrDefault(u => u.F_userId == F_UserId && u.isDeleted == false && u.Status == true);
                if (client != null)
                {
                    var Objects = db.Requests.Include(u => u.Responses).Where(u => u.isDeleted == false && u.F_ClientId == client.Id).Select(y => new { y.Id, y.Status, y.Price, y.CreatedDateOnUTC, y.Image, y.TrackingCode, y.Responses });
                    foreach (var item in Objects)
                    {
                        JsonRequestListModel InsertObject = new JsonRequestListModel();
                        InsertObject.Date = Tools.GetDateTimeReturnJalaliDate(item.CreatedDateOnUTC ?? default(DateTime));
                        InsertObject.Image = ConfigurationManager.AppSettings["WebSiteUrl"] + Tools.ReturnPath("RequestImagePath", "AndroidListRequests") + item.Image;
                        InsertObject.Cost = item.Price.ToString();
                        InsertObject.Status = item.Status;
                        InsertObject.TrackingCode = item.TrackingCode;
                        InsertObject.Name = db.Responses.FirstOrDefault(u => u.F_RequestId == item.Id && u.StationResponse != "Rejected").Station.StaionName;
                        Result.Add(InsertObject);
                    }
                }
                dynamic collectionWrapper = new
                {
                    Root = Result
                };
                var output = Newtonsoft.Json.JsonConvert.SerializeObject(collectionWrapper);
                return output;
            }
        }


        public string AndroidSendRequestToServer(string Address, string Lat, string Long, HttpPostedFileBase Img)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                StationManagement st = new StationManagement();
                Request req = new Request();
                req.Image = Tools.ImageSave_Gallery(Img, Tools.ReturnPathPhysicalMode("RequestImagePath", "AndroidListRequests"));
                req.CreatedDateOnUTC = DateTime.Now;
                req.Coordinates = Lat + ',' + Long;
                req.F_ClientId = Tools.F_ClientID();
                req.isDeleted = false;
                req.isSetteld = false;
                req.Address = Address;
                req.Status = "3";
                req.TrackingCode = Tools.TrackingCodeGenerator();
                db.Requests.Add(req);
                db.SaveChanges();
                Response resp = new Response();
                resp.CreatedDateOnUTC = DateTime.Now;
                resp.F_RequestId = req.Id;
                resp.F_StationId = st.GetNearlyStationId(Lat, Long);
                if (resp.F_StationId == -1)
                {
                    resp.StationRejectDateOnUTC = DateTime.Now;
                }
                db.Responses.Add(resp);
                db.SaveChanges();
                return req.TrackingCode;
            }
        }


        public List<RequestManagementModel> ListRequests(int StationID, int pageNumber, int pageSize, out int total)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                List<RequestManagementModel> Result = new List<RequestManagementModel>();
                var Objects = db.Responses.Include(e => e.Request).Where(u => u.F_StationId == StationID).OrderByDescending(y => y.CreatedDateOnUTC).ToPagedList(pageNumber, pageSize);
                total = Objects.TotalItemCount;
                foreach (var item in Objects)
                {
                    RequestManagementModel InsertObject = new RequestManagementModel();
                    InsertObject.CreateDateOnUtc = item.CreatedDateOnUTC ?? default(DateTime);
                    InsertObject.Img = Tools.ReturnPath("RequestImagePath", "ListRequests") + item.Request.Image;
                    InsertObject.ResponseId = item.Id;
                    InsertObject.RequestId = item.F_RequestId ?? default(int);
                    InsertObject.TrackingCode = item.Request.TrackingCode;
                    InsertObject.Address = item.Request.Address;
                    InsertObject.F_StationId = item.F_StationId ?? default(int);
                    if (item.StationResponse == "OK")
                        InsertObject.Status = "1";
                    else if (item.StationResponse == "NOK")
                        InsertObject.Status = "2";
                    else
                        InsertObject.Status = "3";
                    item.SeenDateOnUTC = DateTime.Now;
                    if (item.StationRejectDateOnUTC != null)
                        InsertObject.RejectDateOnUtc = item.StationRejectDateOnUTC ?? default(DateTime);
                    else
                        InsertObject.RejectDateOnUtc = null;
                    db.SaveChanges();
                    Result.Add(InsertObject);
                }
                return Result;
            }
        }


        public string AcceptRejectRequest(string ResponseIssue, int RequestId, int StationId, string StationResponse, double? Price = 0)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var Request = db.Requests.Include(u => u.Responses).FirstOrDefault(u => u.Id == RequestId);
                var Response = Request.Responses.FirstOrDefault(u => u.F_StationId == StationId);
                Response.StationResponse = StationResponse;
                Response.ResponseIssue = ResponseIssue;
                Response.ProcessingDateOnUTC = DateTime.Now;
                if (StationResponse == "OK")
                {
                    Request.Price = Price;
                    Response.WaitingForClientDateOnUTC = DateTime.Now.AddMinutes(6);
                    Tools.PushNotification(Tools.AndroidId(Request.F_ClientId ?? default(int)), WebConfigurationManager.AppSettings["WebSiteUrl"] + Tools.ReturnPath("RequestImagePath", "ListRequests") + Request.Image, Request.Price.ToString(), Request.Id.ToString());
                }
                else
                    Response.StationRejectDateOnUTC = DateTime.Now.AddMinutes(2);
                db.SaveChanges();
                return "OK";
            }
        }

        public string ClientAcceptRejectRequest(int RequestId, string Type)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var Request = db.Requests.Include(u => u.Responses).FirstOrDefault(u => u.Id == RequestId);
                var Response = Request.Responses.FirstOrDefault(u => u.StationResponse == "OK" && u.F_RequestId == RequestId);
                if (Response != null)
                {
                    Response.ClientResponseDateOnUTC = DateTime.Now;
                    if (Type == "Accept")
                    {
                        Request.Status = "1";
                        Response.ClientResponse = "OK";
                        DeliveryManagement dm = new DeliveryManagement();
                        dm.SendToNuncio(Response.F_StationId ?? default(int), RequestId);
                        db.SaveChanges();
                        return Tools.GenerateJsonResponse("OK", "سفارش شما با موفقیت تایید شد.");
                    }
                    else
                    {
                        Request.Status = "2";
                        Response.ClientResponse = "NOK";
                        db.SaveChanges();
                        return Tools.GenerateJsonResponse("OK", "سفارش شما با موفقیت رد شد.");
                    }

                }
                return Tools.GenerateJsonResponse("NOK", "خطا در ثبت سفارش");
            }
        }

        public void ChooseRecoveryStation(int RequestId)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                StationManagement st = new StationManagement();
                string Coordinate = db.Requests.FirstOrDefault(u => u.Id == RequestId && u.isDeleted == false).Coordinates;
                string Lat = Coordinate.Substring(0, Coordinate.IndexOf(','));
                string Long = Coordinate.Substring(Coordinate.IndexOf(',') + 1);
                var Stations = db.Responses.Where(u => u.F_RequestId == RequestId && u.StationRejectDateOnUTC != null).Select(y => y.F_StationId);
                var req = db.Requests.FirstOrDefault(u => u.Id == RequestId);
                req.Status = "3";
                Response resp = new Response();
                resp.CreatedDateOnUTC = DateTime.Now;
                resp.F_RequestId = RequestId;
                resp.F_StationId = st.GetNearlyStationIdRecovery(Lat, Long, Stations.ToList());
                if (resp.F_StationId == -1)
                {
                    resp.StationRejectDateOnUTC = DateTime.Now;
                }
                db.Responses.Add(resp);
                db.SaveChanges();
            }
        }


        public string PayPrice(int RequestId)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var req = db.Requests.FirstOrDefault(u => u.Id == RequestId && u.isDeleted == false && u.Status == "1" && u.isSetteld == false);
                if (req != null)
                {
                    req.isSetteld = true;
                    db.SaveChanges();
                    return "OK";
                }
                return "NotFound";
            }
        }



        public List<RequestManagementModel> ClientRequests(string UserName, int pageNumber, int pageSize, out int total)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                List<RequestManagementModel> Result = new List<RequestManagementModel>();
                var user = manager.FindByName(UserName);
                if (user != null)
                {
                    int ClientId = Tools.F_ClientID(user.Id);
                    var Objects = db.Requests.Where(u => u.F_ClientId == ClientId).OrderByDescending(y => y.CreatedDateOnUTC).ToPagedList(pageNumber, pageSize);
                    total = Objects.TotalItemCount;
                    foreach (var item in Objects)
                    {
                        RequestManagementModel InsertObject = new RequestManagementModel();
                        InsertObject.CreateDateOnUtc = item.CreatedDateOnUTC ?? default(DateTime);
                        InsertObject.Img = Tools.ReturnPath("RequestImagePath", "ListRequests") + item.Image;
                        InsertObject.RequestId = item.Id;
                        InsertObject.TrackingCode = item.TrackingCode;
                        InsertObject.Address = item.Address;
                        InsertObject.Status = item.Status;
                        Result.Add(InsertObject);
                    }
                }
                else
                    total = 0;
                return Result;
            }
        }


    }
}