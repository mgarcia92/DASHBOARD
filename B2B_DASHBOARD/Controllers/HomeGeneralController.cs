using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B2B_DASHBOARD.Controllers
{
    [Authorize]
    public class HomeGeneralController : Controller
    {
        // GET: HomeGeneral
        public ActionResult Index()
        {
            return View();
        }
    }
}