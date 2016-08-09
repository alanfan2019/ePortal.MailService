using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;

namespace ePortal.MailService.MailSender
{
    public class SMTPSender : IMailSender
    {
        public void Send(Model.MailModel mail)
        {
            MailMessage msg = new MailMessage();
            msg.From = mail.From;
            msg.To = mail.To.Trim(',');
            msg.Cc = !string.IsNullOrEmpty(mail.Cc) ? mail.Cc.Trim(',') : string.Empty;
            msg.Bcc = !string.IsNullOrEmpty(mail.Cc) ? mail.Cc.Trim(',') : string.Empty;
            msg.Subject = mail.Subject;
            msg.Body = mail.Body;

            msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", mail.Config.SMTPClient); // set smtp server 
            msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", mail.Config.Port); // set smtp port 
            msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2"); // set sendusing 
            msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", mail.Config.Authenticate);   //basic   authentication   
            msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", mail.Config.User);   //set   your   username   here   
            msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", mail.Config.Pwd);   //set   your   password   here   
            msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", mail.Config.SSL);

            msg.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            msg.BodyFormat = MailFormat.Html;
            msg.Priority = MailPriority.High;

            SmtpMail.Send(msg);
        }
    }
}