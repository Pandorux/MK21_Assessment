using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        [SerializeField]
        private TextMeshProUGUI timeDisplay;

        public string text
        {
            get
            {
                return timeDisplay.text;
            }

            set
            {
                timeDisplay.text = value;
            }
        }
    } 

    TimeModeInformation getActiveTimeMode
    {
        get
        {
            return timeModes[(int)currentTimeMode];
        }
    }

    [SerializeField]
    private TimeModeInformation timeDisplayTimeMode;

    [SerializeField]
    private TimeModeInformation stopWatchTimeMode;

    [SerializeField]
    private TimeModeInformation countDownTimeMode;

    private TimeModeInformation[] timeModes;

    [SerializeField]
    private  Button destroyButton;
    private bool m_CanUserDestroy = false;
    public bool canUserDestroy
    {
        get
        {
            return m_CanUserDestroy;
        }

        private set
        {
            m_CanUserDestroy = value;
        }
    }

    void Start()
    {
        timeDisplayTimeMode.timeMode = timeDisplayTimeMode.timeModeGameObject.GetComponent<TimeDisplayTimeMode>();
        stopWatchTimeMode.timeMode = stopWatchTimeMode.timeModeGameObject.GetComponent<StopWatchTimeMode>();
        countDownTimeMode.timeMode = countDownTimeMode.timeModeGameObject.GetComponent<CountDownTimeMode>();

        timeModes = new TimeModeInformation[] {
            timeDisplayTimeMode,
            stopWatchTimeMode,
            countDownTimeMode
        };

        if(timeModes[(int)TimeModes.TimeDisplay].timeMode != null)
        {
            timeDisplayTimeMode.timeMode.timeDisplayUpdate += new OnTimeUpdateEventHandler(UpdateTimeDisplay);
        }
        
        if(timeModes[(int)TimeModes.StopWatch].timeMode != null)
        {
            stopWatchTimeMode.timeMode.timeDisplayUpdate += new OnTimeUpdateEventHandler(UpdateTimeDisplay);
        }
        
        if(timeModes[(int)TimeModes.CountDown].timeMode != null)
        {
            countDownTimeMode.timeMode.timeDisplayUpdate += new OnTimeUpdateEventHandler(UpdateTimeDisplay);
        }

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

    public void StartActiveTimeMode()
    {
        timeModes[(int)currentTimeMode].timeMode.StopTimeMode();
    }

    public void StopActiveTimeMode()
    {
        timeModes[(int)currentTimeMode].timeMode.StopTimeMode();
    }

    public void ActiveTimeModeEdited()
    {
        if(getActiveTimeMode.timeMode is ISettableTimeMode)
        {
            OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();
            e.time = "This is working";

            #if UNITY_EDITOR
                Debug.Log($"User Edit Update Test: {e.time}");
            #endif
            
            ((ISettableTimeMode) getActiveTimeMode.timeMode).OnUserEdited(e);
        }
    }

    private void UpdateTimeDisplay(object sender, OnTimeUpdateEventArgs e)
    {
        timeModes[(int)currentTimeMode].text = e.time;
    }

    public void UserCannotDestroy()
    {
        canUserDestroy = false;
        destroyButton.interactable = canUserDestroy;
    }

    public void UserCanDestroy()
    {
        canUserDestroy = true;
        destroyButton.interactable = canUserDestroy;
    }
}
