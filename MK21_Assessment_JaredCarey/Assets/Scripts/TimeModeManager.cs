using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeModeManager : MonoBehaviour
{
    [SerializeField]
    private TimeModes startingTimeMode = TimeModes.TimeDisplay;
    public TimeModes currentTimeMode
    {
        get;
        private set;
    }

    [System.Serializable]
    private class TimeModeInformation
    {
        public ITimeMode timeMode;
        public GameObject timeModeGameObject;
        public TextMeshProUGUI timeDisplay;
    } 

    [SerializeField]
    private TimeModeInformation timeDisplayTimeMode;

    [SerializeField]
    private TimeModeInformation stopWatchTimeMode;

    private TimeModeInformation[] timeModes;

    void Start()
    {
        timeDisplayTimeMode.timeMode = timeDisplayTimeMode.timeModeGameObject.GetComponent<TimeDisplayTimeMode>();
        stopWatchTimeMode.timeMode = stopWatchTimeMode.timeModeGameObject.GetComponent<StopWatchTimeMode>();

        timeModes = new TimeModeInformation[] {
            timeDisplayTimeMode,
            stopWatchTimeMode
        };

        if(timeModes[(int)TimeModes.TimeDisplay].timeMode != null)
        {
            timeDisplayTimeMode.timeMode.timeDisplayUpdate += new OnTimeUpdateEventHandler(UpdateTimeDisplay);
        }
        
        if(timeModes[(int)TimeModes.StopWatch].timeMode != null)
        {
            stopWatchTimeMode.timeMode.timeDisplayUpdate += new OnTimeUpdateEventHandler(UpdateTimeDisplay);
        }
        

        SelectTimeMode((int)startingTimeMode);
    }
    
    public void SelectTimeMode(int timeModeIndex) 
    {
        timeModes[timeModeIndex].timeMode.StopTimeMode();
        currentTimeMode = (TimeModes)timeModeIndex;

        for(int i = 0; i < timeModes.Length; i++) 
        {
            bool isSelectedMode = (i == timeModeIndex);
            timeModes[i].timeModeGameObject.SetActive(isSelectedMode);
        }
    }

    public void ChangeTimeModeToCurrentTime() 
    {
        currentTimeMode = TimeModes.TimeDisplay;
        SelectTimeMode((int)currentTimeMode);
    }

    private void UpdateTimeDisplay(object sender, OnTimeUpdateEventArgs e)
    {
        timeModes[(int)currentTimeMode].timeDisplay.text = e.time;
    }
}
