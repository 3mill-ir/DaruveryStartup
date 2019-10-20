using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using DeliveryService.Areas.Admin3mill.Models;
using System.Data.Entity;

namespace DeliveryService.Models
{
    public class ReportManagement
    {
        public List<StationReportModel> StationActivityReport(int StationID, int pageNumber, int pageSize, out int total)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                List<StationReportModel> Result = new List<StationReportModel>();
                var Objects = db.Responses.Include(e => e.Request).Where(u => u.F_StationId == StationID).OrderByDescending(y => y.CreatedDateOnUTC).ToPagedList(pageNumber, pageSize);
                total = Objects.TotalItemCount;
                foreach (var item in Objects)
                {
                    StationReportModel InsertObject = new StationReportModel();
                    InsertObject.CreateDateOnUtc = item.CreatedDateOnUTC ?? default(DateTime);
                    InsertObject.Img = Tools.ReturnPath("RequestImagePath", "ListRequests") + item.Request.Image;
                    InsertObject.ResponseId = item.Id;
                    InsertObject.IsSetted = item.Request.isSetteld ?? default(bool);
                    InsertObject.RequestId = item.F_RequestId ?? default(int);
                    InsertObject.TrackingCode = item.Request.TrackingCode;
                    InsertObject.Address = item.Request.Address;
                    InsertObject.F_StationId = item.F_StationId ?? default(int);
                    InsertObject.ClientStatus = item.Request.Status;
                    if (item.StationResponse == "OK")
                        InsertObject.StationStatus = "1";
                    else if (item.StationResponse == "NOK")
                        InsertObject.StationStatus = "2";
                    else
                        InsertObject.StationStatus = "3";
                    InsertObject.Price = item.Request.Price ?? default(double);
                    Result.Add(InsertObject);
                }
                return Result;
            }
        }


        public StationDetailModel StationActivityDetail(int StationID, int RequestId)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var request = db.Requests.FirstOrDefault(u => u.Id == RequestId);
                var response = db.Responses.FirstOrDefault(u => u.F_StationId == StationID && u.F_RequestId == RequestId);
                var delivery = db.Deliveries.Include(e => e.Nuncio).FirstOrDefault(u => u.Id == RequestId && u.F_StationId == StationID);
                StationDetailModel Result = new StationDetailModel();
                Result.HaveNuncio = false;
                List<string> Statuses = new List<string>() { "تایید شده", "رد شده", "در حال بررسی" };
                if (request != null)
                {
                    Result.Img = Tools.ReturnPath("RequestImagePath", "ListRequests") + request.Image;
                    Result.RequestId = request.Id;
                    Result.Address = request.Address;
                    Result.Price = request.Price ?? default(double);
                    Result.IsSetted = request.isSetteld ?? default(bool);
                    Result.RequestCreateDateOnUtc = request.CreatedDateOnUTC ?? default(DateTime);
                    Result.Coordinates = request.Coordinates;
                    Result.TrackingCode = request.TrackingCode;
                    if (request.Status != null)
                        Result.Status = Statuses[Convert.ToInt32(request.Status) - 1];
                }
                if (response != null)
                {
                    Result.CreateDateOnUtc = response.CreatedDateOnUTC ?? default(DateTime);
                    Result.SeenDateOnUTC = response.SeenDateOnUTC ?? default(DateTime);
                    Result.ProcessingDateOnUTC = response.ProcessingDateOnUTC ?? default(DateTime);
                    Result.WaitingForClientDateOnUTC = response.WaitingForClientDateOnUTC ?? default(DateTime);
                    Result.ClientResponseDateOnUTC = response.ClientResponseDateOnUTC ?? default(DateTime);
                    Result.F_StationId = response.F_StationId ?? default(int);
                    Result.ResponseId = response.Id;
                    Result.ResponseIssue = response.ResponseIssue;
                }
                if (delivery != null)
                {
                    Result.NuncioAtClientDateOnUTC = delivery.NuncioAtClientDateOnUTC ?? default(DateTime);
                    Result.NuncioAtStationDateOnUTC = delivery.NuncioAtStationDateOnUTC ?? default(DateTime);
                    Result.SendNuncioDateOnUTC = delivery.SendNuncioDateOnUTC ?? default(DateTime);
                    Result.DeliveryCreateDateOnUtc = delivery.CreatedDateOnUTC ?? default(DateTime);
                    Result.DeliverySeenDateOnUTC = delivery.SeenDateOnUTC ?? default(DateTime);
                    Result.DeliveryIssue = delivery.DeliveryIssue;
                    Result.NuncioTell = delivery.Nuncio.Tell;
                    Result.NuncioAddress = delivery.Nuncio.Address;
                    Result.NuncioName = delivery.Nuncio.NuncioName;
                    Result.NuncioOwnerCompleteName = delivery.Nuncio.FirstName + " " + delivery.Nuncio.LastName;
                    Result.HaveNuncio = true;
                }
                return Result;
            }
        }


        public ClientRequestDetailModel ClientRequestDetail(int RequestId)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var request = db.Requests.Include(e => e.Responses).FirstOrDefault(u => u.Id == RequestId);
                var AcceptedResponse = request.Responses.FirstOrDefault(u => u.StationResponse == "OK");
                int F_StationId = -1;
                if (AcceptedResponse != null)
                    F_StationId = AcceptedResponse.F_StationId ?? default(int);
                var delivery = db.Deliveries.Include(e => e.Nuncio).FirstOrDefault(u => u.Id == RequestId && u.F_StationId == 1);
                ClientRequestDetailModel Result = new ClientRequestDetailModel();
                Result.HaveNuncio = false;
                List<string> Statuses = new List<string>() { "تایید شده", "رد شده", "در حال بررسی" };
                if (request != null)
                {
                    Result.Img = Tools.ReturnPath("RequestImagePath", "ListRequests") + request.Image;
                    Result.RequestId = request.Id;
                    Result.Address = request.Address;
                    Result.Price = request.Price ?? default(double);
                    Result.IsSetted = request.isSetteld ?? default(bool);
                    Result.RequestCreateDateOnUtc = request.CreatedDateOnUTC ?? default(DateTime);
                    Result.Coordinates = request.Coordinates;
                    Result.TrackingCode = request.TrackingCode;
                    if (request.Status != null)
                        Result.Status = Statuses[Convert.ToInt32(request.Status) - 1];
                    foreach (var response in request.Responses)
                    {
                        ClientRequestResponsesModel temp = new ClientRequestResponsesModel();
                        temp.CreateDateOnUtc = Tools.GetDateTimeReturnJalaliDate(response.CreatedDateOnUTC ?? default(DateTime));
                        temp.SeenDateOnUTC = Tools.GetDateTimeReturnJalaliDate(response.SeenDateOnUTC ?? default(DateTime));
                        temp.ProcessingDateOnUTC = Tools.GetDateTimeReturnJalaliDate(response.ProcessingDateOnUTC ?? default(DateTime));
                        temp.WaitingForClientDateOnUTC = Tools.GetDateTimeReturnJalaliDate(response.WaitingForClientDateOnUTC ?? default(DateTime));
                        temp.ClientResponseDateOnUTC = Tools.GetDateTimeReturnJalaliDate(response.ClientResponseDateOnUTC ?? default(DateTime));
                        temp.RejectDateOnUtc = Tools.GetDateTimeReturnJalaliDate(response.StationRejectDateOnUTC ?? default(DateTime));
                        temp.F_StationId = response.F_StationId ?? default(int);
                        temp.ResponseId = response.Id;
                        temp.ResponseIssue = response.ResponseIssue;
                        temp.StationName = response.Station.StaionName;
                        temp.StationResponse = response.StationResponse;
                        Result.Responses.Add(temp);
                    }
                }
                if (delivery != null)
                {
                    Result.NuncioAtClientDateOnUTC = delivery.NuncioAtClientDateOnUTC ?? default(DateTime);
                    Result.NuncioAtStationDateOnUTC = delivery.NuncioAtStationDateOnUTC ?? default(DateTime);
                    Result.SendNuncioDateOnUTC = delivery.SendNuncioDateOnUTC ?? default(DateTime);
                    Result.DeliveryCreateDateOnUtc = delivery.CreatedDateOnUTC ?? default(DateTime);
                    Result.DeliverySeenDateOnUTC = delivery.SeenDateOnUTC ?? default(DateTime);
                    Result.DeliveryIssue = delivery.DeliveryIssue;
                    Result.NuncioTell = delivery.Nuncio.Tell;
                    Result.NuncioAddress = delivery.Nuncio.Address;
                    Result.NuncioName = delivery.Nuncio.NuncioName;
                    Result.NuncioOwnerCompleteName = delivery.Nuncio.FirstName + " " + delivery.Nuncio.LastName;
                    Result.HaveNuncio = true;
                }
                return Result;
            }
        }



        public List<RequestManagementModel> AllStationActivityReport(int pageNumber, int pageSize, out int total)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                List<RequestManagementModel> Result = new List<RequestManagementModel>();
                var Objects = db.Responses.Include(e => e.Request).OrderByDescending(y => y.CreatedDateOnUTC).ToPagedList(pageNumber, pageSize);
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
                    InsertObject.Status = item.Request.Status;
                    InsertObject.Price = item.Request.Price ?? default(double);
                    item.SeenDateOnUTC = DateTime.Now;
                    db.SaveChanges();
                    Result.Add(InsertObject);
                }
                return Result;
            }
        }
    }
}