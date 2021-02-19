using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class that manages the Countdown Time Mode UI and update the data within Countdown Time Mode UI
/// <summary>
public class CountdownSettings : MonoBehaviour
{
    // Variables
    [SerializeField]
    private CountDownTimeMode countdownTimeMode;

    [SerializeField]
    private TMP_Dropdown setHourDropDown;

    [SerializeField]
    private TMP_Dropdown setMinuteDropDown;

    [SerializeField]
    private TMP_Dropdown setSecondDropDown;



    // Methods
    void Start()
    {
        SetupTimeFormatOptionData();
    }

    public void UpdateTime()
    {
        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();

        e.hours = setHourDropDown.value;
        e.minutes = setMinuteDropDown.value;
        e.seconds = setSecondDropDown.value;

        countdownTimeMode.OnUserEdited(e);
    }

    private void SetupTimeFormatOptionData()
    {
        // Create Dropdown Options - Hours
        List<TMP_Dropdown.OptionData> hoursOptData = new List<TMP_Dropdown.OptionData>();
        for(int i = 0; i <= DateTime.MaxValue.Hour; i++)
        {
            string text = i < 10 ? "0" + i.ToString() : i.ToString();
            hoursOptData.Add(new TMP_Dropdown.OptionData(text));
        }

        // Create Dropdown Options - Minutes
        List<TMP_Dropdown.OptionData> minutesOptData = new List<TMP_Dropdown.OptionData>();
        for(int i = 0; i <= DateTime.MaxValue.Minute; i++)
        {
            string text = i < 10 ? "0" + i.ToString() : i.ToString();
            minutesOptData.Add(new TMP_Dropdown.OptionData(text));
        }

        // Create Dropdown Options - Seconds
        List<TMP_Dropdown.OptionData> secondsOptData = new List<TMP_Dropdown.OptionData>();
        for(int i = 0; i <= DateTime.MaxValue.Second; i++)
        {
            string text = i < 10 ? "0" + i.ToString() : i.ToString();
            secondsOptData.Add(new TMP_Dropdown.OptionData(text));
        }

        // Add all of the newly created dropdown options to their respective dropdowns
        setHourDropDown.AddOptions(hoursOptData);
        setMinuteDropDown.AddOptions(minutesOptData);
        setSecondDropDown.AddOptions(secondsOptData);
    }
}
