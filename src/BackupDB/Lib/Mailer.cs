using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BackupDB.Lib
{
    class Mailer
    {
        public void SendMail(string asunto, string cuerpo)
        {
            using (MailMessage mail = new MailMessage("backupdb@smp.com.ar", "leoariaspaz@gmail.com"))
            using (SmtpClient client = new SmtpClient())
            {
                //<network host = "smtpserver1" port = "25" userName = "username" password = "secret" defaultCredentials = "true" />
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("leoariaspaz@gmail.com", "mqxtcunruclxeszu");
                client.EnableSsl = true;
                mail.Subject = asunto;
                mail.Body = cuerpo;
                client.Send(mail);
            }
        }
    }
}
