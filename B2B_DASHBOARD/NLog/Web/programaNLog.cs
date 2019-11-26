using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog.Config;
using NLog.Targets;
using NLog.Layouts;

namespace NLog.programaNLog
{
    public static class ExtensionMethods
    {

    }
    public class programaNLog
    {
        private static readonly ILogger elLog = LogManager.GetCurrentClassLogger();

        public static void LogProc(string msg)
        {
            Console.WriteLine("logproc: {0}", msg);
        }

        static void A()
        {
            B(3);
        }

        static void B(int a)
        {
            elLog.Trace("ttt");
            elLog.Debug("ala ma kota");
            elLog.Info("ala ma kanarka");
            elLog.Warn("aaa");
            elLog.Error("err");
            elLog.Fatal("fff");
        }

        static void Main(string[]args)
        {
            Target t;

            // t.WriteLogEvent()
            //ConsoleTarget ct = new ConsoleTarget();
            //ct.Layout = "${message} ${longdate} ${replace:searchFor=(..):regex=true:wholeWords=true:replaceWith=[xx'$1'yy]:inner=${rot13:inner=${message}:uppercase=true:padding=-10}}";

            //CsvLayout csv = new CsvLayout();
            //csv.Columns.Add(new CsvColumn("msg", "${message}"));
            //csv.Columns.Add(new CsvColumn("date", "${longdate}"));
            //csv.Columns.Add(new CsvColumn("level", "${level}"));
            //csv.WithHeader = true;
            ////ct.Layout = csv;

            //SimpleConfigurator.ConfigureForTargetLogging(ct);

            //InternalLogger.LogToConsole = true;
            //InternalLogger.LogLevel = LogLevel.Debug;

            elLog.Debug(() => "foo bar");

            for (int i = 0; i < 3; ++i)
            {
                elLog.Trace("ttt");
                elLog.Debug("ala ma kota {0}", i);
                elLog.Info("ala ma kanarka");
                elLog.Warn("aaa");
                elLog.Error("err");
                elLog.Fatal("fff");
            }
        }
    }
}