using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace ePortal.MailService.Model
{
    [Serializable]
    [DataContract]
    [KnownType(typeof(MailModel))]
    public class MailModel
    {
        private string _subject;
        private string _to;
        private string _cc;
        private string _bcc;
        private string _body;
        private string _sys;

        public long ID { get; set; }
        public string From { get; set; }
        public DateTime SendTime { get; set; }
        public SMTPConfig Config { get; set; }

        [DataMember]
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        [DataMember]
        public string To
        {
            get { return _to; }
            set { _to = value; }
        }

        [DataMember]
        public string Cc
        {
            get { return _cc; }
            set { _cc = value; }
        }

        [DataMember]
        public string Bcc
        {
            get { return _bcc; }
            set { _bcc = value; }
        }

        [DataMember]
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        [DataMember]
        public string Sys
        {
            get { return _sys; }
            set { _sys = value; }
        }

    }
}