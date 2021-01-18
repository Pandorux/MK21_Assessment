using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CountDownTimeMode : AbstractTimeMode
{
    private Stopwatch timeElapsed = new Stopwatch();
    private float countdownStartTime = 0;

    public float getTimeRemaining
    {
        get
        {
            TimeSpan ts = timeElapsed.Elapsed;
            float timeRemaining = countdownStartTime - ts.Seconds;
            return timeRemaining;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GetIsTimeModeActive()) 
        {
            UpdateDisplayWithCountDownTime();
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
    }

    public override void ResetTimeMode() 
    {
        base.ResetTimeMode();
        timeElapsed.Reset();
    }

    public void SetCountDownTime() 
    {
        countdownStartTime = 120f;
    }

    public void UpdateDisplayWithCountDownTime()
    {
        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();
        e.time = getTimeRemaining.ToString();

        TimeDisplayUpdated(e);
    }
}
