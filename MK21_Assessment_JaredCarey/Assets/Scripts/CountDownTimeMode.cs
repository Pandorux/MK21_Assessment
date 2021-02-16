using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CountDownTimeMode : AbstractSettableTimeMode
{
    [SerializeField]
    private AudioSource countDownAlarm;

    private Stopwatch timeElapsed = new Stopwatch();
    private TimeSpan countdownStartTime = TimeSpan.Zero;
    private bool m_IsAlarmPlaying;

    public bool isAlarmPlaying
    {
        get
        {
            return m_IsAlarmPlaying;
        }

        private set
        {
            m_IsAlarmPlaying = value;
        }
    }

    public TimeSpan getTimeRemaining
    {
        get
        {
            TimeSpan timeLeft = DateTimeHelper.GetTimeDifference(timeElapsed.Elapsed, countdownStartTime);
            return timeLeft;
        }
    }

    void Awake()
    {
        onUserEdit += new OnTimeUpdateEventHandler(SetTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(GetIsTimeModeActive()) 
        {
            UpdateDisplayWithCountDownTime();

            if(getTimeRemaining < TimeSpan.Zero && !isAlarmPlaying)
            {
                PlayAlarmClock();
            }
        }
    }

    public override void StartTimeMode() 
    {
        base.StartTimeMode();
        timeElapsed.Start();
    }

    public override void StopTimeMode() 
    {
        base.StopTimeMode();
        timeElapsed.Stop();

        if(isAlarmPlaying)
        {
            StopAlarmClock();
        }
    }

    public override void ResetTimeMode() 
    {
        base.ResetTimeMode();
        timeElapsed.Reset();
    }

    public void UpdateDisplayWithCountDownTime()
    {
        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();
        e.time = getTimeRemaining.ToString();

        TimeDisplayUpdated(e);
    }

    public override void SetTime(TimeSpan newTime)
    {
        countdownStartTime = newTime;
        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();
        e.time = getTimeRemaining.ToString();

        TimeDisplayUpdated(e);
    }

    protected override void SetTime(object sender, OnTimeUpdateEventArgs e)
    {
        SetTime(new TimeSpan(e.hours, e.minutes, e.seconds));
    }

    public void PlayAlarmClock()
    {
        isAlarmPlaying = true;
        countDownAlarm.Play();
    }

    public void StopAlarmClock()
    {
        isAlarmPlaying = false;
        countDownAlarm.Stop();
    }
}
