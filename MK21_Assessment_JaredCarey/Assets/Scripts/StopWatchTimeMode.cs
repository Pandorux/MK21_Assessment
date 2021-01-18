using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StopWatchTimeMode : AbstractTimeMode
{
    private Stopwatch stopWatch = new Stopwatch();

    // Update is called once per frame
    void Update()
    {
        if(GetIsTimeModeActive())
        {
            UpdateDisplayWithStopWatchTime();
        }
    }

    public override void StartTimeMode()
    {
        base.StartTimeMode();
        stopWatch.Start();
    }

    public override void ResetTimeMode()
    {
        base.ResetTimeMode();
        stopWatch.Reset();
        
    }

    public override void StopTimeMode()
    {
        base.StopTimeMode();
        stopWatch.Stop();
    }

    public void UpdateDisplayWithStopWatchTime()
    {
        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();
        e.time = stopWatch.Elapsed.ToString();

        TimeDisplayUpdated(e);
    }
}
