using ePortal.MailService.Data;
using ePortal.MailService.Model;
using ePortal.MailService.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ePortal.MailService.Service
{
    public class MailServiceHost : IMailService
    {
        IDataContext dataContext=new Data.DBContext();

        public void SendMail(MailModel mail)
        {
            //dataContext.InsertSendingMail(model);
            //Send_Queue.Instance.Add(model);
            MailService.logger.Info("SendMail was Called");
            MailService.logger.Info(string.Format("MailInfo:{0},{1},{2}", mail.Subject, mail.To, mail.Body));
        }
    }
}
