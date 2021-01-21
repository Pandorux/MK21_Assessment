using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISettableTimeMode : ITimeMode
{
    void SetTime(TimeSpan newTime);
    void OnUserEdited(OnTimeUpdateEventArgs e);
    event OnTimeUpdateEventHandler onUserEdit;
}
