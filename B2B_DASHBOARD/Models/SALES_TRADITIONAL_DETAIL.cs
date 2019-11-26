using B2B_DASHBOARD.Models.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using B2B_DASHBOARD.Utils;
namespace B2B_DASHBOARD.Models
{
    public class SALES_TRADITIONAL_DETAIL
    {
        public int Product { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }


        public SALES_TRADITIONAL_DETAIL()
        {

        }


        public static List<SALES_TRADITIONAL_DETAIL> TRADITIONAL_DETAIL(string ORDER_ID)
        {
            DBContextMSSQL db = new DBContextMSSQL();
            string query = string.Empty;

            if (ORDER_ID != string.Empty)
            {
                query = $@"SELECT DPR.PROCODE AS PRODUCT
	                           ,P.PRONAME AS NAME
	                           ,DPR.DPTQUANTITY AS QUANTITY
	                           ,DPR.UNTCODE AS UNIT
	                           ,DPR.DPTPRICE AS PRICE
		                        FROM DEMANDPRODUCT DPR
		                        INNER JOIN PRODUCT P ON DPR.PROCODE = P.PROCODE
		                        WHERE DPR.DMDCODE = '{ORDER_ID}'";

                DataTable data = db.ReadSql(query);

                if (data.Rows.Count > 0)
                {
                    return data.ToList<SALES_TRADITIONAL_DETAIL>();
                }
            }

            return null;


        }


    }
}