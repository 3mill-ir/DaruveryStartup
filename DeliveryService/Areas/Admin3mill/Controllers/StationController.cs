using DeliveryService.Areas.Admin3mill.Models;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DeliveryService.CustomFilters;

namespace DeliveryService.Areas.Admin3mill.Controllers
{
    [AuthLog(Roles = "SystemAdmin")]
    public class StationController : Controller
    {
        public ActionResult ListStations()
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_ListStation;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_AddStation, "AddStation", "Station", "btn-green ti-Add", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            ViewBag.jsNotifyMessage = TempData["Notification"];
            StationManagement sm = new StationManagement();
            return View(sm.ListStation());
        }




        public ActionResult AddStation()
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_AddStation;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_ListStation, "ListStations", "Station", "btn-purple ti-list", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStation(StationModel model)
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_AddStation;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_ListStation, "ListStations", "Station", "btn-purple ti-list", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            StationManagement sm = new StationManagement();
            if (ModelState.IsValid)
            {
                model.Tell = '0' + model.Tell.Replace(")", "").Replace("(", "").Replace("-", "").Replace(" ", "");
                string scale = sm.AddStation(model);
                if (scale == "OK")
                    TempData["Notification"] = "success";
                else
                {
                    TempData["Notification"] = "حساب کاربری تکراری می باشد و یا حاوی کاراکتر های غیر مجاز نظیر - و _ می باشد   !";
                    return View(model);
                }
                return RedirectToAction("ListStations", "Station");
            }
            return View(model);
        }

        public ActionResult EditStation(int StationId)
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_EditStation;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_ListStation, "ListStations", "Station", "btn-purple ti-view-list-alt", null));
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_AddStation, "AddStation", "Station", "btn-green ti-plus", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            StationManagement sm = new StationManagement();
            return View(sm.DetailStation(StationId, "Edit"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStation(StationModel model)
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_EditStation;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_ListStation, "ListStations", "Station", "btn-purple ti-view-list-alt", null));
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_AddStation, "AddStation", "Station", "btn-green ti-plus", null));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            StationManagement sm = new StationManagement();
            ModelState["Password"].Errors.Clear();
            ModelState["UserName"].Errors.Clear();
            ModelState["ConfirmPassword"].Errors.Clear();
            if (ModelState.IsValid)
            {
                model.Tell = "0" + model.Tell.Replace(")", "").Replace("(", "").Replace("-", "").Replace(" ", "");
                string scale = sm.EditStation(model);
                if (scale == "OK")
                    TempData["Notification"] = "success";
                else if (scale == "ChangePasswordError")
                {
                    TempData["Notification"] = "خطا در ویرایش رمز عبور ! لطفا مجددا تلاش کنید";
                    return View(model);
                }
                else
                    TempData["Notification"] = "error";
                return RedirectToAction("ListStations", "Station");
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult ChangeDisplayStation(int StationId)
        {
            StationManagement sm = new StationManagement();
            if (sm.ChangeStatusStation(StationId) == "NotFound")
                TempData["Notification"] = "error";
            else
                TempData["Notification"] = "success";
            return RedirectToAction("ListStations", "Station");
        }

        [HttpPost]
        public ActionResult DeleteStation(int StationId)
        {
            StationManagement sm = new StationManagement();
            if (sm.DeleteStation(StationId) == "NotFound")
                TempData["Notification"] = "error";
            else
                TempData["Notification"] = "success";
            return RedirectToAction("ListStations", "Station");
        }


        public ActionResult DetailStation(int StationId)
        {
            //************ Start Page Tittle *****************************
            ViewBag.PageTittle_Tittle = Resource.Resource.PageTittle_DetailStation;
            ViewBag.PageTittle_Description = "توضیحات";
            ViewBag.PageTittle_ContactUS = Resource.Resource.PageTittle_ContactUS;
            List<PageTitle> PathLog = new List<PageTitle>();
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_ListStation, "ListStations", "Station", "btn-purple ti-view-list-alt", null));
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_AddStation, "AddStation", "Station", "btn-green ti-plus", null));
            PathLog.Add(new PageTitle(Resource.Resource.PageTittle_EditStation, "EditStation", "Station", "btn-purple ti-view-list-alt", "?StationId=" + StationId));
            ViewBag.PathLog = PathLog;
            //************ End Page Tittle *****************************
            StationManagement sm = new StationManagement();
            return View(sm.DetailStation(StationId));
        }

    }
}