using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortal.MailService.Model;

namespace ePortal.MailService.Queue
{
    public class Recive_Queue : IQueue
    {
        #region Fields
        private IList<MailModel> mailList;

        #endregion

        #region Singleton
        private static Recive_Queue _instance;
        public static Recive_Queue Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Recive_Queue();
                }

                return _instance;
            }
        }
        #endregion

        #region Constructor(s)
        private Recive_Queue()
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

        public void Add(MailModel model)
        {
            mailList.Add(model);
        }

        public void Add(IList<MailModel> mailList)
        {
            throw new NotImplementedException();
        }

        public void Remove(MailModel model)
        {
            mailList.Remove(model);
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
                QueueData = mailList[0];

                return true;
            }
            else
            {
                return false;
            }
        }

        public int Count()
        {
            return mailList.Count;
        }
        #endregion

    }
}