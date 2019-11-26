using B2B_DASHBOARD.Models.Connection;
using B2B_DASHBOARD.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace B2B_DASHBOARD.Models
{
    public class Route
    {
        public string ROTCODE { get; set; }
        public string ROTNAME { get; set; }
        public string LGNCODE { get; set; }
        public string ORGV { get; set; }



        public static List<Route> GetList()
        {
            DBContextMSSQL db = new DBContextMSSQL();
            string query = @"SELECT ROTCODE
	                           ,ROTNAME
	                           ,LGNCODE
	                           ,TRYCODE AS ORGV 
		                        FROM ROUTE 
		                        WHERE _DELETED <> 1";
            DataTable data = db.ReadSql(query);

            if (data.Rows.Count > 0)
            {
                return data.ToList<Route>();
            }

            return null;
        }


    }
}