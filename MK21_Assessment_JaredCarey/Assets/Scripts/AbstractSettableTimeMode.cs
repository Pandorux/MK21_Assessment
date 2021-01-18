using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSettableTimeMode : AbstractTimeMode, ISettableTimeMode
{
    public abstract void SetTime();
}
