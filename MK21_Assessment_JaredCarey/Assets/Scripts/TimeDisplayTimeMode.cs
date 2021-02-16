using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisplayTimeMode : AbstractSettableTimeMode
{
    public readonly static string[] TimeFormats = new string[] {
        "h:mm tt",
        "h:mm:ss tt",
        "hh:mm:ss tt",
        "HH:mm:ss",
        "hh:mm:ss",
        "H:mm:ss",
        "h:mm:ss",
        "H:mm",
        "h:mm"
    };

    private string defaultTimeFormat = TimeFormats[0];
    private string m_ChosenTimeFormat;
    public string chosenTimeFormat
    {
        get
        {
            return m_ChosenTimeFormat;
        }

        set
        {
            if(Array.Exists(TimeFormats, timeFormat => timeFormat == value))
            {
                m_ChosenTimeFormat = value;
            }
            else
            {
                // TODO: Throw an exception
            }
        }
    }

    public DateTime getCurrentTime
    {
        get
        {
            DateTime adjustedTime = DateTime.Now.Add(timeAdjustment);

            return adjustedTime;
        }
    }

    private TimeSpan m_TimeAdjustment = TimeSpan.Zero;
    public TimeSpan timeAdjustment
    {
        get
        {
            return m_TimeAdjustment;
        }

        private set
        {
            m_TimeAdjustment = value;
        }
    }

    public int hoursTimeAdjustment
    {
        get
        {
            return timeAdjustment.Hours;
        }

        set
        {
            timeAdjustment = new TimeSpan(value, minutesTimeAdjustment, secondsTimeAdjustment);
        }
    } 

    public int minutesTimeAdjustment
    {
        get
        {
            return timeAdjustment.Minutes;
        }

        set
        {
            timeAdjustment = new TimeSpan(hoursTimeAdjustment, value, secondsTimeAdjustment);
        }
    } 

    public int secondsTimeAdjustment
    {
        get
        {
            return timeAdjustment.Seconds;
        }

        set
        {
            timeAdjustment = new TimeSpan(hoursTimeAdjustment, minutesTimeAdjustment, value);
        }
    }

    void Awake()
    {
        chosenTimeFormat = defaultTimeFormat;

        onUserEdit += new OnTimeUpdateEventHandler(SetTime);
        StartTimeMode();

        #if UNITY_EDITOR
            Debug.Log($"DateTime Max Hours Value: {DateTime.MaxValue.Hour}");
            Debug.Log($"DateTime Max Minutes Value: {DateTime.MaxValue.Minute}");
            Debug.Log($"DateTime Max Seconds Value: {DateTime.MaxValue.Second}");
        #endif
    }

    void OnEnable()
    {
        UpdateManager.instance.userInterfaceUpdate += new Update(UpdateDisplayWithCurrentTime);
    }

    void OnDisable()
    {
        UpdateManager.instance.userInterfaceUpdate -= new Update(UpdateDisplayWithCurrentTime);
    }

    public void UpdateDisplayWithCurrentTime(object sender)
    {
        if(GetIsTimeModeActive())
        {
            UpdateDisplayWithCurrentTime();
        }
    }

    public void UpdateDisplayWithCurrentTime()
    {
        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();
        e.time = getCurrentTime.ToString(chosenTimeFormat);

        TimeDisplayUpdated(e);
    }

    public override void SetTime(TimeSpan newTime)
    {
        timeAdjustment = DateTimeHelper.GetTimeDifference(
            DateTime.Now.TimeOfDay, 
            newTime
        );

        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();
        e.time = getCurrentTime.ToString(chosenTimeFormat);

        TimeDisplayUpdated(e);
    }

    protected override void SetTime(object sender, OnTimeUpdateEventArgs e)
    {
        SetTime(new TimeSpan(
            e.hours,
            e.minutes,
            e.seconds
        ));
    }

}
