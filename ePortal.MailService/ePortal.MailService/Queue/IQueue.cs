using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortal.MailService.Model;

namespace ePortal.MailService.Queue
{
    interface IQueue
    {
        MailModel QueueData { get; set; }

        void Add(MailModel mail);
        void Add(IList<MailModel> mailList);

        void Remove(MailModel mail);
        void Remove(long id);

        //IList<AbstractMail> GetData();
        bool Next();

        int Count();
    }
}
