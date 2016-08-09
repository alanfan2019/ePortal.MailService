using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortal.MailService.ThreadFactory.ThreadPipe;
using System.Threading;

namespace ePortal.MailService.ThreadFactory
{
    internal class ThreadFactory : IDisposable
    {
        private IList<AbstractPipe> pipeList;

        public ThreadFactory(params AbstractPipe[] pipelines)
        {
            if (pipelines == null)
            {
                throw new NullReferenceException();
            }
            pipeList = pipelines;
        }

        public void Run()
        {
            foreach (var pipe in pipeList)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(pipe.Run));
            }
            Thread.Sleep(120000);
        }

        public void Stop()
        {
            foreach (var pipe in pipeList)
            {
                pipe.Stop();
            }
        }

        public void Dispose()
        {
            this.Stop();
        }
    }
}