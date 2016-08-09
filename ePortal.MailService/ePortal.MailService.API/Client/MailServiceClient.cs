using ePortal.MailService.API.MailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ePortal.MailService.API.Client
{
    public class MailServiceClient
    {
        public void SendMail(MailModel mail)
        {
            using (MailService.MailServiceClient client = 
                new MailService.MailServiceClient(new WSHttpBinding(),
                    new EndpointAddress("http://127.0.0.1:8733/MailService/")))
            {
                client.SendMail(mail);
            }
            
        }
    }
}
