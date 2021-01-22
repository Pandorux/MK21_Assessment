using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSettableTimeMode : AbstractTimeMode, ISettableTimeMode
{
    public event OnTimeUpdateEventHandler onUserEdit;
    public void OnUserEdited(OnTimeUpdateEventArgs e)
    {
        if(onUserEdit != null)
            onUserEdit(this, e);
    }

    public abstract void SetTime(TimeSpan newTime);
    protected abstract void SetTime(object sender, OnTimeUpdateEventArgs e);
}
