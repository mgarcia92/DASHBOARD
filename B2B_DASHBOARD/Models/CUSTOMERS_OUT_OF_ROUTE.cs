using B2B_DASHBOARD.Models.Connection;
using B2B_DASHBOARD.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace B2B_DASHBOARD.Models
{
    public class CUSTOMERS_OUT_OF_ROUTE
    {
        public string cuscode { get; set; }
        public string rotcode  { get; set; }
        public string reaname { get; set; }
        public bool reanovisit { get; set; }
        public bool reanosale { get; set; }
        public bool reanocollect { get; set; }
        public bool readmd { get; set; }
        public bool reapay { get; set; }
        public string cuslatitude { get; set; }
        public string cuslongitude { get; set; }


        public static List<CUSTOMERS_OUT_OF_ROUTE> GetCustomers(string route)
        {
            try
            {
                DBContextMysql db = new DBContextMysql("db_dashboard");
                List<CUSTOMERS_OUT_OF_ROUTE> list;
                MySqlParameter[] paramater =
               {
                    new MySqlParameter("ROUTE",route),
               };

               DataTable data = db.ExecProcedure("sp_customers_out_of_route", paramater);

                if (data.Rows.Count > 0)
                {
                    list = data.ToList<CUSTOMERS_OUT_OF_ROUTE>();
                }
                else
                {
                    list = new List<CUSTOMERS_OUT_OF_ROUTE>();
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static async Task<List<CUSTOMERS_OUT_OF_ROUTE>> GetCustomersAsync(string route)
        {
            try
            {
                DBContextMysql db = new DBContextMysql();
                List<CUSTOMERS_OUT_OF_ROUTE> list;
                MySqlParameter[] paramater =
               {
                    new MySqlParameter("ROUTE",route),
               };

                DataTable data = await db.ExecProcedureAsync("sp_customers_out_of_route", paramater);

                if (data.Rows.Count > 0)
                {
                    list = data.ToList<CUSTOMERS_OUT_OF_ROUTE>();
                }
                else
                {
                    list = new List<CUSTOMERS_OUT_OF_ROUTE>();                    
                }
                return list;            
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}