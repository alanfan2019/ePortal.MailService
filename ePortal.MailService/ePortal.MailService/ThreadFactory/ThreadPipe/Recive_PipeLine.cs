using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortal.MailService.Queue;
using ePortal.MailService.Model;

namespace ePortal.MailService.ThreadFactory.ThreadPipe
{
    internal class Recive_PipeLine : AbstractPipe
    {
        public Recive_PipeLine()
            : base()
        {
            queue = Send_Queue.Instance;
        }

        public override void Run(object state)
        {
            base.Run(state);
            //线程信号指示Set()放行
            _stoppedEvent.Set();
        }

        protected override void Run_Pipe()
        {
            ReadToSend();
        }

        private void ReadToSend()
        {
            var mailList = dataContext.GetMailToSend();
            queue.Add(mailList);
        }
    }
}