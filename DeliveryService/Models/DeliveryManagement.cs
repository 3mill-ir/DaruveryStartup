using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DeliveryService.Areas.Admin3mill.Models;
using PagedList;

namespace DeliveryService.Models
{
    public class DeliveryManagement
    {
        public List<DeliveryModel> ListDeliverys(int pageNumber, int pageSize, out int total)
        {
            List<DeliveryModel> Result = new List<DeliveryModel>();
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var Deliverys = db.Deliveries.Include(u => u.Request).OrderByDescending(u => u.CreatedDateOnUTC).ToPagedList(pageNumber, pageSize);
                total = Deliverys.TotalItemCount;
                foreach (var item in Deliverys)
                {
                    var m = new DeliveryModel();
                    m.ID = item.Id;
                    m.DeliveryIssue = item.DeliveryIssue;
                    m.CreatedDateOnUTC = Tools.GetDateTimeReturnJalaliDate(item.CreatedDateOnUTC ?? default(DateTime));
                    m.Status = item.NuncioResponse;
                    m.Img = Tools.ReturnPath("RequestImagePath", "ListDeliverys") + item.Request.Image;
                    m.TrackingCode = item.Request.TrackingCode;
                    m.Address = item.Request.Address;
                    item.SeenDateOnUTC = DateTime.Now;
                    Result.Add(m);
                }
                db.SaveChanges();
            }
            return Result;
        }

        public DeliveryModel DeliverDetail(int DeliveryId)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var Delivery = db.Deliveries.Include(u => u.Request).Include(y => y.Nuncio).OrderByDescending(u => u.CreatedDateOnUTC).FirstOrDefault(u => u.Id == DeliveryId);
                var m = new DeliveryModel();
                m.ID = Delivery.Id;
                m.DeliveryIssue = Delivery.DeliveryIssue;
                m.Status = Delivery.NuncioResponse;
                m.Img = Tools.ReturnPath("RequestImagePath", "ListDeliverys") + Delivery.Request.Image;
                m.TrackingCode = Delivery.Request.TrackingCode;
                m.Address = Delivery.Request.Address;
                m.NuncioName = Delivery.Nuncio.NuncioName;
                m.NuncioOwnerName = Delivery.Nuncio.FirstName + " " + Delivery.Nuncio.LastName;
                m.NuncioTell = Delivery.Nuncio.Tell;
                m.NuncioAddress = Delivery.Nuncio.Address;
                m.Coordinates = Delivery.Request.Coordinates;
                m.DeliverySeenDateOnUTC = Delivery.SeenDateOnUTC;
                m.NuncioAtClientDateOnUTC = Delivery.NuncioAtClientDateOnUTC;
                m.NuncioAtStationDateOnUTC = Delivery.NuncioAtStationDateOnUTC;
                m.SendNuncioDateOnUTC = Delivery.SendNuncioDateOnUTC;
                m.DeliveryCreateDateOnUtc = Delivery.CreatedDateOnUTC;
                return m;
            }
        }

        public void DeliverOrder(int DeliveryId, string DeliveryIssue)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var del = db.Deliveries.FirstOrDefault(u => u.Id == DeliveryId);
                del.DeliveryIssue = DeliveryIssue;
                del.SendNuncioDateOnUTC = DateTime.Now;
                del.NuncioAtStationDateOnUTC = DateTime.Now.AddHours(1);
                del.NuncioAtClientDateOnUTC = DateTime.Now.AddHours(2).AddMinutes(40);
                del.NuncioResponse = "OK";
                db.SaveChanges();
            }
        }

        public void SendToNuncio(int StationId, int RequestId)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                Delivery del = new Delivery();
                del.Id = RequestId;
                del.CreatedDateOnUTC = DateTime.Now;
                del.F_StationId = StationId;
                del.F_NuncioId = Convert.ToInt32(ConfigurationManager.AppSettings["NuncioId"]);
                del.StationResponse = "OK";
                db.Deliveries.Add(del);
                db.SaveChanges();
            }
        }

    }
}