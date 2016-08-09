using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ePortal.MailService.Log
{
    internal class LogFactory
    {
        private IList<ILog> logList;
        private LogFactory _instance;
        public LogFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogFactory();
                }
                return _instance;
            }
        }

        private LogFactory()
        {
            logList = LogManager.GetCurrentLoggers();
        }
    }
}
