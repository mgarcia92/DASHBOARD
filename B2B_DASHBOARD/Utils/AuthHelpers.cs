using System;
using System.Configuration;
using System.Web;
using System.Web.Security;

namespace B2B_DASHBOARD.Utils
{
    public class AuthHelpers
    {
        public static HttpCookie FormsAuth(string Name, string Login,string pass, string orgv = "", int rol = 0)
        {
            
            string hash = encrypt(pass);
            string infoUser = $"{Name};{orgv};{rol};{hash}";
            FormsAuthenticationTicket auth = new FormsAuthenticationTicket(1, infoUser, DateTime.Now, DateTime.Now.AddMinutes(30), true, Login);
            string EncryptedAuth = FormsAuthentication.Encrypt(auth);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, EncryptedAuth);
            double sessionTime = Convert.ToDouble(ConfigurationManager.AppSettings["session_time"].ToString());
            cookie.Expires = DateTime.Now.AddMinutes(sessionTime);
            //Response.Cookies.Add(cookie);
            return cookie;
        }

        public static FormsAuthenticationTicket FormsDescrypt(HttpCookie authCookie)
        {

            if (authCookie != null)
            {
                FormsAuthenticationTicket forms = FormsAuthentication.Decrypt(authCookie.Value);
                return forms;
            }
            else
            {
                return null;
            }

        }

        public static void Signout()
        {
            FormsAuthentication.SignOut();
        }

        public static string GetName(HttpCookie Cookie)
        {
            HttpCookie cookie = Cookie;
            FormsAuthenticationTicket session = FormsDescrypt(cookie);
            string[] info = session.Name.Split(';');
            if (info.Length > 0)
            {
                return info[0];
            }
            return $"undefined";
        }

        public static string GetOrgv(HttpCookie Cookie)
        {
            HttpCookie cookie = Cookie;
            FormsAuthenticationTicket session = FormsDescrypt(cookie);
            string[] info = session.Name.Split(';');
            if (info.Length > 0)
            {
                return info[1];
            }
            return $"undefined";
        }

        public static int GetUserRol(HttpCookie Cookie)
        {
            HttpCookie cookie = Cookie;
            int rol = 0;
            FormsAuthenticationTicket session = FormsDescrypt(cookie);
            string[] info = session.Name.Split(';');
            if (info.Length > 0)
            {
                int.TryParse(info[2], out rol);
            }
            return rol;
        }

        public static string GetUserId(HttpCookie Cookie)
        {
            HttpCookie cookie = Cookie;
            FormsAuthenticationTicket session = FormsDescrypt(cookie);
            string id = session.UserData.Trim();
            return id;
        }

        public static string GetUserPass(HttpCookie Cookie)
        {
            FormsAuthenticationTicket session = FormsDescrypt(Cookie);
            string[] data = session.Name.Split(';');
            if (data.Length > 0)
            {
                string pass = descrypt(data[3]);
                return pass;
            }
            return string.Empty;
        }

        public static string encrypt(string str)
        {
            string _result = string.Empty;
            char[] temp = str.ToCharArray();
            foreach (var _singleChar in temp)
            {
                var i = (int)_singleChar;
                i = i - 2;
                _result += (char)i;
            }
            return _result;
        }
        public static string descrypt(string str)
        {
            string _result = string.Empty;
            char[] temp = str.ToCharArray();
            foreach (var _singleChar in temp)
            {
                var i = (int)_singleChar;
                i = i + 2;
                _result += (char)i;
            }
            return _result;
        }
    }
}