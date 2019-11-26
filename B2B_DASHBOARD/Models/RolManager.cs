using B2B_DASHBOARD.Models.Connection;
using B2B_DASHBOARD.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B_DASHBOARD.Models
{
    public class RolManager
    {
        public int ID { get; set; }
        public string CONTROLLERNAME { get; set; }
        public string ACTIONCONTROLLER { get; set; }
        public bool MAINCONTROLLER { get; set; }
        public string DESCRIPTION { get; set; }


        public static RolManager GetMainController(int rol)
        {
            DBContextMysql db = new DBContextMysql("db_dashboard");

            MySqlParameter[] paramaters =
            {
                new MySqlParameter("prol",rol)
            };

            var data = db.ExecProcedure("sp_get_maincontroller_by_rol", paramaters);

            if (data.Rows.Count > 0)
            {
                return data.toObject<RolManager>();
            }

            return null;
        }
    }
}