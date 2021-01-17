using System;
using System.Collections;
using System.Collections.Generic;

public interface ITimeMode
{
    void StartTimeMode();
    void ResetTimeMode();
    void StopTimeMode();
    void ValidateInputtedTime();
    void TimeDisplayUpdated();
    event EventHandler TimeDisplayUpdate;
}
