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

        [SerializeField]
        private bool isTimeModeEditable;

        [SerializeField]
        private TextMeshProUGUI timeDisplay;

        [SerializeField]
        private TMP_InputField timeInput;

        public string text
        {
            get
            {
                if(isTimeModeEditable)
                    return timeInput.text;
                else
                    return timeDisplay.text;
            }

            set
            {
                if(isTimeModeEditable)
                    timeInput.text = value;
                else
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
        Debug.Log("ActiveTimeModeEdited Method Called");

        if(getActiveTimeMode.timeMode is ISettableTimeMode)
        {
            OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();
            e.time = "This is working";
            Debug.Log($"User Edit Update Test: {e.time}");
            
            ((ISettableTimeMode) getActiveTimeMode.timeMode).OnUserEdited(e);
        }
    }

    private void UpdateTimeDisplay(object sender, OnTimeUpdateEventArgs e)
    {
        timeModes[(int)currentTimeMode].text = e.time;
    }
}
