using Limilabs.Client.SMTP;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace TapLearn.Core.Sender
{
    public class SendEmail
    {
        public static bool UseDefaultCredentials { get; private set; }

        public static void Send(string To,string Subject,string Body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("zeynab.rostami76@gmail.com","تاپ لرن");
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            SmtpServer.Port =587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("zeynab.rostami76@gmail.com", "1376rostamy");
            SmtpServer.EnableSsl = true;

        

            SmtpServer.Send(mail);

        }
    }
}