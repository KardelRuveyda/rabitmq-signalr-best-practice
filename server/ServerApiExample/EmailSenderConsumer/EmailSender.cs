using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderConsumer
{
    static class EmailSender
    {
        public static void Send(string to, string email)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "stmp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            NetworkCredential credential = new NetworkCredential("testkardel@gmail.com", "7585q@rdelR");
            smtpClient.Credentials = credential;

            MailAddress sender = new MailAddress("testkardel@gmail.com", "Test Kardel");
            MailAddress receiver = new MailAddress(to);

            MailMessage mail = new MailMessage(sender,receiver);

            mail.Subject = "Test";
            mail.Body = "Test";
            
            smtpClient.Send(mail);


        }
    }
}
