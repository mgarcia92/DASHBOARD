using B2B_DASHBOARD.ServiceSap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace B2B_DASHBOARD.SAP
{
    public class ServicesSap
    {
        private string user { get; set; }
        private string password { get; set; }
        private ZSD_SERVI_METHODResponse Response { get; set; }
        private ZSD_DASHBOARD_METHOD01 Client { get; set; }

        public ServicesSap()
        {
            this.user = ConfigurationManager.AppSettings["userSap"].ToString();
            this.password = ConfigurationManager.AppSettings["PasswordSap"].ToString();
            this.CreateClient();
        }

        private void CreateClient()
        {
            this.Client = new ZSD_DASHBOARD_METHOD01();
            this.Client.Credentials = new NetworkCredential(this.user, this.password);
        }
#region
        public ZSD_SERVI_METHODResponse ServiceMethod01Sap(string request,string customer = "",string order = "",
                                       string route = "",string system = "",string var002 = "",
                                       string var003 = "", string var004 = "", string var005 ="", 
                                       RSIS_S_RANGE vkorg = null,RSIS_S_RANGE date = null, 
                                       RSIS_S_RANGE werks = null, RSIS_S_RANGE var001 = null)
        {

         try
            {
                RSIS_S_RANGE[] org = { vkorg }; //new RSIS_S_RANGE() { SIGN = "I", OPTION = "EQ", LOW = "0002", HIGH = string.Empty } };
                RSIS_S_RANGE[] fecha = { date };
                RSIS_S_RANGE[] centro = { werks };
                RSIS_S_RANGE[] var1 = { var001 };
               ZSD_POM_DASHBOARD[] pom = {new ZSD_POM_DASHBOARD() { PVRTNR = "", VKORG = "",
                                                                    VRSIO = "", ZPESO_ENTR = 0,
                                                                    ZPESO_PEDI = 0, ZPLAN_SOP = 0 } };
                ZSD_DOCUMENTS_DASHBOARD[] document = 
                    { new ZSD_DOCUMENTS_DASHBOARD()
                    {
                        CUSTOMER = string.Empty, ID_DOC = string.Empty,
                        PURCHASE_ORDER = string.Empty, POSNR = string.Empty,
                        DESCRIPCION = string.Empty, DOC_TYPE = string.Empty,
                        DATE = string.Empty, ESTADO_ENTREGA = string.Empty,
                        ESTADO_FACTURA = string.Empty, MATERIAL= string.Empty,
                        MEINS = string.Empty, NET_AMOUNT = 0,
                        CANTIDAD = 0,
                        FACTURA = string.Empty
                    }
                     };

                ZSD_SERVI_METHOD peticion = new ZSD_SERVI_METHOD()
                {
                    REQUEST = request,
                    CUSTOMER = customer,
                    ORGV = org,
                    ORDER = order,
                    ROUTE = route,
                    SYSTEM = system,
                    DATE = fecha,
                    WERKS = centro,
                    VAR001 = var1,
                    VAR002 = var002,
                    VAR003 = var003,
                    VAR004 =var004,
                    VAR005 = var005,
                    DATOS_DOCUMENTOS = document,
                    DATOS_POM = pom
                };
             //response
             return   Response = this.Client.ZSD_SERVI_METHOD(peticion);
            }
            catch (Exception ex)
            {
               return Response = new ZSD_SERVI_METHODResponse() { ERROR_MESSAGE = ex.Message };
            }          
        }
#endregion
#region
        public async Task<ZSD_SERVI_METHODResponse> ServiceMethod01SapAsync(string request, string customer = "", string order = "",
                                       string route = "", string system = "", string var002 = "",
                                       string var003 = "", string var004 = "", string var005 = "",
                                       RSIS_S_RANGE vkorg = null, RSIS_S_RANGE date = null,
                                       RSIS_S_RANGE werks = null, RSIS_S_RANGE var001 = null)
        {

            try
            {
                RSIS_S_RANGE[] org = { vkorg }; //new RSIS_S_RANGE() { SIGN = "I", OPTION = "EQ", LOW = "0002", HIGH = string.Empty } };
                RSIS_S_RANGE[] fecha = { date };
                RSIS_S_RANGE[] centro = { werks };
                RSIS_S_RANGE[] var1 = { var001 };
                ZSD_POM_DASHBOARD[] pom = {new ZSD_POM_DASHBOARD() { PVRTNR = "", VKORG = "",
                                                                    VRSIO = "", ZPESO_ENTR = 0,
                                                                    ZPESO_PEDI = 0, ZPLAN_SOP = 0 } };
                ZSD_DOCUMENTS_DASHBOARD[] document =
                    { new ZSD_DOCUMENTS_DASHBOARD()
                    {
                        CUSTOMER = string.Empty, ID_DOC = string.Empty,
                        PURCHASE_ORDER = string.Empty, POSNR = string.Empty,
                        DESCRIPCION = string.Empty, DOC_TYPE = string.Empty,
                        DATE = string.Empty, ESTADO_ENTREGA = string.Empty,
                        ESTADO_FACTURA = string.Empty, MATERIAL= string.Empty,
                        MEINS = string.Empty, NET_AMOUNT = 0,CANTIDAD = 0,FACTURA= string.Empty}
                     };

                ZSD_SERVI_METHOD peticion = new ZSD_SERVI_METHOD()
                {
                    REQUEST = request,
                    CUSTOMER = customer,
                    ORGV = org,
                    ORDER = order,
                    ROUTE = route,
                    SYSTEM = system,
                    DATE = fecha,
                    WERKS = centro,
                    VAR001 = var1,
                    VAR002 = var002,
                    VAR003 = var003,
                    VAR004 = var004,
                    VAR005 = var005,
                    DATOS_DOCUMENTOS = document,
                    DATOS_POM = pom
                };

                return await Task.Run(() => {
                       return Client.ZSD_SERVI_METHOD(peticion);
                }).ConfigureAwait(false);

                //response
               // return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion metodo asincrono metodo asincrono

        public RSIS_S_RANGE CreateRange(string sign, string option,string low, string high)
        {
            RSIS_S_RANGE range = new RSIS_S_RANGE()
            {
                SIGN = sign,
                OPTION = option,
                LOW = low,
                HIGH = high
            };
            return range;
        }
    }
}