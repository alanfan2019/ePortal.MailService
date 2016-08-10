using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ePortal.MailService.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace ePortal.MailService.Data
{
    internal class DBContext : IDataContext
    {
        private const string GETMAILTOSEND = "SELECT q.ID, q.Subject,q.[To],q.CC, c.Bcc, q.Body,c.MailAddress AS [From], c.SMTPClient, c.Port, c.Authenticate, c.[User], c.Pwd, c.SSL from  MailService_Sending_Queue q left join MailService_Config c on q.SysID=c.SysID";
        private const string INSERTSENDINGQUEUE = "INSERT INTO MailService_Sending_Queue(Subject, [To], CC, Body, SysID) VALUES(@p1, @p2, @p3, @p4, @p5)";
        private const string DELETESENDINGQUEUE = "DELETE FROM MailService_Sending_Queue WHERE ID = @p1";
        private const string LOGSENDEDMAIL = "INSERT INTO MailService_Sended_Log(ID, Subject, [To], CC, Bcc, Body, SysID, SendTime) VALUES(@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)";
        private const string GETSCHEDULEMAIL = "SELECT q.ID, q.Subject,q.[To],q.CC, c.Bcc, q.Body,c.MailAddress AS [From], q.nextSendTime, q.sheduleType, q.scheduleTime, c.SMTPClient, c.Port, c.Authenticate, c.[User], c.Pwd, c.SSL from MailService_Schedule q left join MailService_Config c on q.SysID=c.SysID and q.nextSendTime <= GETDATE()";
        private const string SETSHEDULEMAIL = "UPDATE MailService_Schedule SET nextSendTime = @p1 WHERE ID = @p2";
        private static Database database;

        public DBContext()
        {
            database = new SqlDatabase(ServiceConfig.DBConn);
        }

        public IList<MailModel> GetMailToSend()
        {
            IList<MailModel> mailList = new List<MailModel>();

            using (IDataReader reader = database.ExecuteReader(CommandType.Text, GETMAILTOSEND))
            {
                while (reader.Read())
                {
                    var mail = new MailModel()
                    {
                        ID = reader.GetInt64(0),
                        Subject = reader.GetString(1),
                        To = reader.GetString(2),
                        Cc = reader.GetString(3),
                        Bcc = reader.GetString(4),
                        Body = reader.GetString(5),
                        From = reader.GetString(6),
                        Config = new SMTPConfig()
                        {
                            SMTPClient = reader.GetString(7),
                            Port = reader.GetInt32(8),
                            Authenticate = reader.GetInt32(9),
                            User = reader.GetString(10),
                            Pwd = reader.GetString(11),
                            SSL = reader.GetBoolean(12)
                        }
                    };
                    mailList.Add(mail);
                }
            }

            return mailList;
        }

        public IList<MailModel> GetScheduleMail()
        {
            IList<MailModel> mailList = new List<MailModel>();

            using (IDataReader reader = database.ExecuteReader(CommandType.Text, GETSCHEDULEMAIL))
            {
                while (reader.Read())
                {
                    var mail = new ScheduleMailModel()
                    {
                        ID = reader.GetInt64(0),
                        Subject = reader.GetString(1),
                        To = reader.GetString(2),
                        Cc = reader.GetString(3),
                        Bcc = reader.GetString(4),
                        Body = reader.GetString(5),
                        From = reader.GetString(6),
                        NextSendTime = reader.GETDATE(7),
                        ScheduleType = (ScheduleType)reader.GetInt32(8),
                        ScheduleTime = (ScheduleTime)reader.GetInt32(9),
                        Config = new SMTPConfig()
                        {
                            SMTPClient = reader.GetString(10),
                            Port = reader.GetInt32(11),
                            Authenticate = reader.GetInt32(12),
                            User = reader.GetString(13),
                            Pwd = reader.GetString(14),
                            SSL = reader.GetBoolean(15)
                        },
                        _callback = new SenderCallback(UpdateSchedule)
                    };
                    mailList.Add(mail);
                }
            }

            return mailList;
        }
        public void UpdateSchedule(MailModel model)
        {
            ScheduleMailModel scheduleMailModel = model as ScheduleMailModel;
            if (scheduleMailModel != null)
            {
                var nextSendTime = scheduleMailModel.nextSendTime;
                switch (scheduleMailModel.ScheduleType)
                {
                    case ScheduleType.HOURS:
                        nextSendTime = nextSendTime.AddHours(scheduleMailModel.ScheduleTime);
                        break;
                    case ScheduleType.DAYLY:
                        nextSendTime = nextSendTime.AddDays(scheduleMailModel.ScheduleTime);
                        break;
                    case ScheduleType.MONTHLY:
                        nextSendTime = nextSendTime.AddMonths(scheduleMailModel.ScheduleTime);
                        break;
                    default:
                }

                SqlCommand cmd = new SqlCommand(SETSHEDULEMAIL);
                cmd.Parameters.Add("@p1", nextSendTime.ToString());
                cmd.Parameters.Add("@p2", scheduleMailModel.ID);

                database.ExecuteNonQuery(cmd);
            }

        }

        public void InsertSendingMail(MailModel mail)
        {
            SqlCommand cmd = new SqlCommand(INSERTSENDINGQUEUE);
            cmd.Parameters.Add("@p1", System.Data.SqlDbType.NVarChar, 200).Value = mail.Subject;
            cmd.Parameters.Add("@p2", System.Data.SqlDbType.NVarChar, -1).Value = mail.To;
            cmd.Parameters.Add("@p3", System.Data.SqlDbType.NVarChar, -1).Value = mail.Cc;
            cmd.Parameters.Add("@p4", System.Data.SqlDbType.NVarChar, -1).Value = mail.Body;
            cmd.Parameters.Add("@p5", System.Data.SqlDbType.NVarChar, 100).Value = mail.Sys;

            database.ExecuteNonQuery(cmd);
        }

        public void DeleteSendingMail(long id)
        {
            SqlCommand cmd = new SqlCommand(DELETESENDINGQUEUE);
            cmd.Parameters.Add("@p1", System.Data.SqlDbType.BigInt).Value = id;

            database.ExecuteNonQuery(cmd);
        }

        public void LogSendeMail(MailModel mail)
        {
            SqlCommand cmd = new SqlCommand(LOGSENDEDMAIL);

            cmd.Parameters.AddWithValue("@p1", mail.ID);
            cmd.Parameters.AddWithValue("@p2", mail.Subject);
            cmd.Parameters.AddWithValue("@p3", mail.To);
            cmd.Parameters.AddWithValue("@p4", mail.Cc);
            cmd.Parameters.AddWithValue("@p5", mail.Bcc);
            cmd.Parameters.AddWithValue("@p6", mail.Body);
            cmd.Parameters.AddWithValue("@p7", mail.Sys);
            cmd.Parameters.AddWithValue("@p8", mail.SendTime);

            database.ExecuteNonQuery(cmd);
        }
    }
}
