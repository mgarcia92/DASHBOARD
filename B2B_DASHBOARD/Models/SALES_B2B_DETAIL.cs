using B2B_DASHBOARD.Models.Connection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using B2B_DASHBOARD.Utils;

namespace B2B_DASHBOARD.Models
{
    public class SALES_B2B_DETAIL
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Amount { get; set; }
        public string Unit { get; set; }


        public SALES_B2B_DETAIL()
        {

        }


        public static List<SALES_B2B_DETAIL> ORDER_DETAILS(int ORDER)
        {
            try
            {
                DBContextMysql db = new DBContextMysql();

                MySqlParameter[] paramaters =
                {
                new MySqlParameter("_orden",ORDER),
                 };

                DataTable data = db.ExecProcedure("sales_by_advisor_detail", paramaters);

                if (data.Rows.Count > 0)
                {
                    return data.ToList<SALES_B2B_DETAIL>();
                }

                return null;
            }
            catch (MySqlException)
            {

                throw;
            }

           
        }
    }
}