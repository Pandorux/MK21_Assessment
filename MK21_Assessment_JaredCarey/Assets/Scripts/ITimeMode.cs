using System;
using System.Collections;
using System.Collections.Generic;

public interface ITimeMode
{
    bool GetIsTimeModeActive();
    void StartTimeMode();
    void ResetTimeMode();
    void StopTimeMode();
    void ValidateInputtedTime();
    void TimeDisplayUpdated(OnTimeUpdateEventArgs e);
    event OnTimeUpdateEventHandler timeDisplayUpdate;
}

public delegate void OnTimeUpdateEventHandler(object sender, OnTimeUpdateEventArgs e);
public class OnTimeUpdateEventArgs : EventArgs
{
    public string time;
}
