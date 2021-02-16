using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownSettings : MonoBehaviour
{
    [SerializeField]
    private CountDownTimeMode countdownTimeMode;

    [SerializeField]
    private TMP_Dropdown setHourDropDown;

    [SerializeField]
    private TMP_Dropdown setMinuteDropDown;

    [SerializeField]
    private TMP_Dropdown setSecondDropDown;

    void Start()
    {
        SetupTimeFormatOptionData();
    }

    public void UpdateTime(object sender)
    {
        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();

        e.hours = setHourDropDown.value;
        e.minutes = setMinuteDropDown.value;
        e.seconds = setSecondDropDown.value;

        countdownTimeMode.OnUserEdited(e);
    }

    private void SetupTimeFormatOptionData()
    {
        List<TMP_Dropdown.OptionData> hoursOptData = new List<TMP_Dropdown.OptionData>();
        for(int i = 0; i <= DateTime.MaxValue.Hour; i++)
        {
            string text = i < 10 ? "0" + i.ToString() : i.ToString();
            hoursOptData.Add(new TMP_Dropdown.OptionData(text));
        }

        List<TMP_Dropdown.OptionData> minutesOptData = new List<TMP_Dropdown.OptionData>();
        for(int i = 0; i <= DateTime.MaxValue.Minute; i++)
        {
            string text = i < 10 ? "0" + i.ToString() : i.ToString();
            minutesOptData.Add(new TMP_Dropdown.OptionData(text));
        }

        List<TMP_Dropdown.OptionData> secondsOptData = new List<TMP_Dropdown.OptionData>();
        for(int i = 0; i <= DateTime.MaxValue.Second; i++)
        {
            string text = i < 10 ? "0" + i.ToString() : i.ToString();
            secondsOptData.Add(new TMP_Dropdown.OptionData(text));
        }

        setHourDropDown.AddOptions(hoursOptData);
        setMinuteDropDown.AddOptions(minutesOptData);
        setSecondDropDown.AddOptions(secondsOptData);
    }
}
