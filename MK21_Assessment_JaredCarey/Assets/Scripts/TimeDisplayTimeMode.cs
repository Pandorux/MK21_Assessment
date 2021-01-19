using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisplayTimeMode : AbstractSettableTimeMode
{
    void Start()
    {
        onUserEdit += new OnTimeUpdateEventHandler(SetTime);
        StartTimeMode();
    }

    void Update() 
    {
        if(GetIsTimeModeActive())
        {
            UpdateDisplayWithCurrentTime();
        }
    }

    public void UpdateDisplayWithCurrentTime()
    {
        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();
        e.time = DateTime.Now.ToLongTimeString();

        TimeDisplayUpdated(e);
    }

    public override void SetTime(float newValue)
    {
        // TODO:
        throw new NotImplementedException();
    }

    protected override void SetTime(object sender, OnTimeUpdateEventArgs e)
    {
        TimeDisplayUpdated(e);
    }
}
