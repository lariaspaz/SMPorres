﻿using EnvioEMail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace EnvioEMail.Repositories
{
    class EMailRepository
    {
        public static string ArmarBodyEMailHtmlImage(string apellido, string nombre, string documento, string carrera, string curso, int cuotasAdeudadas, decimal? importeDeuda)
        {
            StringBuilder body = new StringBuilder();

            body.AppendFormat("<img src='cid:imagen' />");
            
            body.AppendFormat("<br/>Buenos días <h4>{0} {1}, </h4>", nombre, apellido);

            body.AppendFormat("<p>");
            body.AppendFormat("Al día {0} ", DateTime.Today.ToLongDateString()); //.ToString());    // ("dd/MM/yyyy"));
            body.AppendFormat("detectamos en nuestros sistemas que usted registra {0} cuotas impagas en su carrera {1} <br/>", cuotasAdeudadas, carrera);
            body.AppendFormat("por un importe de ${0:n}. ", importeDeuda);
            body.AppendFormat("</p>");

            body.AppendFormat("<p>");
            body.AppendFormat("Recuerde que tiene la posibilidad de descargar los comprobantes de pago desde nuestra web y luego abonarlos en cualquier ");
            body.AppendFormat("sucursal del <br/>Banco Santiago del Estero o Sol Pago");
            body.AppendFormat("</p>");

            body.AppendFormat("<p>");
            body.AppendFormat("Si usted ya regularizó su situación tenga a bien descartar ésta notificación.");
            body.AppendFormat("</p>");

            body.AppendFormat("<p>");
            body.AppendFormat(" <h3> Cordialmente, <br/>Instituto San Martín de Porres </h3>");
            body.AppendFormat("</p>");

            return body.ToString();
        }

        public static void EnviarEMailImage(EMail eMailDetail)
        {
            try
            {
                AlternateView htmlView =
                AlternateView.CreateAlternateViewFromString(eMailDetail.Body, Encoding.UTF8, MediaTypeNames.Text.Html);

                LinkedResource img = new LinkedResource(@"C:\Temp\ismp.png", MediaTypeNames.Image.Jpeg);
                img.ContentId = "imagen";

                // Lo incrustamos en la vista HTML...

                htmlView.LinkedResources.Add(img);

                MailMessage mail = new MailMessage()
                {
                    // From = new MailAddress(eMailDetail.From),  
                    From = new MailAddress("no-responder@ismp.edu.ar"),
                    //Body = eMailDetail.Body,
                    Subject = eMailDetail.Subject,
                    IsBodyHtml = true
                };

                mail.AlternateViews.Add(htmlView);

                mail.CC.Add("hernan_jhc@hotmail.com");  // --> podríamos usar una cuenta nuestra la del Backup

                SmtpClient smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    UseDefaultCredentials = false,
                    //Credentials = new NetworkCredential("tesoreria@ismp.edu.ar", "QW1951er"),
                    Credentials = new NetworkCredential("hernan.jhc@gmail.com", "dejaselaa..."),
                    EnableSsl = true
                };

                smtp.Send(mail);
            }
            catch (Exception)
            {
                //Grabar log o guardar eMail no enviado
                throw;
            }

        }
    }
}
