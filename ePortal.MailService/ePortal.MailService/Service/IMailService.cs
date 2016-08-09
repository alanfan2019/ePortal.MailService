using ePortal.MailService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ePortal.MailService.Service
{

    [ServiceContract( Name="MailService")]
    [ServiceKnownType(typeof(MailModel))]
    public interface IMailService
    {
        [OperationContract]
        void SendMail(MailModel mail);
    }
}
