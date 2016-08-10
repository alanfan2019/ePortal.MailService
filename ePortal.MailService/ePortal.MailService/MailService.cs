using ePortal.MailService.Service;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;

namespace ePortal.MailService
{
    public partial class MailService : ServiceBase
    {
        private ThreadFactory.ThreadFactory _factory;
        public static ILog logger;

        public MailService()
        {
            InitializeComponent();
            logger = LogManager.GetLogger(this.GetType());
        }

        public void Test()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            _factory = new ThreadFactory.ThreadFactory(
                new ThreadFactory.ThreadPipe.Recive_PipeLine()
                , new ThreadFactory.ThreadPipe.Send_PipeLine()
                , new ThreadFactory.ThreadPipe.WebService_PipeLine()
                , new ThreadFactory.ThreadPipe.Schedule_PipeLine()
                );

            _factory.Run();
        }

        protected override void OnStop()
        {
            MailService.logger.Info("MailService trying to Stop");
            _factory.Stop();
            MailService.logger.Info("MailService is stoped");
        }

    }
}
