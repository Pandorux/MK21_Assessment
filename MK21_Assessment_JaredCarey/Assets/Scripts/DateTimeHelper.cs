using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DateTimeHelper
{
    public static bool DoesTimeFormatUse24HourClock(string timeFormat)
    {
        if(timeFormat.StartsWith("H"))
        {
            return true;
        }

        return false;
    }

    public static bool DoesTimeFormatShowAMAndPM(string timeFormat)
    {
        if(timeFormat.EndsWith("t"))
        {
            return true;
        }

        return false;
    }

    
    public static bool DoesTimeFormatShowSeconds(string timeFormat)
    {
        if(timeFormat.Contains("s"))
        {
            return true;
        }

        return false;
    }
}
