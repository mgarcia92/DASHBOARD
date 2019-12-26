using B2B_DASHBOARD.Models.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using B2B_DASHBOARD.Utils;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Configuration;
using System.Collections;

namespace B2B_DASHBOARD.Models
{
    public class LoginUser
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public short rol_id { get; set; }

         
        public LoginUser LoginPage(string user, string password)
        {
            LoginUser logon = null;

            logon = LoginUsers(user,password);

            if (logon != null)
            {
                return logon;
            }

            logon = LoginRoute(user,password);

            if (logon != null)
            {
                return logon;
            }

            return logon;

        }


        public LoginUser LoginUsers(string User, string Password)
        {
            LoginUser Login = null;
          
            try
            {
                if (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password))
                {
                    DBContextMysql db = new DBContextMysql("db_dashboard");
                    MySqlParameter[] parameters = {
                                                 new MySqlParameter("user",User)
                                                 //,new MySqlParameter("pass", Password)
                                              };

                    DataTable table = db.ExecProcedure("sp_login_dashboard", parameters);

                    if (table.Rows.Count > 0)
                    {
                        if (LoginLdap(User, Password))
                        {
                            return Login = table.toObject<LoginUser>();
                        }
                    }
                }
                return Login;
            }
            catch (Exception)
            {

                return Login;
            }
        }



        public LoginUser LoginRoute(string Route,string Password)
        {
            LoginUser Login = null;

            if (!string.IsNullOrEmpty(Route) && !string.IsNullOrEmpty(Password))
            {
                DBContextMSSQL dbMSSQL = new DBContextMSSQL();
                DataTable table = null;
                table = dbMSSQL.ReadSql($@"SELECT 
		                                        L.lgnCode AS LOGIN
	                                           ,R.rotName as NAME
	                                           ,R.rotDummy1 AS EMAIL
	                                           ,'2' AS ROL_ID
		                                        FROM [LOGIN] L
		                                        INNER JOIN [ROUTE] R ON R.lgnCode = L.lgnCode 
                                                AND R._deleted <> 1
		                                        WHERE L.LGNCODE = '{Route}' 
                                                AND L.lgnPassword = '{Password}'");
                if (table.Rows.Count > 0)
                {
                    return Login =  table.toObject<LoginUser>();
                }
            }

            return Login;          
        }

        public bool LoginLdap(string User, string Password)
        {
            Ldap ldap = new Ldap();
            ldap.FindOne(User,Password);
            return ldap.success;
        }


        public static string GetOrgv(string route)
        {
            DBContextMSSQL dbMSSQL = new DBContextMSSQL();
            string orgv = string.Empty;
            DataTable table = null;
            table = dbMSSQL.ReadSql($@"SELECT TRYCODE AS ORGV FROM [ROUTE] WHERE rotCode = '{route}'");

            if (table.Rows.Count > 0)
            {
                 orgv = (from datos in table.AsEnumerable()
                         select datos.Field<string>("ORGV")).FirstOrDefault<string>().Trim();

                return orgv;
            }
            return orgv;
        }

    }
}