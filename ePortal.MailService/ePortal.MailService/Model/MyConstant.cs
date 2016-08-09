using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ePortal.MailService.Model
{
    internal class MyConstant
    {
        private static Settings1 setting = new Settings1();
        #region DBconfig

        public static string Conn = setting.Conn;
        #endregion

    }
}
