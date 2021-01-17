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
    private TimeModeInformation currentTimeTimeMode;

    private TimeModeInformation[] timeModes;

    void Start()
    {
        currentTimeTimeMode.timeMode = currentTimeTimeMode.timeModeGameObject.GetComponent<CurrentTimeTimeMode>();

        timeModes = new TimeModeInformation[] {
            currentTimeTimeMode
        };

        currentTimeTimeMode.timeMode.timeDisplayUpdate += new OnTimeUpdateEventHandler(UpdateTimeDisplay);
        SelectTimeMode((int)startingTimeMode);
    }
    
    public void SelectTimeMode(int timeModeIndex) 
    {
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
