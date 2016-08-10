using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel;

public class ScheduleMailModel : MailModel 
{
    private DateTime _nextSendTime;
    public DateTime NextSendTime {
        get { return _nextSendTime; }
        set { _nextSendTime = value; }
    }

    private ScheduleType _scheduleType;
    public ScheduleType ScheduleType {
        get { return _scheduleType; }
        set { _scheduleType = value; }
    }

    private int _scheduleTime;
    public int ScheduleTime{
        get { return _scheduleTime; }
        set { _scheduleTime = value; }
    } 
}

public enum ScheduleType
{
    int HOURS = 0;
    int DAYLY = 1;
    int WEEKLY = 2;
    int MONTHLY = 3;
}