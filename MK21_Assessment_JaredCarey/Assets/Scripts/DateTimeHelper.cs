using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DateTimeHelper
{
    public static TimeSpan GetTimeDifference(TimeSpan timeA, TimeSpan timeB)
    {
        TimeSpan timeDiff = new TimeSpan(
            timeB.Hours - timeA.Hours, 
            timeB.Minutes - timeA.Minutes, 
            timeB.Seconds - timeA.Seconds
        );

        return timeDiff;
    }

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

    public static bool IsItPM(int currentHour)
    {
        if(currentHour > 12)
        {
            return true;
        }

        return false;
    }

    public static int ConvertHoursTo12HourClock(int currentHour)
    {
        // You can also do Convert.ToInt32(dateTime.ToString("h")), but this seems quite wasteful of the CPU
        return ((currentHour + 11) % 12) + 1;
    }
}
