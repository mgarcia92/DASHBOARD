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
    public class DayofWeek
    {
        public int DAY_ID { get; set; }
        public string DAY_NAME { get; set; }
        public string DAY_NAME_ES { get; set; }


        public static List<DayofWeek> GetDaysOfWeek()
        {
            try
            {
                DBContextMysql db = new DBContextMysql("db_dashboard");
                DataTable data = db.ExecProcedure("sp_get_dayofweek");
                if (data.Rows.Count > 0)
                {
                    return data.ToList<DayofWeek>();
                }

                return null;
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }
    }
}