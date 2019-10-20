using DeliveryService.CustomFilters;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeliveryService.Areas.Admin3mill.Controllers
{
    [AuthLog(Roles = "SystemAdmin")]
    public class UtilityController : Controller
    {
        public ActionResult StationsResponsibilaty()
        {
            using (DeliveryService.Models.DBEntities db = new DBEntities())
            {
                var Statuses = (from c in db.Requests group c by c.Status into g select new { FieldCount = g.Count(), FieldName = g.Key }).ToList();
                List<ChartModel> model = new List<ChartModel>();
                foreach (var item in Statuses)
                {
                    ChartModel m = new ChartModel();
                    m.Text = item.FieldName;
                    m.Value = item.FieldCount + 1;
                    model.Add(m);
                }
                return View(model);
            }
        }
    }
}