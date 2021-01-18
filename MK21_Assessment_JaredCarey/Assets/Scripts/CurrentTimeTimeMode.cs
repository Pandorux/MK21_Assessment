using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTimeTimeMode : AbstractTimeMode
{
    public void UpdateCurrentTime()
    {
        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();
        e.time = DateTime.Now.ToLongTimeString();

        TimeDisplayUpdated(e);
    }
}
