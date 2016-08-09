using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ePortal.MailService.Model
{
    public class Send_MailModel : MailModel
    {
        public Send_MailModel(MailModel model)
        {
            ID = model.ID;
            Subject = model.Subject;
            To = model.To;
            Cc = model.Cc;
            Bcc = model.Bcc;
            Body = model.Body;
            Sys = model.Sys;
        }
    }
}