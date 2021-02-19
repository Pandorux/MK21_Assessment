using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class handles swapping between different Time Modes of a clock and triggering their generic functions (Starting and stopping)
/// </summary>
public class TimeModeManager : MonoBehaviour
{

    /// <summary>
    /// Internal Container class of generic information needed about a Time Mode. 
    /// Also enables the information required to be shown clearly in the Unity Inspector.
    /// </summary>
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



    // Time Mode Variables
    [SerializeField]
    private TimeModes startingTimeMode = TimeModes.TimeDisplay;
    public TimeModes currentTimeMode
    {
        get;
        private set;
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

    private List<TimeModeInformation> timeModes = new List<TimeModeInformation>();



    // Clock Destruction Variables
    [SerializeField]
    private Button destroyButton;
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


    // Monobehaviours
    void Start()
    {
        timeDisplayTimeMode.timeMode = timeDisplayTimeMode?.timeModeGameObject?.GetComponent<TimeDisplayTimeMode>();
        stopWatchTimeMode.timeMode = stopWatchTimeMode?.timeModeGameObject?.GetComponent<StopWatchTimeMode>();
        countDownTimeMode.timeMode = countDownTimeMode?.timeModeGameObject?.GetComponent<CountDownTimeMode>();

        if(timeDisplayTimeMode.timeMode != null)
        {
            timeDisplayTimeMode.timeMode.timeDisplayUpdate += new OnTimeUpdateEventHandler(UpdateTimeDisplay);
            timeModes.Add(timeDisplayTimeMode);
        }
        
        if(stopWatchTimeMode.timeMode != null)
        {
            stopWatchTimeMode.timeMode.timeDisplayUpdate += new OnTimeUpdateEventHandler(UpdateTimeDisplay);
            timeModes.Add(stopWatchTimeMode);
        }
        
        if(countDownTimeMode.timeMode != null)
        {
            countDownTimeMode.timeMode.timeDisplayUpdate += new OnTimeUpdateEventHandler(UpdateTimeDisplay);
            timeModes.Add(countDownTimeMode);
        }

        SelectTimeMode((int)startingTimeMode);
    }
    


    // Time Mode Functions 
    public void SelectTimeMode(int timeModeIndex) 
    {
        currentTimeMode = (TimeModes)timeModeIndex;

        for(int i = 0; i < timeModes.Count; i++) 
        {
            bool isSelectedMode = (i == timeModeIndex);
            timeModes[i].timeModeGameObject.SetActive(isSelectedMode);
        }
    }

    public void StartActiveTimeMode()
    {
        timeModes[(int)currentTimeMode].timeMode.StopTimeMode();
    }

    public void StopActiveTimeMode()
    {
        timeModes[(int)currentTimeMode].timeMode.StopTimeMode();
    }

    private void UpdateTimeDisplay(object sender, OnTimeUpdateEventArgs e)
    {
        timeModes[(int)currentTimeMode].text = e.time;
    }



    // Clock Destruction Methods
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
