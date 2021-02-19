using System;
using System.Collections;
using System.Collections.Generic;

///<summary>
/// Interface that contains all the generic implementation requirements a time mode needs 
///<summary>
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

///<summary>
/// All possible time modes that have been created and can be chosen
///<summary>
public enum TimeModes {
    TimeDisplay,
    StopWatch,
    CountDown
}

public delegate void OnTimeUpdateEventHandler(object sender, OnTimeUpdateEventArgs e);
public class OnTimeUpdateEventArgs : EventArgs
{
    public string time;
    public int hours;
    public int minutes;
    public int seconds;
}

