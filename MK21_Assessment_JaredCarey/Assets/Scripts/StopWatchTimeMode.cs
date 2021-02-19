using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StopWatchTimeMode : AbstractTimeMode
{
    public readonly static string StopWatchDisplayFormat = @"hh\:mm\:ss";
    private Stopwatch stopWatch = new Stopwatch();



    // Stop Watch Time Mode Mthods
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



    // IMonobehaviourEventSubscriptions Methods
    public override void SubscribeEvents()
    {
        if(areMonobehaviourEventsSubscribed != true)
        {
            UpdateManager.instance.userInterfaceUpdate += new Update(UpdateDisplayWithStopWatchTime);
            base.SubscribeEvents();
        }
    }

    public override void UnsubscribeEvents()
    {
        UpdateManager.instance.userInterfaceUpdate -= new Update(UpdateDisplayWithStopWatchTime);
        base.UnsubscribeEvents();
    }
}
