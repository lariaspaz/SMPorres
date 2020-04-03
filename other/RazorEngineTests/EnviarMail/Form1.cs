using RazorEngine;
using RazorEngine.Templating; // For extension methods.
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace EnviarMail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnviarMailConZimbraBSE("This is my subject", "This is the content", false);
        }

        private static void EnviarMailConZimbraBSE(string subject, string body, bool isBodyHtml)
        {
            //descomentar la siguiente sentencia si el certificado del servidor no es válido
            ServicePointManager.ServerCertificateValidationCallback =
                            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                            { return true; };

            using (MailMessage mail = new MailMessage(ConfigurationManager.AppSettings["from"],
                ConfigurationManager.AppSettings["to"]))
            using (SmtpClient client = new SmtpClient())
            {
                client.Host = ConfigurationManager.AppSettings["host"];
                //client.Port = 25;
                client.Port = 587;

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

            //using (MailMessage mail = new MailMessage("backupdb@smp.com.ar", "leoariaspaz@gmail.com"))
            //using (SmtpClient client = new SmtpClient())
            //{
            //    //<network host = "smtpserver1" port = "25" userName = "username" password = "secret" defaultCredentials = "true" />
            //    client.Host = "smtp.gmail.com";
            //    client.Port = 587;
            //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    client.UseDefaultCredentials = false;
            //    client.Credentials = new NetworkCredential("leoariaspaz@gmail.com", "mqxtcunruclxeszu");
            //    client.EnableSsl = true;
            //    mail.Subject = subject;
            //    mail.Body = body;
            //    mail.IsBodyHtml = isBodyHtml;
            //    client.Send(mail);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string template = "Hello @Model.Name, welcome to RazorEngine!";
            var result =
                Engine.Razor.RunCompile(template, "templateKey", null, new { Name = "World" });

            MessageBox.Show(result);
        }

        public class Deuda
        {
            public int Id { get; set; }

            public string Nombre { get; set; }

            public string Fecha { get; set; }

            public int CantidadCuotas { get; set; }

            public string Carrera { get; set; }

            public string Importe { get; set; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var template = File.ReadAllText(Application.StartupPath + @"\Plantillas\default.cshtml");
            var datos = new Deuda[] {                  
                new Deuda { Id = 1, Nombre = "Juan", Fecha = "3 de marzo de 2.020", CantidadCuotas = 2,
                    Carrera = "Técnico Superior en Hemoterapia", Importe = "8.502,00" },
                new Deuda { Id = 2, Nombre = "Pedro", Fecha = "6 de marzo de 2.020", CantidadCuotas = 5,
                    Carrera = "Radiología", Importe = "1.456,00" },
                new Deuda { Id = 3, Nombre = "María", Fecha = "3 de marzo de 2.020", CantidadCuotas = 1,
                    Carrera = "Técnico Superior en Hemoterapia", Importe = "4.123,15" },
            };
            foreach (var item in datos)
            {
                var result = Engine.Razor.RunCompile(template, "deuda" + item.Id + DateTime.Now, typeof(Deuda), item, null);
                //MessageBox.Show(result);
                EnviarMailConZimbraBSE("Notificación de deuda - " + DateTime.Now, result, true);
                MessageBox.Show("Mail enviado");
                return;
            }
        }
    }
}
