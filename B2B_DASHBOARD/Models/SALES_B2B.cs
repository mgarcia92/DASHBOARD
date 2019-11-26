using B2B_DASHBOARD.Models.Connection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using B2B_DASHBOARD.Utils;
using System.Threading.Tasks;


namespace B2B_DASHBOARD.Models
{
    public class SALES_B2B
    {
        public int ORDER_ID { get; set; }
        public string LOGIN { get; set; }
        public string PLANT_ID { get; set; }
        public string ORG_VENTAS { get; set; }
        public string CHANNEL { get; set; }
        public string PLANT { get; set; }
        public string CUSTOMER { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string AMOUNT { get; set; }
        public string STATUS { get; set; }
        public string ADVISOR_ID { get; set; }
        public string CREATION_DATE { get; set; }

        public SALES_B2B()
        {

        }

        public static async Task<List<SALES_B2B>>  Sales_By_AdvisorAsync(string Advisor, string Date1, string Date2)
        {
            try
            {
                string route = string.Concat("AS000", Advisor).Trim();
                DBContextMysql db = new DBContextMysql();

                MySqlParameter[] paramaters =
                {
                new MySqlParameter("advisor",route),
                new MySqlParameter("fecha_ini",Date1),
                new MySqlParameter("fecha_final",Date2)
            };

                DataTable data = await db.ExecProcedureAsync("sp_b2b_sales_by_adivisor", paramaters);

                if (data.Rows.Count > 0)
                {
                    return data.ToList<SALES_B2B>();
                }

                return null;
            }
            catch (MySqlException)
            {

                throw;
            }
           
        }


        public static List<SALES_B2B> Sales_By_Advisor(string Advisor, string Date1, string Date2)
        {
            try
            {
                string route = string.Concat("AS000", Advisor).Trim();
                DBContextMysql db = new DBContextMysql();

                MySqlParameter[] paramaters =
                {
                new MySqlParameter("advisor",route),
                new MySqlParameter("fecha_ini",Date1),
                new MySqlParameter("fecha_final",Date2)
            };

                DataTable data = db.ExecProcedure("sp_b2b_sales_by_adivisor", paramaters);

                if (data.Rows.Count > 0)
                {
                    return data.ToList<SALES_B2B>();
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