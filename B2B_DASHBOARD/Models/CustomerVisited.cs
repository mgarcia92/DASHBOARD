using B2B_DASHBOARD.Models.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using B2B_DASHBOARD.Utils;
using System.Threading.Tasks;

namespace B2B_DASHBOARD.Models
{
    public class CustomerVisited
    {
        public string CUSTOMER_ID { get; set; }
        public string  NAME { get; set; }


        public static List<CustomerVisited> GetCustomerVisited(string date,int day,string route)
        {
            DBContextMSSQL db = new DBContextMSSQL();
            List<CustomerVisited> list; 
            string query = $@"SELECT * FROM (
                                (SELECT CR.CUSCODE AS CUSTOMER_ID
                                      ,D.DMDCUSTOMERNAME AS NAME
	                                  ,CASE
		                                when  cr.ctrMonday > 0 then '2'
		                                when  cr.ctrTuesday > 0 then '3'
		                                when  cr.ctrWednesday > 0 then '4'
		                                when  cr.ctrThursday > 0 then '5'
		                                when  cr.ctrFriday > 0 then '6'
		                                when  cr.ctrSaturday > 0 then '7'
		                                when  cr.ctrSunday > 0 then '1'
		                                end as [DAY]
	                                FROM customerRoute CR
	                                INNER JOIN demand D ON CR.cusCode = D.cusCode AND CR.rotCode = D.rotCode
	                                WHERE CR.rotCode = '{route}' AND FORMAT(CAST(D.DMDDATE AS DATE),'dd-MM-yyyy') = '{date}')) AS CUSTOMER
	                                WHERE [DAY] = {day}";

            DataTable table = db.ReadSql(query);

            if (table.Rows.Count > 0)
            {
                return  list = table.ToList<CustomerVisited>();
            }

            return list = new List<CustomerVisited>();
        }


        public static async Task<List<CustomerVisited>> GetCustomerVisitedAsync(string date1,string date2, int day, string route)
        {
            DBContextMSSQL db = new DBContextMSSQL();
            List<CustomerVisited> list;
            string query = $@"SELECT * FROM (
                                (SELECT CR.CUSCODE AS CUSTOMER_ID
                                      ,D.DMDCUSTOMERNAME AS NAME
	                                  ,CASE
		                                when  cr.ctrMonday > 0 then '2'
		                                when  cr.ctrTuesday > 0 then '3'
		                                when  cr.ctrWednesday > 0 then '4'
		                                when  cr.ctrThursday > 0 then '5'
		                                when  cr.ctrFriday > 0 then '6'
		                                when  cr.ctrSaturday > 0 then '7'
		                                when  cr.ctrSunday > 0 then '1'
		                                end as [DAY]
	                                FROM customerRoute CR
	                                INNER JOIN demand D ON CR.cusCode = D.cusCode AND CR.rotCode = D.rotCode AND D.DMDCANCELORDER <> 1
	                                WHERE CR.rotCode = '{route}' AND D.DMDDATE >= '{date1}' AND D.DMDDATE <= '{date2}')) AS CUSTOMER
	                                WHERE [DAY] = {day}";

            DataTable table = await db.ReadSqlAsync(query);

            if (table.Rows.Count > 0)
            {
                return list = table.ToList<CustomerVisited>();
            }

            return list = new List<CustomerVisited>();
        }
    }
}