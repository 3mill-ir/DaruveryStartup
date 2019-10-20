using DeliveryService.Areas.Admin3mill.Models;
using DeliveryService.CustomFilters;
using DeliveryService.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeliveryService.Areas.Admin3mill.Controllers
{
    [AuthLog(Roles = "SystemAdmin")]
    public class DeliveryController : Controller
    {
        public ActionResult ListDeliverys(int? page)
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_DeliveryList;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_Dashboard, "Index", "Home", "btn-purple ti-Home", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            ViewBag.jsNotifyMessage = TempData["Notification"];
            DeliveryManagement dm = new DeliveryManagement();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            int total;
            ViewBag.Pagination = pageNumber;
            var pagedList = new StaticPagedList<DeliveryModel>(dm.ListDeliverys(pageNumber, pageSize, out total), pageNumber, pageSize, total);
            return View(pagedList);
        }

        public ActionResult DeliveryDetail(int DeliveryId)
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_DeliveryDetail;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_Dashboard, "Index", "Home", "btn-purple ti-Home", null));
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_DeliveryList, "ListDeliverys", "Delivery", "btn-purple ti-list", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            ViewBag.jsNotifyMessage = TempData["Notification"];
            DeliveryManagement dm = new DeliveryManagement();
            return View(dm.DeliverDetail(DeliveryId));
        }


        [HttpPost]
        public ActionResult DeliverOrder(string DeliveryIssue, int DeliveryId, int Page)
        {
            DeliveryManagement dm=new DeliveryManagement();
            dm.DeliverOrder(DeliveryId, DeliveryIssue);
            return RedirectToAction("ListDeliverys", "Delivery", new { page = Page });
        }
    }
}