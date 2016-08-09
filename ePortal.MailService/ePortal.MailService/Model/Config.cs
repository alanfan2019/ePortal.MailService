using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ePortal.MailService.Model
{
    public  class ServiceConfig
    {
        public static string DBConn
        {
            get { return AppSetting.Default.Conn; }
        }

        public static int Interval
        {
            get
            {
                return AppSetting.Default.Interval * 1000;
            }
        }
    }

    public class SMTPConfig
    {
        public string SMTPClient { get; set; }
        public int Port { get; set; }
        public int Authenticate { get; set; }
        public string User { get; set; }
        public string Pwd { get; set; }
        public bool SSL { get; set; }
    }
}
