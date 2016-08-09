using ePortal.MailService.MailSender;
using ePortal.MailService.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ePortal.MailService.ThreadFactory.ThreadPipe
{
    public class Schedule_PipeLine : AbstractPipe
    {
        
        IMailSender sender;

        public Schedule_PipeLine()
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
            CheckSchedule();
        }

        public void CheckSchedule()
        {
            var mailList = dataContext.GetScheduleMail();
            queue.Add(mailList);
        }
    }
}
