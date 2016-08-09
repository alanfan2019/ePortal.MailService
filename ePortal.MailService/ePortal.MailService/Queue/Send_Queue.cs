using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortal.MailService.Model;

namespace ePortal.MailService.Queue
{
    public class Send_Queue : IQueue
    {
        #region Fields
        private IList<MailModel> mailList;

        #endregion

        #region Singleton
        private static Send_Queue _instance;
        public static Send_Queue Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Send_Queue();
                }

                return _instance;
            }
        }
        #endregion

        #region Constructor(s)
        private Send_Queue()
        {
            mailList = new List<MailModel>();
        }

        #endregion

        #region IQueue

        private MailModel _queueData;
        public MailModel QueueData
        {
            get
            {
                return _queueData;
            }
            set
            {
                _queueData = value;
            }
        }

        public void Add(MailModel mail)
        {
            if (!mailList.Any(m => m.ID.Equals(mail)))
            {
                mailList.Add(mail);
            }
        }

        public void Add(IList<MailModel> mailList)
        {
            foreach (var m in mailList)
            {
                this.Add(m);
            }
        }

        public void Remove(MailModel mail)
        {
            mailList.Remove(mail);
        }

        public void Remove(long id)
        {
            var mail = mailList.SingleOrDefault(m => m.ID.Equals(id));

            if (mail != null)
            {
                this.Remove(mail);
            }
        }

        public bool Next()
        {
            if (mailList.Any())
            {
                QueueData = this.mailList[0];

                return true;
            }
            else
            {
                return false;
            }
        }

        public int Count()
        {
            return this.mailList.Count;
        }

        #endregion
    }
}