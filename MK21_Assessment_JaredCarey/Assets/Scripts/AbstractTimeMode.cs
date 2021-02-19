using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTimeMode : MonoBehaviour, ITimeMode, IMonoBehaviourEventSubscriptions
{
    protected bool isTimeModeActive = false;
    protected bool areMonobehaviourEventsSubscribed = false;

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

    // IMonoBehaviourEventSubscriptions Implementation
    void OnEnable()
    {
        SubscribeEvents();
    }

    void OnDisable()
    {
        UnsubscribeEvents();
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    public virtual void SubscribeEvents()
    {
        areMonobehaviourEventsSubscribed = true;
    }

    public virtual void UnsubscribeEvents()
    {
        areMonobehaviourEventsSubscribed = false;
    }
}
