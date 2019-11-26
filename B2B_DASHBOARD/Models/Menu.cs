using B2B_DASHBOARD.Models.Connection;
using B2B_DASHBOARD.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace B2B_DASHBOARD.Models
{
    public class Menu
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string AliasMenu { get; set; }
        public string Icon { get; set; }
        public string Active { get; set; }


        public static List<Menu> GetMenu(int rol)
        {
            DBContextMysql db = new DBContextMysql("db_dashboard");
            MySqlParameter[] parameters = {
                                                 new MySqlParameter("PROL",rol)
                                              };
            List<Menu> menu = new List<Menu>();
            DataTable table = db.ExecProcedure("sp_get_menu", parameters);

            if (table.Rows.Count > 0)
            {                              
                menu = table.ToList<Menu>();               
            }

            return menu;
        }
    }
}