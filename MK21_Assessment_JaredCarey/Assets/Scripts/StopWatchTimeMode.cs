using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StopWatchTimeMode : AbstractTimeMode
{
    public readonly static string StopWatchDisplayFormat = @"hh\:mm\:ss";
    private Stopwatch stopWatch = new Stopwatch();

    void OnEnable()
    {
        UpdateManager.instance.userInterfaceUpdate += new Update(UpdateDisplayWithStopWatchTime);
    }

    void OnDisable()
    {
        UpdateManager.instance.userInterfaceUpdate -= new Update(UpdateDisplayWithStopWatchTime);
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
        UpdateDisplayWithStopWatchTime();
    }

    public override void StopTimeMode()
    {
        base.StopTimeMode();
        stopWatch.Stop();
    }

    public void UpdateDisplayWithStopWatchTime(object sender)
    {
        if(GetIsTimeModeActive())
        {
            UpdateDisplayWithStopWatchTime();
        }
    }

    public void UpdateDisplayWithStopWatchTime()
    {
        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();
        e.time = stopWatch.Elapsed.ToString(StopWatchDisplayFormat);

        TimeDisplayUpdated(e);
    }
}
