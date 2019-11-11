using SMPorres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SMPorres.Repositories
{
    class EMailRepository
    {
        public static string ArmarBodyEMail(string apellido, string nombre, string documento, string carrera, string curso, int cuotasAdeudadas, decimal? importeDeuda)
        {
            string body = "";
            body = "Estimado/a " + nombre + " " + apellido + ", DNI " + documento + "." + Environment.NewLine;
            body += "Le recordamos que al día de hoy " + DateTime.Today.ToString("dd/MM/yyyy") + " ud registra deuda con nuestra institución." + Environment.NewLine;
            body += "Correspondiente a " + cuotasAdeudadas.ToString() + " cuotas impagas registradas en nuestro sistema, por un saldo de $" + importeDeuda.ToString() +
                Environment.NewLine;
            body += Environment.NewLine;
            body += "Si ud ya regularizó ésta situación tenga en cuenta descartar el mensaje recibido." + Environment.NewLine;
            body += Environment.NewLine;
            body += "Atte." + Environment.NewLine;
            body += "Instituto San Martín de Porres";
            return body;
        }

        public static void EnviarEMail(EMail eMailDetail)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    // From = new MailAddress(eMailDetail.From),  
                    From = new MailAddress("hernan.jhc@gmail.com"),
                    Body = eMailDetail.Body,
                    Subject = eMailDetail.Subject,
                    IsBodyHtml = false
                };

                mail.CC.Add("hernan_jhc@hotmail.com");  // --> podríamos usar una cuenta nuestra la del Backup

                SmtpClient smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    UseDefaultCredentials = false,
                    //Credentials = new NetworkCredential("tesoreria@ismp.edu.ar", "QW1951er"),
                    Credentials = new NetworkCredential("hernan.jhc@gmail.com", "..."),
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
