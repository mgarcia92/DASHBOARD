using B2B_DASHBOARD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using B2B_DASHBOARD.Models;
using Newtonsoft.Json;
using B2B_DASHBOARD.SAP;
using B2B_DASHBOARD.ServiceSap;
using System.Threading.Tasks;
using System.Threading;

namespace B2B_DASHBOARD.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            List<DayofWeek> days = DayofWeek.GetDaysOfWeek();
            days.OrderByDescending(x => x.DAY_ID);  
            return View(days);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<string> CustomerOutOfRoute(string Route) { 
          Thread.Sleep(1000);
            List<CUSTOMERS_OUT_OF_ROUTE> customers;
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            string json = string.Empty;
            string user = AuthHelpers.GetUserId(cookie);
            customers = await CUSTOMERS_OUT_OF_ROUTE.GetCustomersAsync(user);
            DATAJSON<CUSTOMERS_OUT_OF_ROUTE> data = new DATAJSON<CUSTOMERS_OUT_OF_ROUTE>();
            data.data = customers;
            json = JsonConvert.SerializeObject(data);
            return json;
        }

        [HttpPost]
        public async Task<string> Home_CoordinatesAsync(string Day = null)
        {
            Thread.Sleep(1000); // SE DUERME EL HILO ACTUAL 1 SEG PARA QUE LA PETICION AJAX TENGA TIEMPO DE ACTUALIZAR EL MAPA
            int day = Day == null || Day == string.Empty ? Sales_Coordinates.GetDay() : Convert.ToInt32(Day);
            List<Sales_Coordinates> Sales_By_Coordinate;
            List<CUSTOMERS_OUT_OF_ROUTE> customers;
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            string json = string.Empty;
            string user = AuthHelpers.GetUserId(cookie);

            Sales_By_Coordinate = await Sales_Coordinates.CordenadasAsync(user, day);
            DATAJSON<Sales_Coordinates> data = new DATAJSON<Sales_Coordinates>();
            data.data = Sales_By_Coordinate;

            customers =  CUSTOMERS_OUT_OF_ROUTE.GetCustomers(user);
            DATAJSON<CUSTOMERS_OUT_OF_ROUTE> data2 = new DATAJSON<CUSTOMERS_OUT_OF_ROUTE>();
            data2.data = customers;

            var objeto = new { customer = data.data, oufofroute = data2.data };
            json = JsonConvert.SerializeObject(objeto);
            return json;
        }

        [HttpPost]
        public async Task<string> GetCustomerVisitedAsync(string Day = null)
        {
            try
            {
                HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                string user = AuthHelpers.GetUserId(cookie);
                int day = Day == null || Day == string.Empty ? Sales_Coordinates.GetDay() :Convert.ToInt32(Day);
                string date = Day == null || Day == string.Empty ? DateTime.Now.ToString("yyyy-MM-dd"): GetDateFromDayOfWeek(Convert.ToInt32(Day), (int)DateTime.Now.DayOfWeek);
                string date2 = Convert.ToDateTime(date).AddDays(1).ToString("yyyy-MM-dd");
                List<CustomerVisited> visits = await CustomerVisited.GetCustomerVisitedAsync(date, date2, day, user);
                List<Sales_Coordinates> customers = Sales_Coordinates.Cordenadas(user, day);
                object[] objeto = { new { name = "Visitados", data = new int?[1] { visits.Count }  },
                                    new { name = "Total", data = new int?[1] { customers.Count } } };
                string json = JsonConvert.SerializeObject(objeto);
                return json;
            }
            catch (Exception)
            {
                object[] objeto = { new { name = "Visitados", data = new int?[1] { 0 }  },
                                    new { name = "Total", data = new int?[1] { 0 } } };
                string json = JsonConvert.SerializeObject(objeto);
                return json;
            }
        }

        [HttpPost]
        public async Task<string> GetPomByAdvisorAsync()
        {
            ZSD_SERVI_METHODResponse R;
            string json = string.Empty;
            int intentos = 0;
            do
            {
                try
                {
                    HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                    string user = AuthHelpers.GetUserId(cookie);
                    int day = Sales_Coordinates.GetDay();
                    string orgv = AuthHelpers.GetOrgv(cookie);
                    string date = DateTime.Now.ToString("dd-MM-yyyy");
                    string todayDate = DateTime.Now.ToString("yyyyMMdd");
                    ServicesSap service = new ServicesSap();
                    RSIS_S_RANGE ORGV = service.CreateRange("I", "EQ", orgv, String.Empty);
                    //se debe colocar la fecha automatica
                    RSIS_S_RANGE RANGE_DATE = service.CreateRange("I", "BT", "20171201", "20171231");
                    //fecha para calcular el pom por cliente
                    RSIS_S_RANGE RANGE_DATE2 = service.CreateRange("I", "EQ", "20171214", string.Empty);
                    //respuesta al servicio para calcular el pom mensual.
                    R = await service.ServiceMethod01SapAsync("L2", route: user, vkorg: ORGV, date: RANGE_DATE);
                    object[] pomTotal = { new { name = "Cuota Total", colorByPoint = true, data = new object[] { new { name = "Cuota Mensual", y = (double)R.DATOS_POM[1].ZPLAN_SOP }, new { name = "Facturado", y = (double)R.DATOS_POM[1].ZPESO_ENTR } } } };
                    object[] pomAcumulado = { new { name = "Acumulado Mes", data = new object[] { new { name = "Pedido", y = (double)R.DATOS_POM[1].ZPESO_PEDI },
                                          new { name = "Facturado", y = (double)R.DATOS_POM[1].ZPESO_ENTR } } } };
                    json = JsonConvert.SerializeObject(new { pomTotal = pomTotal, pomAcumulado = pomAcumulado });
                    intentos = 4;  // salir del ciclo.                
                }
                catch (Exception ex)
                {
                    intentos++;
                    if (intentos > 3)
                    {
                        string response = JsonConvert.SerializeObject(new { error = ex.Message });
                        return response;
                    }
                }
            } while (intentos <= 3);
            return json;
        }
        [HttpPost]
        public async Task<string> GetTopOrdersAsync()
        {
            string json;
            try
            {
                List<SALES_TRADITIONAL> List = new List<SALES_TRADITIONAL>();
                string EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                string StartDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                string user = AuthHelpers.GetUserId(cookie);
                List = await SALES_TRADITIONAL.SalesByRouteAsync(user, StartDate, EndDate);
                if (List != null)
                {
                    List = (from l in List
                             orderby l.KG descending
                             select l).Take(10).ToList();
                }
                json = JsonConvert.SerializeObject(List);
               
            }
            catch (Exception e)
            {
                json = JsonConvert.SerializeObject( new { ErrorMessage = e.Message });
            }

            return json;         
        }

        [HttpPost]
        public async Task<string> GetPomCustomerByDay()
        {
            ZSD_SERVI_METHODResponse R2;
            int intentos = 0;
            string json = string.Empty;
            do
            {
                try
                {
                    HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                    string user = AuthHelpers.GetUserId(cookie);
                    string orgv = AuthHelpers.GetOrgv(cookie);
                    // string date = DateTime.Now.ToString("dd-MM-yyyy");
                    string todayDate = DateTime.Now.ToString("yyyyMMdd");
                    ServicesSap service = new ServicesSap();
                    RSIS_S_RANGE ORGV = service.CreateRange("I", "EQ", orgv, String.Empty);
                    //fecha para calcular el pom por cliente
                    RSIS_S_RANGE RANGE_DATE2 = service.CreateRange("I", "EQ", "20171214", string.Empty);
                    //respuesta al servicio para calcular el pom por cliente.
                    R2 = await service.ServiceMethod01SapAsync("L3", route: user, vkorg: ORGV, date: RANGE_DATE2);
                    json = JsonConvert.SerializeObject(new { pomPorCliente = R2.DATOS_POM });
                    intentos = 4;
                }
                catch (Exception ex)
                {
                    intentos++;
                    if (intentos > 3)
                    {
                        string respuesta = JsonConvert.SerializeObject(new { error = ex.Message });
                        return respuesta;
                    }                   
                }
            } while (intentos < 3);
            return json;
        }

        [HttpPost]
        public async Task<string> GetTopOrdersB2BAsync()
        {
            string json;
            try
            {
                List<SALES_B2B> List = new List<SALES_B2B>();
                string EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                string StartDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                string user = AuthHelpers.GetUserId(cookie);
                //colocar fecha automatica
                List = await SALES_B2B.Sales_By_AdvisorAsync(user, "2014-01-01", "2018-01-01");
                if (List != null)
                {
                    List = (from l in List
                                 orderby l.AMOUNT descending
                                 select l).Take(10).ToList();
                    json = JsonConvert.SerializeObject(List);
                }
                else
                {
                    json = JsonConvert.SerializeObject(new { ErrorMessage = "NO SE ENCONTRARON PEDIDOS B2B" });
                }
                
            }
            catch (Exception e)
            {
                json = JsonConvert.SerializeObject(new { ErrorMessage = e.Message });
            }

            return json;
        }

        public string GetDateFromDayOfWeek(int day, int today)
        {
            string date = string.Empty;
            int dia = day == 1 ? day : day - 1;
            int diaActual = today;
            switch (dia)
            {
                case 1:
                    date = DateTime.Now.AddDays((dia - diaActual)).ToString("yyyy-MM-dd");
                    break;
                case 2:
                    date = DateTime.Now.AddDays((dia - diaActual)).ToString("yyyy-MM-dd");
                    break;
                case 3:
                    date = DateTime.Now.AddDays((dia - diaActual)).ToString("yyyy-MM-dd");
                    break;
                case 4:
                    date = DateTime.Now.AddDays((dia - diaActual)).ToString("yyyy-MM-dd");
                    break;
                case 5:
                    date = DateTime.Now.AddDays((dia - diaActual)).ToString("yyyy-MM-dd");
                    break;
                case 6:
                    date = DateTime.Now.AddDays((dia - diaActual)).ToString("yyyy-MM-dd");
                    break;
                case 7:
                    date = DateTime.Now.AddDays((dia - diaActual)).ToString("yyyy-MM-dd");
                    break;
                default:
                    break;
            }
            return date;
        }

    }
}
