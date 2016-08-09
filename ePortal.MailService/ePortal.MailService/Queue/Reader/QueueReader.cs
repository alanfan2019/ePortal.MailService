using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ePortal.MailService.Queue.Reader
{
    internal class QueueReader<T> : IQueueReader<T>, IDisposable
    {
        private IList<T> queueList;
        private int currentIndex;

        public QueueReader(IList<T> queueList)
        {
            this.queueList = queueList;
            currentIndex = -1;
        }

        public bool Read()
        {
            currentIndex++;
            if (queueList.Count > currentIndex)
            {
                return true;
            }

            return false;
        }


        public T GetData()
        {
            try
            {
                return queueList[currentIndex];
            }
            catch (IndexOutOfRangeException ex)
            {
                currentIndex = queueList.Count;
                return default(T);
            }
            catch (NullReferenceException ex)
            {
                return default(T);
            }
        }

        public void Dispose()
        {
            this.queueList = null;
        }
    }
}
