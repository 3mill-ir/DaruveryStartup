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
    public class ReportController : Controller
    {
        #region Station
        [AuthLog(Roles = "Station,SystemAdmin")]
        public ActionResult StationActivityReport(int? page, int stationId)
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_SingleStationResponse;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_Dashboard, "Index", "Home", "btn-purple ti-List", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            ReportManagement rm = new ReportManagement();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            int total;
            ViewBag.Pagination = pageNumber;
            var pagedList = new StaticPagedList<StationReportModel>(rm.StationActivityReport(stationId, pageNumber, pageSize, out total), pageNumber, pageSize, total);
            ViewBag.StationId = stationId;
            return View(pagedList);
        }

        [AuthLog(Roles = "Station,SystemAdmin")]
        public ActionResult StationResponseDetail(int RequestId, int StationId, int page)
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_StationResponseDetail;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_StationResponse, "StationActivityReport", "Report", "btn-purple ti-view-list-alt", "?stationId=" + StationId + "&page=" + page));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            ReportManagement rm = new ReportManagement();
            return View(rm.StationActivityDetail(StationId, RequestId));
        }
        #endregion

        #region Admin
        [AuthLog(Roles = "SystemAdmin")]
        public ActionResult AllStationActivityReport()
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_StationResponse;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_Dashboard, "Index", "Home", "btn-purple ti-List", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            ViewBag.jsNotifyMessage = TempData["Notification"];
            StationManagement sm = new StationManagement();
            return View(sm.ListStation());
        }

        [AuthLog(Roles = "SystemAdmin")]
        public ActionResult FindClient()
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_FindClient;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_Dashboard, "Index", "Home", "btn-purple ti-List", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            return View();
        }

        [AuthLog(Roles = "SystemAdmin")]
        public ActionResult ClientActivityReport(string UserName, int? page)
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_ClientActivityReport;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_Dashboard, "Index", "Home", "btn-purple ti-List", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            RequestManagement rm = new RequestManagement();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            int total;
            ViewBag.Pagination = pageNumber;
            ViewBag.UserName = UserName;
            var pagedList = new StaticPagedList<RequestManagementModel>(rm.ClientRequests("0" + UserName.Replace(")", "").Replace("(", "").Replace("-", "").Replace(" ", ""), pageNumber, pageSize, out total), pageNumber, pageSize, total);
            return View(pagedList);
        }

        [AuthLog(Roles = "SystemAdmin")]
        public ActionResult ClientRequestDetail(int RequestId, int page,string UserName)
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_StationResponseDetail;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_StationResponse, "ClientActivityReport", "Report", "btn-purple ti-view-list-alt", "?UserName=" + UserName + "&page=" + page));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            ReportManagement rm = new ReportManagement();
            return View(rm.ClientRequestDetail(RequestId));
        }
        #endregion
    }
}