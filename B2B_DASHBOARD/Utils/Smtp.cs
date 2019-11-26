using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Configuration;
using System.Net;
using System.IO;
using System.Net.Mime;

namespace B2B_DASHBOARD.Utils
{
    public class Smtp
    {
        private SmtpClient client { get; set; }
        private string server { get; set; }
        private int port { get; set; }
        private string account { get; set; }
        private string password { get; set; }

        public Smtp()
        {
             server = ConfigurationManager.AppSettings["smtpServer"].ToString();
             port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"].ToString());
             account = ConfigurationManager.AppSettings["smtpAccount"].ToString();
             password = ConfigurationManager.AppSettings["smtpPassword"].ToString();           
        }

        private SmtpClient ConnectServer()
        {
            client = new SmtpClient(server, port);
            return client;
        }

        public string SendMessage(string To, string Subject, string file)
        {
            SmtpClient client = ConnectServer();
            
                try
                {
                    client.EnableSsl = true;
                    string From = this.account;
                    string Body = "Correo enviado de Forma automatica por el sistema por favor no responder.";
                    client.Credentials = new NetworkCredential(account,password);
                    MailMessage message = new MailMessage(From,To,Subject,Body);
                    //FileStream pdf = new FileStream(file,FileMode.Open);
                    message.Attachments.Add(new Attachment(file, MediaTypeNames.Application.Pdf));
                    client.Send(message);
                    return "Correo Enviado con Exito!!.";
                }
                catch (Exception Ex)
                {
                    return Ex.Message;
                }
                finally
                {
                    client.Dispose();
                }

            
        }

    }
}