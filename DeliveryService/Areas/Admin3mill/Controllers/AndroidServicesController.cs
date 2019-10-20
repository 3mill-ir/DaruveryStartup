using DeliveryService.Areas.Admin3mill.Models;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DeliveryService.Areas.Admin3mill.Controllers
{
    [Authorize]
    public class AndroidServicesController : ApiController
    {
        [HttpPost]
        public string AndroidListStations()
        {
            StationManagement sm = new StationManagement();
            return sm.AndroidListStation();
        }

        [HttpPost]
        public string AndroidListRequests()
        {
            RequestManagement rm = new RequestManagement();
            return rm.AndroidListRequests();
        }

    }
}
