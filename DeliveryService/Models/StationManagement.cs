using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Data.Entity;
using DeliveryService.Areas.Admin3mill.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DeliveryService.Models
{
    public class StationManagement
    {

        public List<StationModel> ListStation()
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var Objects = db.Stations.Where(u => u.isDeleted == false).OrderByDescending(y => y.CreatedDateOnUTC).Select(y => new { y.Id, y.Address, y.Status, y.FirstName, y.LastName, y.StaionName, y.F_UserId });
                List<StationModel> Result = new List<StationModel>();
                foreach (var item in Objects)
                {
                    StationModel InsertObject = new StationModel();
                    InsertObject.ID = item.Id;
                    InsertObject.Address = item.Address;
                    InsertObject.FirstName = item.FirstName;
                    InsertObject.LastName = item.LastName;
                    InsertObject.StationName = item.StaionName;
                    InsertObject.Status = item.Status ?? default(bool);
                    InsertObject.UserName = Tools.F_UserName(item.F_UserId);
                    Result.Add(InsertObject);
                }
                return Result;
            }
        }
        public string AddStation(StationModel model)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Station");
                    Station InsertObject = new Station();
                    InsertObject.Address = model.Address;
                    InsertObject.Coordinates = model.Coordinates;
                    InsertObject.CreatedDateOnUTC = DateTime.Now;
                    InsertObject.F_UserId = user.Id;
                    InsertObject.FirstName = model.FirstName;
                    InsertObject.isDeleted = false;
                    InsertObject.LastName = model.LastName;
                    InsertObject.StaionName = model.StationName;
                    InsertObject.Status = true;
                    InsertObject.Tell = model.Tell;
                    db.Stations.Add(InsertObject);
                    db.SaveChanges();
                    return "OK";
                }
                return "Iterative";
            }
        }

        public string EditStation(StationModel model)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var EditObject = db.Stations.FirstOrDefault(u => u.Id == model.ID && u.isDeleted == false);
                if (EditObject != null)
                {
                    if (!string.IsNullOrEmpty(model.OldPassword) && !string.IsNullOrEmpty(model.NewPassword))
                    {
                        UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                        IdentityResult result = UserManager.ChangePassword(model.F_UserId, model.OldPassword, model.NewPassword);
                        if (!result.Succeeded)
                            return "ChangePasswordError";
                    }
                    EditObject.Address = model.Address;
                    EditObject.Coordinates = model.Coordinates;
                    EditObject.EditedDateOnUTC = DateTime.Now;
                    EditObject.FirstName = model.FirstName;
                    EditObject.LastName = model.LastName;
                    EditObject.StaionName = model.StationName;
                    EditObject.Tell = model.Tell;
                    db.SaveChanges();
                    return "OK";
                }
                else
                    return "NOK";
            }
        }


        public string ChangeStatusStation(int ID)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var ChangeStatusObject = db.Stations.FirstOrDefault(u => u.Id == ID && u.isDeleted == false);
                if (ChangeStatusObject != null)
                {
                    ChangeStatusObject.Status = !ChangeStatusObject.Status;
                    db.SaveChanges();
                    return "OK";
                }
                return "NotFound";
            }
        }

        public string DeleteStation(int ID)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var DeleteObject = db.Stations.FirstOrDefault(u => u.Id == ID && u.isDeleted == false);
                if (DeleteObject != null)
                {
                    DeleteObject.isDeleted = true;
                    db.SaveChanges();
                    return "OK";
                }
                return "NotFound";
            }
        }


        public StationModel DetailStation(int ID, string type = "Default")
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var DetailObject = db.Stations.FirstOrDefault(u => u.Id == ID && u.isDeleted == false);
                StationModel Result = new StationModel();
                if (DetailObject != null)
                {
                    Result.Address = DetailObject.Address;
                    Result.ID = DetailObject.Id;
                    Result.CreateDateOnUTC = DetailObject.CreatedDateOnUTC ?? default(DateTime);
                    if (DetailObject.EditedDateOnUTC != null)
                        Result.EditDateOnUTC = DetailObject.EditedDateOnUTC ?? default(DateTime);
                    Result.FirstName = DetailObject.FirstName;
                    Result.F_UserId = DetailObject.F_UserId;
                    Result.Coordinates = DetailObject.Coordinates;
                    Result.LastName = DetailObject.LastName;
                    Result.StationName = DetailObject.StaionName;
                    Result.Status = DetailObject.Status ?? default(bool);
                    if (type == "Default")
                        Result.Tell = DetailObject.Tell;
                    else
                        Result.Tell = DetailObject.Tell.Substring(1);
                    Result.UserName = Tools.F_UserName(DetailObject.F_UserId);
                }
                return Result;
            }
        }


        public int GetNearlyStationId(string Lat, string Long)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                List<KeyValuePair<int, double>> differencs = new List<KeyValuePair<int, double>>();
                double x = Convert.ToDouble(Lat);
                double y = Convert.ToDouble(Long);
                var Coordinates = db.Stations.Where(u => u.isDeleted == false && u.Status == true);
                foreach (var item in Coordinates)
                {
                    double dx = x - Convert.ToDouble(item.Coordinates.Substring(0, item.Coordinates.IndexOf(',')));
                    double dy = y - Convert.ToDouble(item.Coordinates.Substring(item.Coordinates.IndexOf(',') + 1));
                    differencs.Add(new KeyValuePair<int, double>(item.Id, Math.Sqrt(dx * dx + dy * dy)));
                }
                if (differencs.Count > 0)
                {
                    var ttt = differencs.Min(u => u.Value);
                    return differencs.FirstOrDefault(u => u.Value == ttt).Key;
                }
                else
                    return -1;
            }
        }



        public int GetNearlyStationIdRecovery(string Lat, string Long, List<int?> StationIds)
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                List<KeyValuePair<int, double>> differencs = new List<KeyValuePair<int, double>>();
                double x = Convert.ToDouble(Lat);
                double y = Convert.ToDouble(Long);
                var Coordinates = db.Stations.Where(u => u.isDeleted == false && u.Status == true && !StationIds.Contains(u.Id));
                foreach (var item in Coordinates)
                {
                    double dx = x - Convert.ToDouble(item.Coordinates.Substring(0, item.Coordinates.IndexOf(',')));
                    double dy = y - Convert.ToDouble(item.Coordinates.Substring(item.Coordinates.IndexOf(',') + 1));
                    differencs.Add(new KeyValuePair<int, double>(item.Id, Math.Sqrt(dx * dx + dy * dy)));
                }
                if (differencs.Count > 0)
                {
                    var ttt = differencs.Min(u => u.Value);
                    return differencs.FirstOrDefault(u => u.Value == ttt).Key;
                }
                else
                    return -1;
            }
        }



        #region Android
        public string AndroidListStation()
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var Objects = db.Stations.Where(u => u.isDeleted == false && u.Status == true).Select(y => new { y.Address, y.Status, y.StaionName, y.Coordinates, y.Tell });
                List<JsonStationListModel> Result = new List<JsonStationListModel>();
                foreach (var item in Objects)
                {
                    JsonStationListModel InsertObject = new JsonStationListModel();
                    InsertObject.Lat = item.Coordinates.Substring(0, item.Coordinates.IndexOf(','));
                    InsertObject.Long = item.Coordinates.Substring(item.Coordinates.IndexOf(',') + 1);
                    InsertObject.Name = item.StaionName;
                    InsertObject.Tell = item.Tell;
                    InsertObject.Address = item.Address;
                    Result.Add(InsertObject);
                }
                dynamic collectionWrapper = new
                {
                    Root = Result
                };
                var output = Newtonsoft.Json.JsonConvert.SerializeObject(collectionWrapper);
                return output;
            }
        }
        #endregion

    }
}