using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortal.MailService.Model;

namespace ePortal.MailService.MailSender
{
    interface IMailSender
    {
        void Send(MailModel model);
    }
}
