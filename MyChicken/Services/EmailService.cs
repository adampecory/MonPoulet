using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MyChicken.Services
{
    public class EmailService
    {
        public void Sendmail(Email mail)
        {
            //Test mail 
            SendGrid.SendGridMessage msg = new SendGrid.SendGridMessage();
            msg.From = new System.Net.Mail.MailAddress("ne_pas_repondre@saveursdecheznous.com");
            msg.AddTo(mail.Recipient);
            msg.Subject = mail.Subject;
            msg.Text = mail.Content;

            var credentials = new NetworkCredential("pkaco", "dami07mata");
            var transport = new Web(credentials);
            transport.DeliverAsync(msg);
        }
    }

    public class Email
    {
        public string From { get; set; }
        public List<string> Recipient { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }


}