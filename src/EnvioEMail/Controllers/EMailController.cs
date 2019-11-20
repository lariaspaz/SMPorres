using EnvioEMail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Reflection;

namespace EnvioEMail.Controllers
{
    class EMailController
    {
        public static string ArmarBodyEMailHtmlImage(string apellido, string nombre, string documento, string carrera, string curso, int cuotasAdeudadas, decimal? importeDeuda)
        {
            StringBuilder body = new StringBuilder();

            body.AppendFormat("<img src='cid:imagen' width='500' >");
            
            body.AppendFormat("<br/>Buenos días <h4>{0} {1}, </h4>", nombre, apellido);

            body.AppendFormat("<p>");
            body.AppendFormat("Al día {0} ", DateTime.Today.ToLongDateString());
            body.AppendFormat("detectamos en nuestros sistemas que usted registra {0} cuotas impagas en su carrera {1} ", cuotasAdeudadas, carrera);
            body.AppendFormat("por un importe de ${0:n}. ", importeDeuda);
            body.AppendFormat("</p>");

            body.AppendFormat("<p>");
            body.AppendFormat("Recuerde que tiene la posibilidad de descargar los comprobantes de pago desde <a href='http://190.105.227.212/consultas/Account/Login'>nuestra web</a> y luego abonarlos en cualquier ");
            body.AppendFormat("sucursal del Banco Santiago del Estero o Sol Pago");
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
                if (string.IsNullOrEmpty(eMailDetail.From))
                {
                    AlternateView htmlView =
                    AlternateView.CreateAlternateViewFromString(eMailDetail.Body, Encoding.UTF8, MediaTypeNames.Text.Html);
                    var dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    LinkedResource img = new LinkedResource($@"{dir}\images\Header.png", MediaTypeNames.Image.Jpeg);
                    img.ContentId = "imagen";

                    // Lo incrustamos en la vista HTML...

                    htmlView.LinkedResources.Add(img);

                    MailMessage mail = new MailMessage()
                    {
                        From = new MailAddress("tesoreria@ismp.edu.ar", "Instituto San Martín de Porres"),
                        //Body = eMailDetail.Body,
                        Subject = eMailDetail.Subject,
                        IsBodyHtml = true
                    };

                    mail.AlternateViews.Add(htmlView);
                    mail.To.Add("cpn.alanizclaudioalejandro@gmail.com");
                    mail.CC.Add("hernan_jhc@hotmail.com");  // --> podríamos usar una cuenta nuestra la del Backup

                    SmtpClient smtp = new SmtpClient()
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("tesoreria@ismp.edu.ar", "QW1951er"),
                        //Credentials = new NetworkCredential("hernan.jhc@gmail.com", "kkdjkukmwckyufwk"),
                        EnableSsl = true
                    };
                    //if (EMailVálido(eMailDetail.From))
                    //{
                      smtp.Send(mail);
                    //}
                }
            }
            catch (Exception)
            {
                //Grabar log o guardar eMail no enviado
                throw;
            }

        }

        private static bool EMailVálido(string from)
        {
            bool válido = false;
            string pattern = null;
            pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if(!string.IsNullOrEmpty(from))
            {
                if (Regex.IsMatch(from, pattern))
                {
                    válido = true;
                }
            }
            
            return válido;
        }
    }
}
