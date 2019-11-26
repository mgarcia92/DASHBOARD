using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog.Config;
using NLog.Targets;
using NLog.Layouts;

namespace NLog.B2B_DASHBOARD
{
    public static class ExtensionMethods{}

    public class programaNLog2
    {
        /*
        protected Logger Log { get; private set; }

        protected ApplicationBase(Type declaringType) {
            Log = LogManager.GetLogger(declaringType.FullName);
        }*/

       /*GIThUB*/
        private static readonly ILogger elLog = LogManager.GetCurrentClassLogger();
        public void Main(string[]args) {
            this.iniciaTransaccion();
            this.hazAlgo();
            this.pararTransaccion();
        }

        public void iniciaTransaccion() {
            elLog.Info("transaccion iniciada...");
            elLog.Debug("init main");
        }
        public void hazAlgo() { elLog.Debug("debugueando"); }
        public void pararTransaccion() { elLog.Error("transaccion detenida..."); }

    }

    
}