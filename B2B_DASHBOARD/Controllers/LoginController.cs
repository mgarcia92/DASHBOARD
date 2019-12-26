
using B2B_DASHBOARD.Enums;
using B2B_DASHBOARD.Models;
using B2B_DASHBOARD.Models.Repository;
using B2B_DASHBOARD.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace B2B_DASHBOARD.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(string user, string password)
        {
            AuthHelpers.Signout();
            Request.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(-1);
            if ( !string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password))
            {
               HttpCookie cookie;
               user = user.Trim();
               string pass = password.Trim();
               LoginUser login = new LoginUser();
               login =  login.LoginPage(user,pass);
                if (login != null)
                {    //ROL ASESOR
                    if (login.rol_id.Equals((short)Enums.Rol.Asesor))
                    {
                        cookie = AuthHelpers.FormsAuth(login.Name, login.Login, pass, LoginUser.GetOrgv(user), login.rol_id);
                    }
                    else
                    {                
                        cookie = AuthHelpers.FormsAuth(login.Name, login.Login, pass, rol: login.rol_id);
                    }
                  
                    Response.Cookies.Add(cookie);
                    return Json(new {  login, ok = true });
                }
                else
                {
                    ViewBag.Message = "Usuario O Clave Incorrectos";
                    return Json(new { ok = false});
                }   
            }
            else
            {
                ViewBag.Message = "Escriba un Usuario y Clave";
                return Json(new { ok = false });
            }
        }

        public ActionResult SignOut()
        {
            //CERRAR SESION 
            AuthHelpers.Signout();
            Request.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(-1);
            //HttpCookie authCookied = Context.Request.Cookies[FormsAuthentication.FormsCookieName];

            return RedirectToAction("Index", "Login");

        }

        public string EncryptPass(string pass)
        {
            string clave1 = "b2b";
            string clave2 = "plumr0S3";
            string hash = $"{clave1}{pass}{clave2}";
            byte[] Bytes = Encoding.ASCII.GetBytes(hash);

            //Crear objeto MD5
            MD5 Md5 = MD5.Create();

            //Convertir la data a un arreglo de Bytes
            byte[] data = Md5.ComputeHash(Bytes);

            //Convertir de arreglo bytes[] a un array string[]
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                StringBuilder hex = new StringBuilder();
                hex.Append(Convert.ToInt32(data[i]).ToString("x2"));
                if (hex.Length < 2)
                {
                    hex.Insert(0, '0');
                }
                sb.Append(hex);
            }

            return sb.ToString();
        }


        public FileContentResult GetPictureUser()
        {
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                using (MemoryStream st = new MemoryStream())
                {
                    FileContentResult File = null;
                    Ldap ad = new Ldap();
                    Image image = ad.GetUserPicture(AuthHelpers.GetUserId(cookie), AuthHelpers.GetUserPass(cookie));
                    image.Save(st, System.Drawing.Imaging.ImageFormat.Gif);
                    File = new FileContentResult(st.ToArray(), "image/jpg");
                    return File;
                }          
        }
    }
}