using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ePortal.MailService.Queue.Reader
{
    interface IQueueReader<T>
    {
        bool Read();
        T GetData();
    }
}
