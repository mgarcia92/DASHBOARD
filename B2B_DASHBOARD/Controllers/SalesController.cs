using B2B_DASHBOARD.Enums;
using B2B_DASHBOARD.Models;
using B2B_DASHBOARD.SAP;
using B2B_DASHBOARD.ServiceSap;
using B2B_DASHBOARD.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Office.Interop.Excel;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using NLog;/*new18/09/19*/

namespace B2B_DASHBOARD.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Index()
        {
            ViewBag.asesores = GetRoutes();
            return View();
        }

        public List<Route> GetRoutes()
        {
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (AuthHelpers.GetUserRol(cookie) != (short)Enums.Rol.Asesor)
            {
               var asesores = Route.GetList();
               return asesores;
            }
            return null;
        }

        public ActionResult Traditional_Sales()
        {
            ViewBag.asesores = GetRoutes();
            return View();
        }

        public ActionResult Sales_History_B2B()
        {
            return View();
        }

        public ActionResult Traditional_Sales_History()
        {
            return View();
        }
        //VENTAS B2B
        [HttpPost]
        public string Sales_By_Adivisor(string route = "",string date1 = "" , string date2 = "")
        {
            List<SALES_B2B> sales;

            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var session = route != string.Empty ? route : AuthHelpers.GetUserId(cookie);

            if (date1 != string.Empty && date2 == string.Empty)
            {
                sales = SALES_B2B.Sales_By_Advisor(session, date1, null);
            }
            else if (date1 != string.Empty && date2 != string.Empty)
            {
                sales = SALES_B2B.Sales_By_Advisor(session, date1, date2);
            }
            else 
            {
                sales = SALES_B2B.Sales_By_Advisor(session, "2014-08-08", null);
                //sales = SALES_B2B.Sales_By_Advisor(session, null, date2);
            }

            string json = string.Empty;

            if (sales != null)
            {
                DATAJSON<SALES_B2B> data = new DATAJSON<SALES_B2B>();
                data.data = sales;

                json = JsonConvert.SerializeObject(data);

                return json;
            }

            sales = new List<SALES_B2B>();
            json = JsonConvert.SerializeObject(sales);
            return json;

        }

        [HttpPost]
        public string Sales_By_Details_B2B(int? ORDER_ID)
        {
            List<SALES_B2B_DETAIL> Detail_B2B;

            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var user = AuthHelpers.GetUserId(cookie);
            //session.UserData.Trim();

            string json = string.Empty;
            if (ORDER_ID != null)
            {
                Detail_B2B = SALES_B2B_DETAIL.ORDER_DETAILS((int)ORDER_ID);

                if (Detail_B2B  != null)
                {
                    DATAJSON<SALES_B2B_DETAIL> data = new DATAJSON<SALES_B2B_DETAIL>();
                    data.data = Detail_B2B;

                    json = JsonConvert.SerializeObject(data);

                    return json;
                         
                }
               
            }

            Detail_B2B = new List<SALES_B2B_DETAIL>();
            json = JsonConvert.SerializeObject(Detail_B2B);

            return json;
        }

        [HttpPost]
        public string Sales_Traditional_By_Route(string route = "" ,string Date1 = "", string Date2 = "")
        {
            List<SALES_TRADITIONAL> Sales_By_Route;

            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var user = route != string.Empty ? route :AuthHelpers.GetUserId(cookie);
            //session.UserData.Trim();
            string locaDate = DateTime.Now.ToString("yyyy-MM-dd");
            if (Date1 != string.Empty && Date2 == string.Empty)
            {
                Sales_By_Route = SALES_TRADITIONAL.SalesByRoute(user, Date1);
            }
            else if (Date1 != string.Empty && Date2 != string.Empty)
            {
                Sales_By_Route = SALES_TRADITIONAL.SalesByRoute(user, Date1,Date2);
            }
            else
            {              
                Sales_By_Route = SALES_TRADITIONAL.SalesByRoute(user, locaDate);          
            }

            string json = string.Empty;

            if (Sales_By_Route != null)
            {
                DATAJSON<SALES_TRADITIONAL> data = new DATAJSON<SALES_TRADITIONAL>();
                data.data = Sales_By_Route;
                json = JsonConvert.SerializeObject(data);
                return json;
            }

            Sales_By_Route = new List<SALES_TRADITIONAL>();
            json = JsonConvert.SerializeObject(Sales_By_Route);
            return json;

        }

        [HttpPost]
        public string traditional_detail(string ORDER_ID)
        {
            List<SALES_TRADITIONAL_DETAIL> t_detail;

            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var session = AuthHelpers.FormsDescrypt(cookie);
            session.UserData.Trim();

            string json = string.Empty;
            if (ORDER_ID != string.Empty)
            {
                
                t_detail = SALES_TRADITIONAL_DETAIL.TRADITIONAL_DETAIL(ORDER_ID);

                if (t_detail != null)
                {
                    DATAJSON<SALES_TRADITIONAL_DETAIL> data = new DATAJSON<SALES_TRADITIONAL_DETAIL>();
                    data.data = t_detail;
                    json = JsonConvert.SerializeObject(data);
                    return json;
                }

            }

            t_detail = new List<SALES_TRADITIONAL_DETAIL>();
            json = JsonConvert.SerializeObject(t_detail);

            return json;
        }

        [HttpPost]
        public string Order_Detail_Sap(string ORDER_ID,string CUSTOMER)
        {
            string json = string.Empty;
            ZSD_SERVI_METHODResponse response;
            if (string.IsNullOrEmpty(ORDER_ID) && string.IsNullOrEmpty(CUSTOMER))
            {
                 response = new ZSD_SERVI_METHODResponse() { ERROR_MESSAGE = "PARAMETROS VACIOS!!!" };
                json = JsonConvert.SerializeObject(response.ERROR_MESSAGE);
                return json;
            }
            string ORDER = ORDER_ID.Trim();
            string CUST = CUSTOMER.Trim();
            ServicesSap client = new ServicesSap();
             response = client.ServiceMethod01Sap(request:"L1", order:ORDER, customer:CUST);

            if (response.FLAG_01 == "OK")
            {
                json = JsonConvert.SerializeObject(response.DATOS_DOCUMENTOS);
                return json;
            }else if (response.FLAG_01 == "ER")
            {
                json = JsonConvert.SerializeObject(new { ERROR = response.ERROR_MESSAGE, CODE = "1" });
                return json;
            }
            else
            {                
                json = JsonConvert.SerializeObject(new { ERROR = response.ERROR_MESSAGE, CODE = "2" });
                return json;
            }
        }

        [HttpPost]
        public string Order_Detail_Sap_B2B(string ORDER_ID, string CUSTOMER)
        {
            string json = string.Empty;
            ZSD_SERVI_METHODResponse response;
            if (string.IsNullOrEmpty(ORDER_ID) && string.IsNullOrEmpty(CUSTOMER))
            {
                response = new ZSD_SERVI_METHODResponse() { ERROR_MESSAGE = "PARAMETROS VACIOS!!!" };
                json = JsonConvert.SerializeObject(response.ERROR_MESSAGE);
                return json;
            }
            string ORDER = ORDER_ID.Trim();
            string CUST = CUSTOMER.Trim();
            ServicesSap client = new ServicesSap();
            response = client.ServiceMethod01Sap(request: "L1",system:"B2B", order: ORDER, customer: CUST);

            if (response.FLAG_01 == "OK")
            {
                json = JsonConvert.SerializeObject(response.DATOS_DOCUMENTOS);
                return json;
            }
            else if (response.FLAG_01 == "ER")
            {
                json = JsonConvert.SerializeObject(new { ERROR = response.ERROR_MESSAGE, CODE = "1" });
                return json;
            }
            else
            {
                json = JsonConvert.SerializeObject(new { ERROR = response.ERROR_MESSAGE, CODE = "2" });
                return json;
            }
        }

        [HttpPost]
        public JsonResult sendMailTraditional(string JSON, string MAIL)
        {
            Smtp correo = new Smtp();
            string message = correo.SendMessage(MAIL,"Reporte Dashboard", CreateExcel<SALES_TRADITIONAL>(JSON));
            object json = new { msg = message };
            return Json(json);
        }


        [HttpPost]
        public JsonResult sendMailB2B(string JSON, string MAIL)
        {
            Smtp correo = new Smtp();
            string message = correo.SendMessage(MAIL, "Reporte Dashboard", CreateExcel<SALES_B2B>(JSON));
            object json = new { msg = message };
            return Json(json);
        }

        public string CreateExcel<T>(string json)
        {
            List<T> lista = JsonConvert.DeserializeObject<List<T>>(json);
            T obj = lista[0];
            PropertyInfo[] props = obj.GetType().GetProperties().Where(x => x.GetValue(obj) != null).ToArray<PropertyInfo>();
    
           var xlApp = new Application();
            xlApp.Visible = false;
            xlApp.DisplayAlerts = false;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            int countProps = 0;
            foreach (var prop in props)
            {
              countProps++;
              xlWorkSheet.Cells[1, countProps] = prop.Name;
            }

            int countRow = 1;
            int countColumn = 0;
            foreach (var item in lista)
            {
                countRow++;
                foreach (var prop in props)
                {
                    countColumn++;
                    PropertyInfo propInfo = item.GetType().GetProperty(prop.Name);
                    xlWorkSheet.Cells[countRow, countColumn] = propInfo.GetValue(item);
                }
                 countColumn = 0;
            }


            Guid id = Guid.NewGuid();
            string path = Server.MapPath($"~/Archivos/{id}.xlsx");
            xlWorkBook.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
            misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            return path;
        }


        public System.Data.DataTable ConvertToDataTable<T>(IList<T> data)

        {

            PropertyDescriptorCollection properties =

            TypeDescriptor.GetProperties(typeof(T));

            System.Data.DataTable table = new System.Data.DataTable();

            foreach (PropertyDescriptor prop in properties)

                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)

            {

                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)

                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);

            }

            return table;

        }


    }
}