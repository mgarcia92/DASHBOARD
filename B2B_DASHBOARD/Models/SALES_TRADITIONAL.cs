using B2B_DASHBOARD.Models.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using B2B_DASHBOARD.Utils;
using System.Threading.Tasks;

namespace B2B_DASHBOARD.Models
{
    public class SALES_TRADITIONAL
    {
        public string ORDER { get; set; }
        public string ROUTE { get; set; }
        public string CUSTOMER { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string PLANT { get; set; }
        public string PLANT_NAME { get; set; }
        public string CHANNEL { get; set; }
        public string DOCTYPE { get; set; }
        public string TYPE { get; set; }
        public string STATUS { get; set; }
        public string KG { get; set; }
        public string CREATION_DATE { get; set; }


        public SALES_TRADITIONAL()
        {

        }

        public static List<SALES_TRADITIONAL> SalesByRoute(string Route, string Date1, string Date2 = "")
        {
            string query = string.Empty;          
            DBContextMSSQL db = new DBContextMSSQL();

          if (Route != string.Empty && Date1 != string.Empty && Date2 == string.Empty)
            {
                query = $@"SELECT DISTINCT
                    D.DMDCODE AS[ORDER]
                    ,R.rotcode as [ROUTE]
                    ,D.CUSCODE AS CUSTOMER
                    ,C.CUSNAME AS CUSTOMER_NAME
                    ,B.BRCCODE AS PLANT
                    ,B.BRCNAME AS PLANT_NAME
                    , T3.TP3NAME AS CHANNEL
		            ,DT.DOCCODE AS DOCTYPE
                    ,DT.DOCNAME AS [TYPE]
                    ,CASE D.DMDPROCESS
                    WHEN '0' THEN
                    'PENDIENTE'
                    WHEN '1' THEN
                    'SAP'
                    END AS [STATUS]
                    ,(
	                    SELECT
			                CASE  
				                WHEN SUM(DP.kilogramos) IS NULL THEN '0'
				                ELSE
				                SUM(DP.kilogramos) 
				                END	AS PESO
		                FROM (
			                SELECT
				                (SELECT                                  
				                (prumultiplyby * dpt.dptquantity)
					                FROM productunit
					                 WHERE procode =  dpt.procode and untcode = dpt.untcode
			                ) as kilogramos	
				                FROM demandproduct dpt 
					                WHERE dpt.dmdcode = D.DMDCODE
		                ) AS DP		
		                ) AS KG
		            ,FORMAT(D.DMDDATE,'dd-MM-yyyy') AS CREATION_DATE
                    FROM DBO.DEMAND D
                    INNER JOIN DEMANDPRODUCT DPR ON D.DMDCODE = DPR.dmdCode
                    INNER JOIN [ROUTE] R ON D.rotCode = R.rotCode AND R._DELETED<> 1
                    INNER JOIN CUSTOMER C ON D.cusCode = C.cusCode
                    INNER JOIN BRANCH B ON R.TRYCODE = B.BRCCODE
                    INNER JOIN TYPE3 T3 ON C.TP3CODE = T3.TP3CODE
                    INNER JOIN DOCUMENT DT ON D.DOCCODE = DT.DOCCODE AND DOCDEMAND = 1
                    WHERE CAST(DMDDATE AS DATE) = '{Date1}' AND R.rotCode = '{Route}' AND D.DMDCANCELORDER <> 1";
            }else if (Route != string.Empty && Date1 != string.Empty && Date2 != string.Empty)
            {
                query = $@"SELECT DISTINCT
                    D.DMDCODE AS[ORDER]
                    ,R.rotcode as [ROUTE]
                    ,D.CUSCODE AS CUSTOMER
                    ,C.CUSNAME AS CUSTOMER_NAME
                    ,B.BRCCODE AS PLANT
                    ,B.BRCNAME AS PLANT_NAME
                    , T3.TP3NAME AS CHANNEL
		            ,DT.DOCCODE AS DOCTYPE
                    ,DT.DOCNAME AS [TYPE]
                    ,CASE D.DMDPROCESS
                    WHEN '0' THEN
                    'PENDIENTE'
                    WHEN '1' THEN
                    'SAP'
                    END AS [STATUS]
                    ,(
	                    SELECT
			                CASE  
				                WHEN SUM(DP.kilogramos) IS NULL THEN '0'
				                ELSE
				                SUM(DP.kilogramos) 
				                END	AS PESO
		                FROM (
			                SELECT
				                (SELECT                                  
				                (prumultiplyby * dpt.dptquantity)
					                FROM productunit
					                 WHERE procode =  dpt.procode and untcode = dpt.untcode
			                ) as kilogramos	
				                FROM demandproduct dpt 
					                WHERE dpt.dmdcode = D.DMDCODE
		                ) AS DP		
		                ) AS KG
		            ,FORMAT(D.DMDDATE,'dd-MM-yyyy') AS CREATION_DATE
                    FROM DBO.DEMAND D
                    INNER JOIN DEMANDPRODUCT DPR ON D.DMDCODE = DPR.dmdCode
                    INNER JOIN [ROUTE] R ON D.rotCode = R.rotCode AND R._DELETED<> 1
                    INNER JOIN CUSTOMER C ON D.cusCode = C.cusCode
                    INNER JOIN BRANCH B ON R.TRYCODE = B.BRCCODE
                    INNER JOIN TYPE3 T3 ON C.TP3CODE = T3.TP3CODE
                    INNER JOIN DOCUMENT DT ON D.DOCCODE = DT.DOCCODE AND DOCDEMAND = 1
                    WHERE CAST(DMDDATE AS DATE) BETWEEN '{Date1}' AND '{Date2}' AND R.rotCode = '{Route}' AND D.DMDCANCELORDER <> 1";
            }

           DataTable table = db.ReadSql(query);

            if (table.Rows.Count > 0)
            {
                return table.ToList<SALES_TRADITIONAL>();
            }

            return null;
            
        }

        public static async  Task<List<SALES_TRADITIONAL>> SalesByRouteAsync(string Route, string Date1, string Date2 = "")
        {
            string query = string.Empty;
            DBContextMSSQL db = new DBContextMSSQL();

            if (Route != string.Empty && Date1 != string.Empty && Date2 == string.Empty)
            {
                query = $@"SELECT DISTINCT
                    D.DMDCODE AS[ORDER]
                    ,R.rotcode as [ROUTE]
                    ,D.CUSCODE AS CUSTOMER
                    ,C.CUSNAME AS CUSTOMER_NAME
                    ,B.BRCCODE AS PLANT
                    ,B.BRCNAME AS PLANT_NAME
                    , T3.TP3NAME AS CHANNEL
		            ,DT.DOCCODE AS DOCTYPE
                    ,DT.DOCNAME AS [TYPE]
                    ,CASE D.DMDPROCESS
                    WHEN '0' THEN
                    'PENDIENTE'
                    WHEN '1' THEN
                    'SAP'
                    END AS [STATUS]
                    ,(
	                    SELECT
			                CASE  
				                WHEN SUM(DP.kilogramos) IS NULL THEN '0'
				                ELSE
				                SUM(DP.kilogramos) 
				                END	AS PESO
		                FROM (
			                SELECT
				                (SELECT                                  
				                (prumultiplyby * dpt.dptquantity)
					                FROM productunit
					                 WHERE procode =  dpt.procode and untcode = dpt.untcode
			                ) as kilogramos	
				                FROM demandproduct dpt 
					                WHERE dpt.dmdcode = D.DMDCODE
		                ) AS DP		
		                ) AS KG
		            ,FORMAT(D.DMDDATE,'dd-MM-yyyy') AS CREATION_DATE
                    FROM DBO.DEMAND D
                    INNER JOIN DEMANDPRODUCT DPR ON D.DMDCODE = DPR.dmdCode
                    INNER JOIN [ROUTE] R ON D.rotCode = R.rotCode AND R._DELETED<> 1
                    INNER JOIN CUSTOMER C ON D.cusCode = C.cusCode
                    INNER JOIN BRANCH B ON R.TRYCODE = B.BRCCODE
                    INNER JOIN TYPE3 T3 ON C.TP3CODE = T3.TP3CODE
                    INNER JOIN DOCUMENT DT ON D.DOCCODE = DT.DOCCODE AND DOCDEMAND = 1
                    WHERE CAST(DMDDATE AS DATE) = '{Date1}' AND R.rotCode = '{Route}' AND D.DMDCANCELORDER <> 1";
            }
            else if (Route != string.Empty && Date1 != string.Empty && Date2 != string.Empty)
            {
                query = $@"SELECT DISTINCT
                    D.DMDCODE AS[ORDER]
                    ,R.rotcode as [ROUTE]
                    ,D.CUSCODE AS CUSTOMER
                    ,C.CUSNAME AS CUSTOMER_NAME
                    ,B.BRCCODE AS PLANT
                    ,B.BRCNAME AS PLANT_NAME
                    , T3.TP3NAME AS CHANNEL
		            ,DT.DOCCODE AS DOCTYPE
                    ,DT.DOCNAME AS [TYPE]
                    ,CASE D.DMDPROCESS
                    WHEN '0' THEN
                    'PENDIENTE'
                    WHEN '1' THEN
                    'SAP'
                    END AS [STATUS]
                    ,(
	                    SELECT
			                CASE  
				                WHEN SUM(DP.kilogramos) IS NULL THEN '0'
				                ELSE
				                SUM(DP.kilogramos) 
				                END	AS PESO
		                FROM (
			                SELECT
				                (SELECT                                  
				                (prumultiplyby * dpt.dptquantity)
					                FROM productunit
					                 WHERE procode =  dpt.procode and untcode = dpt.untcode
			                ) as kilogramos	
				                FROM demandproduct dpt 
					                WHERE dpt.dmdcode = D.DMDCODE
		                ) AS DP		
		                ) AS KG
		            ,FORMAT(D.DMDDATE,'dd-MM-yyyy') AS CREATION_DATE
                    FROM DBO.DEMAND D
                    INNER JOIN DEMANDPRODUCT DPR ON D.DMDCODE = DPR.dmdCode
                    INNER JOIN [ROUTE] R ON D.rotCode = R.rotCode AND R._DELETED<> 1
                    INNER JOIN CUSTOMER C ON D.cusCode = C.cusCode
                    INNER JOIN BRANCH B ON R.TRYCODE = B.BRCCODE
                    INNER JOIN TYPE3 T3 ON C.TP3CODE = T3.TP3CODE
                    INNER JOIN DOCUMENT DT ON D.DOCCODE = DT.DOCCODE AND DOCDEMAND = 1
                    WHERE CAST(DMDDATE AS DATE) BETWEEN '{Date1}' AND '{Date2}' AND R.rotCode = '{Route}' AND D.DMDCANCELORDER <> 1";
            }

            DataTable table = await db.ReadSqlAsync(query);

            if (table.Rows.Count > 0)
            {
                return table.ToList<SALES_TRADITIONAL>();
            }

            return null;

        }





    }
}