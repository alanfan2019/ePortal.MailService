using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortal.MailService.Queue;
using ePortal.MailService.MailSender;

namespace ePortal.MailService.ThreadFactory.ThreadPipe
{
    internal class Send_PipeLine :AbstractPipe
    {
        IMailSender sender;

        public Send_PipeLine()
            : base()
        {
            queue = Send_Queue.Instance;
            sender = new SMTPSender();
        }

        public override void Run(object state)
        {
            base.Run(state);
            //线程信号指示Set()放行
            _stoppedEvent.Set();
        }

        protected override void Run_Pipe()
        {
            SendMail();
        }

        private void SendMail()
        {
            var mail = queue.QueueData;
            while (queue.Next())
            {
                MailService.logger.Info(string.Format("Mail {0} try to send", mail.ID));
                mail.SendTime = DateTime.Now;

                sender.Send(mail);

                dataContext.UpdateSchedule(mail.ID);
                dataContext.LogSendeMail(mail);
                dataContext.DeleteSendingMail(mail.ID);

                queue.Remove(mail);
            }
        }
    }
}