using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// An extension of ITimeMode that adds generic functionality for time modes that can be altered
///<summary>
public interface ISettableTimeMode : ITimeMode
{
    void SetTime(TimeSpan newTime);
    void OnUserEdited(OnTimeUpdateEventArgs e);
    event OnTimeUpdateEventHandler onUserEdit;
}
