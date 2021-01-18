using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSettableTimeMode : AbstractTimeMode, ISettableTimeMode
{
    public event OnTimeUpdateEventHandler userTimeUpdate;
    public void UserTimeUpdated(OnTimeUpdateEventArgs e)
    {
        if(userTimeUpdate != null)
            userTimeUpdate(this, e);
    }

    public abstract void SetTime(float newValue);
    protected abstract void SetTime(object sender, OnTimeUpdateEventArgs e);
}
