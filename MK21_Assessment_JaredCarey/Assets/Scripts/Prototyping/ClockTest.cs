using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTest : MonoBehaviour
{
    [SerializeField]
    private TimeModes startingTimeMode = TimeModes.TimeDisplay;

    [SerializeField]
    private TimeDisplayTest timeDisplay;

    [SerializeField]
    private StopwatchTest stopWatch;

    [SerializeField]
    private CountdownTest countdown;

    public enum TimeModes {
        TimeDisplay,
        StopWatch,
        Countdown
    }

    protected GameObject[] timeModeGameObjects;

    // Start is called before the first frame update
    void Start()
    {
        timeModeGameObjects = new GameObject[] {
            timeDisplay.gameObject,
            stopWatch.gameObject,
            countdown.gameObject
        };

        SelectTimeMode((int)startingTimeMode);
    }

    public void SelectTimeMode(int timeModeIndex) 
    {
        for(int i = 0; i < timeModeGameObjects.Length; i++) 
        {
            bool isSelectedMode = (i == timeModeIndex);
            timeModeGameObjects[i].gameObject.SetActive(isSelectedMode);
        }
    }
}
