using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTimeMode : MonoBehaviour, ITimeMode
{
    protected bool isTimeModeActive = false;

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

    public virtual void StartTimeMode()
    {
        SetIsTimeModeActive(true);
    }

    public virtual void ResetTimeMode()
    {
        SetIsTimeModeActive(false);
    }

    public virtual void StopTimeMode()
    {
        SetIsTimeModeActive(false);
    }

    public virtual void ValidateInputtedTime()
    {
        // TODO:
        throw new NotImplementedException();
    }
}
