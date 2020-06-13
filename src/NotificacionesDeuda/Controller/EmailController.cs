using NotificacionesDeuda.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesDeuda.Controller
{
    class EmailController
    {
        public static void EnviarMail(string subject, string body, bool isBodyHtml, string to)
        {
            //verifica si el certificado del servidor es válido
            if (Configuration.ValidateServerX509Certificate)
            {
                ServicePointManager.ServerCertificateValidationCallback =
                                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                                { return true; };
            }

            using (MailMessage mail = new MailMessage())
            using (SmtpClient client = new SmtpClient())
            {
                client.Host = Configuration.Host;
                client.Port = Configuration.Port;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Configuration.User, Configuration.Password);
                client.EnableSsl = Configuration.EnableSsl;
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = isBodyHtml;
                mail.From = new MailAddress(Configuration.From, Configuration.DisplayName);
                mail.To.Add(to);
                client.Send(mail);
            }
        }

    }
}
