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
    private string chosenTimeFormatIndex;

    public DateTime getCurrentTime
    {
        get
        {
            return DateTime.Now;
        }
    }

    void Start()
    {
        chosenTimeFormatIndex = defaultTimeFormat;

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
        e.time = DateTime.Now.ToString(chosenTimeFormatIndex);

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
