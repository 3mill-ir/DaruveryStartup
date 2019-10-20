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
    public class ClientController : Controller
    {
        public ActionResult ListClients(int? page)
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_ListClient;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_Dashboard, "Index", "Home", "btn-purple ti-List", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            ViewBag.jsNotifyMessage = TempData["Notification"];
            ClientManagement sm = new ClientManagement();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            int total;
            ViewBag.Pagination = pageNumber;
            var pagedList = new StaticPagedList<ClientModel>(sm.ListClient(pageNumber, pageSize, out total), pageNumber, pageSize, total);
            return View(pagedList);
        }

        [HttpPost]
        public ActionResult ChangeDisplayClient(int ClientId, int Page)
        {
            ClientManagement sm = new ClientManagement();
            if (sm.ChangeStatusClient(ClientId) == "NotFound")
                TempData["Notification"] = "error";
            else
                TempData["Notification"] = "success";
            return RedirectToAction("ListClients", "Client", new { page = Page });
        }

        [HttpPost]
        public ActionResult DeleteClient(int ClientId, int Page)
        {
            ClientManagement sm = new ClientManagement();
            if (sm.DeleteClient(ClientId) == "NotFound")
                TempData["Notification"] = "error";
            else
                TempData["Notification"] = "success";
            return RedirectToAction("ListClients", "Client", new { page = Page });
        }


        public ActionResult DetailClient(int ClientId)
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_DetailClient;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_ListClient, "ListClients", "Client", "btn-purple ti-view-list-alt", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            ClientManagement sm = new ClientManagement();
            return View(sm.DetailClient(ClientId));
        }
    }
}