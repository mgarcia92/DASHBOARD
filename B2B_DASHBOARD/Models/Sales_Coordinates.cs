using B2B_DASHBOARD.Models.Connection;
using B2B_DASHBOARD.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace B2B_DASHBOARD.Models
{
	public class Sales_Coordinates
	{
        public string CODIGO { get; set; }
        public string COMERCIO { get; set; }
        public string DIRECCION { get; set; }
        public string TELEFONO { get; set; }
        public string PERSONA_CONTACTO { get; set; }
        public string LATITUD  { get; set; }
        public string LONGITUD{ get; set; }

        public Sales_Coordinates() { }

             public static List<Sales_Coordinates> Cordenadas(string Route, int Day_Active)
                {
                    string query = string.Empty;                    
                    DBContextMSSQL db = new DBContextMSSQL();
                    query = $@"SELECT *
                                     FROM((select
                                            cusBusinessName AS 'COMERCIO',
                                            CONCAT(cusStreet1, cusStreet2) AS 'DIRECCION'
                                            , cusPhone AS 'TELEFONO'
                                            , C.cusContactPerson AS 'PERSONA_CONTACTO'
                                            , cusLatitude AS 'LATITUD'
                                            , cusLongitude AS 'LONGITUD'
                                        , CASE
                                            when  cr.ctrMonday > 0 then '2'
                                            when  cr.ctrTuesday > 0 then '3'
                                            when  cr.ctrWednesday > 0 then '4'
                                            when  cr.ctrThursday > 0 then '5'
                                            when  cr.ctrFriday > 0 then '6'
                                            when  cr.ctrSaturday > 0 then '7'
                                            when  cr.ctrSunday > 0 then '1'
                                            end as Dia
                                            FROM dbo.customerRoute  cr
                                            INNER JOIN customer c ON cr.cusCode = c.cusCode
                                            WHERE CR.rotCode = '{Route}')) as cust
                                        WHERE Dia = '{Day_Active}'";

                    DataTable table = db.ReadSql(query);

                    if (table.Rows.Count > 0)
                    {                        
                        return table.ToList<Sales_Coordinates>();
                    }
                    return null;
                }


        public static async Task<List<Sales_Coordinates>> CordenadasAsync(string Route, int Day_Active)
        {
            string query = string.Empty;
            DBContextMSSQL db = new DBContextMSSQL();
            query = $@"SELECT *
                                     FROM((select
                                            cr.cuscode AS 'CODIGO',
                                            cusBusinessName AS 'COMERCIO',
                                            CONCAT(cusStreet1, cusStreet2) AS 'DIRECCION'
                                            , cusPhone AS 'TELEFONO'
                                            , C.cusContactPerson AS 'PERSONA_CONTACTO'
                                            , cusLatitude AS 'LATITUD'
                                            , cusLongitude AS 'LONGITUD'
                                        , CASE
                                            when  cr.ctrMonday > 0 then '2'
                                            when  cr.ctrTuesday > 0 then '3'
                                            when  cr.ctrWednesday > 0 then '4'
                                            when  cr.ctrThursday > 0 then '5'
                                            when  cr.ctrFriday > 0 then '6'
                                            when  cr.ctrSaturday > 0 then '7'
                                            when  cr.ctrSunday > 0 then '1'
                                            end as Dia
                                            FROM dbo.customerRoute  cr
                                            INNER JOIN customer c ON cr.cusCode = c.cusCode
                                            WHERE CR.rotCode = '{Route}')) as cust
                                        WHERE Dia = '{Day_Active}'";

            DataTable table = await db.ReadSqlAsync(query);

            if (table.Rows.Count > 0)
            {
                return table.ToList<Sales_Coordinates>();
            }
            return null;
        }


        public static int GetDay()
        {
            int day = 0;
            DBContextMSSQL db = new DBContextMSSQL();
            string query = @"SELECT DATEPART(DW,GETDATE()) AS DAY";

            DataTable result = db.ReadSql(query);
           
            if (result.Rows.Count > 0)
            {          
                return Convert.ToInt16(result.Rows[0]["DAY"]);
            }
            return day;           
        }


    }

   


}