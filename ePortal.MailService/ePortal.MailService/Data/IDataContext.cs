using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortal.MailService.Model;

namespace ePortal.MailService.Data
{
    interface IDataContext
    {
        IList<MailModel> GetMailToSend();
        IList<MailModel> GetScheduleMail();
        void UpdateSchedule(long id);

        void InsertSendingMail(MailModel mail);
        void DeleteSendingMail(long id);

        void LogSendeMail(MailModel mail);
    }
}
