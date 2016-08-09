using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using ePortal.MailService.Model;
using ePortal.MailService.Queue;
using ePortal.MailService.Data;

namespace ePortal.MailService.ThreadFactory.ThreadPipe
{
    internal abstract class AbstractPipe
    {
        protected bool isRuning;
        protected IDataContext dataContext;
        protected IQueue queue;
        /// <summary>
        /// 线程信号指示
        /// </summary>
        protected ManualResetEvent _stoppedEvent;

        public AbstractPipe()
        {
            isRuning = true;
            dataContext = new DBContext();
            _stoppedEvent = new ManualResetEvent(false);
        }

        public virtual void Run(object state)
        {
            while (isRuning)
            {
                Run_Pipe();
                //线程信号指示Set()放行
                this._stoppedEvent.Set();
                Thread.Sleep(ServiceConfig.Interval);
            }
        }

        public virtual void Stop()
        {
            isRuning = false;
            //只有接收到信号指示Set()的时候才让线程停止
            this._stoppedEvent.WaitOne();
        }

        protected abstract void Run_Pipe();
    }
}