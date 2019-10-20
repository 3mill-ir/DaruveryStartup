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
    [Authorize]
    public class RequestController : Controller
    {
        [AuthLog(Roles = "Station")]
        public ActionResult RequestManagement(int? page, int stationId)
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_RequestManagement;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_Dashboard, "Index", "Home", "btn-purple ti-Home", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            ViewBag.jsNotifyMessage = TempData["Notification"];
            RequestManagement rm = new RequestManagement();
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            int total;
            ViewBag.Pagination = pageNumber;
            var pagedList = new StaticPagedList<RequestManagementModel>(rm.ListRequests(stationId, pageNumber, pageSize, out total), pageNumber, pageSize, total);
            ViewBag.StationId = stationId;
            return View(pagedList);
        }

        [HttpPost]
        public string ClientRejectAcceptRequest(int RequestId, string Type)
        {
            RequestManagement rm = new RequestManagement();
            return rm.ClientAcceptRejectRequest(RequestId, Type);
        }

        [HttpPost]
        public string SendRequestToServer(string Address, string Lat, string Long, HttpPostedFileBase Img)
        {
            if (Img != null)
            {
                RequestManagement rm = new RequestManagement();
                return rm.AndroidSendRequestToServer(Address, Lat, Long, Img);
            }
            else
                return "Error";
        }

        [AuthLog(Roles = "Station")]
        public ActionResult AcceptRequest(double? Price, string ResponseIssue, int RequestId, int StationId, int Page)
        {
            if (Price == null)
                TempData["Notification"] = "فیلد قیمت پیشنهادی خالی می باشد !";
            else
            {
                RequestManagement rm = new RequestManagement();
                if (rm.AcceptRejectRequest(ResponseIssue, RequestId, StationId, "OK", Price) == "NotFound")
                    TempData["Notification"] = "error";
                else
                    TempData["Notification"] = "success";
                TempData["Notification"] = "success";
            }
            return RedirectToAction("RequestManagement", "Request", new { page = Page, stationId = StationId });
        }


        [AuthLog(Roles = "Station")]
        public ActionResult RejectRequest(string ResponseIssue, int RequestId, int StationId, int Page)
        {
            RequestManagement rm = new RequestManagement();
            if (rm.AcceptRejectRequest(ResponseIssue, RequestId, StationId, "Rejected") == "NotFound")
                TempData["Notification"] = "error";
            else
                TempData["Notification"] = "success";
            rm.ChooseRecoveryStation(RequestId);
            return RedirectToAction("RequestManagement", "Request", new { page = Page, stationId = StationId });
        }


        [AuthLog(Roles = "Station")]
        public ActionResult PayPrice(int RequestId, int StationId, int Page)
        {
            RequestManagement rm = new RequestManagement();
            if (rm.PayPrice(RequestId) == "NotFound")
                TempData["Notification"] = "error";
            else
                TempData["Notification"] = "success";
            return RedirectToAction("StationActivityReport", "Report", new { page = Page, stationId = StationId });
        }
    }
}