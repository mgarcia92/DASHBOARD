using B2B_DASHBOARD.Models;
using B2B_DASHBOARD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace B2B_DASHBOARD.Controllers
{
    [Authorize]
    public class PermissionsController : Controller
    {
        // GET: Permissions
        public ActionResult GetPermissions()
        {   //LOGICA PARA VALIDAR A QUE CONTROLADOR PUEDE INGRESAR CADA ROL
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var rol = AuthHelpers.GetUserRol(cookie);
            var controller = RolManager.GetMainController(rol);

            if (controller != null)
            {
                return RedirectToAction(controller.ACTIONCONTROLLER, controller.CONTROLLERNAME);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}