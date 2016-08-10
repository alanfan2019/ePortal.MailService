using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortal.MailService.Model;

namespace ePortal.MailService.MailSender
{
    public delegate void SenderCallback(MailModel);

    interface IMailSender
    {
        void Send(MailModel model);
    }
}
