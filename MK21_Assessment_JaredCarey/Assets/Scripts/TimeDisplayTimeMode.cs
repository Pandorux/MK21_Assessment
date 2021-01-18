using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDisplayTimeMode : AbstractTimeMode
{
    void Update() 
    {
        if(GetIsTimeModeActive())
        {
            UpdateCurrentTime();
        }
    }

    public void UpdateCurrentTime()
    {
        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();
        e.time = DateTime.Now.ToLongTimeString();

        TimeDisplayUpdated(e);
    }
}
