using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTimeTimeMode : MonoBehaviour, ITimeMode
{

    private bool isTimeModeActive = false;

    public event OnTimeUpdateEventHandler timeDisplayUpdate;
    public void TimeDisplayUpdated(OnTimeUpdateEventArgs e)
    {
        if(timeDisplayUpdate != null)
            timeDisplayUpdate(this, e);
    }

    public bool GetIsTimeModeActive()
    {
        return isTimeModeActive;
    }

    private void SetIsTimeModeActive(bool newState)
    {
        isTimeModeActive = newState;
    }

    public void StartTimeMode()
    {
        SetIsTimeModeActive(true);
    }

    public void ResetTimeMode()
    {
        SetIsTimeModeActive(false);
    }

    public void StopTimeMode()
    {
        SetIsTimeModeActive(false);
    }

    public void ValidateInputtedTime()
    {
        // TODO:
        throw new NotImplementedException();
    }
    
}
