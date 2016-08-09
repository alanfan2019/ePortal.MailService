using ePortal.MailService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ePortal.MailService.ThreadFactory.ThreadPipe
{
    class WebService_PipeLine : AbstractPipe
    {
        public override void Run(object state)
        {
            using (ServiceHost host = new ServiceHost(typeof(MailServiceHost)))
            {
                host.Opened += delegate
                {
                    MailService.logger.Info("MailServiceHos is Running");
                };
                host.Closing += delegate
                {
                    MailService.logger.Info("MailServiceHost is Closing");
                };
                host.Faulted += delegate
                {
                    MailService.logger.Info("MailServiceHost is Faulted");
                };

                host.Open();
                base.Run(state);
            }
            //线程信号指示Set()放行
            _stoppedEvent.Set();
        }

        protected override void Run_Pipe()
        {
        }
    }
}
