using System;
using System.Collections.Generic;
using System.Configuration;
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
        //public static void EnviarEMailImage(EMail eMailDetail)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(eMailDetail.From))
        //        {
        //            AlternateView htmlView =
        //            AlternateView.CreateAlternateViewFromString(eMailDetail.Body, Encoding.UTF8, MediaTypeNames.Text.Html);
        //            var dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        //            LinkedResource img = new LinkedResource($@"{dir}\images\Header.png", MediaTypeNames.Image.Jpeg);
        //            img.ContentId = "imagen";
        //
        //            // Lo incrustamos en la vista HTML...
        //
        //            htmlView.LinkedResources.Add(img);
        //
        //            MailMessage mail = new MailMessage()
        //            {
        //                From = new MailAddress("tesoreria@ismp.edu.ar", "Instituto San Martín de Porres"),
        //                //Body = eMailDetail.Body,
        //                Subject = eMailDetail.Subject,
        //                IsBodyHtml = true
        //            };
        //
        //            mail.AlternateViews.Add(htmlView);
        //            mail.To.Add("cpn.alanizclaudioalejandro@gmail.com");
        //            mail.CC.Add("hernan_jhc@hotmail.com");  // --> podríamos usar una cuenta nuestra la del Backup
        //
        //            SmtpClient smtp = new SmtpClient()
        //            {
        //                Host = "smtp.gmail.com",
        //                Port = 587,
        //                UseDefaultCredentials = false,
        //                Credentials = new NetworkCredential("tesoreria@ismp.edu.ar", "skcbhjxwgockqnfo"),
        //                //Credentials = new NetworkCredential("hernan.jhc@gmail.com", "kkdjkukmwckyufwk"),
        //                EnableSsl = true
        //            };
        //            //if (EMailVálido(eMailDetail.From))
        //            //{
        //            smtp.Send(mail);
        //            //}
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //Grabar log o guardar eMail no enviado
        //        throw;
        //    }
        //
        //}

        public static void EnviarMail(string subject, string body, bool isBodyHtml, string to)
        {
            //descomentar la siguiente sentencia si el certificado del servidor no es válido
            ServicePointManager.ServerCertificateValidationCallback =
                            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                            { return true; };

            using (MailMessage mail = new MailMessage(ConfigurationManager.AppSettings["from"],
                to)) //ConfigurationManager.AppSettings["to"]))
            using (SmtpClient client = new SmtpClient())
            {
                client.Host = ConfigurationManager.AppSettings["host"];
                client.Port = 587;  // 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["user"],
                    ConfigurationManager.AppSettings["password"]);
                client.EnableSsl = true;
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = isBodyHtml;
                client.Send(mail);
            }
        }

    }
}
