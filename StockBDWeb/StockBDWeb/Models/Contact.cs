using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace StockBDWeb.Models
{
    public class Contact
    {
        public string contName { get; set; }
        public string contEmail { get; set; }
        public string contText { get; set; }

        public Contact()
        {
            contEmail = "";
            contName = "";
            contText = "";
        }
        public bool isValid()
        {
            if (contName == null || contEmail == null || contText == null)
                return false;

            //check the name
            if (contName.Length < 3) return false;

            //check the email
            EmailHelper emailChecker = new EmailHelper();
            if (emailChecker.IsValidEmail(contEmail) == false)
                return false;
            if (contText.Length < 100) return false;

            return true;
        }

        public bool SendEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("localhost");

                string emailSendUser = System.Configuration.ConfigurationManager.AppSettings["emailSendUser"].ToString();
                string emailPassword = System.Configuration.ConfigurationManager.AppSettings["emailPassword"].ToString();
                string emailReceiveUser = System.Configuration.ConfigurationManager.AppSettings["emailReceiveUser"].ToString();
                
                mail.From = new MailAddress(emailSendUser);
                mail.To.Add(emailReceiveUser);
                mail.Subject = "Message from StockBD Contact";
                mail.Body = "Email: "+contEmail +"  " + contText;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(emailSendUser, emailPassword);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                ;
            }

            return true;
        }
    }
}