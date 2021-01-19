using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISettableTimeMode : ITimeMode
{
    void SetTime(float newValue);
    void OnUserEdited(OnTimeUpdateEventArgs e);
    event OnTimeUpdateEventHandler onUserEdit;
}
