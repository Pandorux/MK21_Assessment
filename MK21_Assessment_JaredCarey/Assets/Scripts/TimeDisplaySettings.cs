using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;


public class TimeDisplaySettings : MonoBehaviour, IMonoBehaviourEventSubscriptions
{
    [SerializeField]
    private TimeDisplayTimeMode timeDisplayTimeMode;

    [SerializeField]
    private TMP_Dropdown timeFormatDropDown;

    [SerializeField]
    private TMP_Dropdown setHourDropDown;

    [SerializeField]
    private TMP_Dropdown setMinuteDropDown;

    [SerializeField]
    private TMP_Dropdown amPMDropDown;

    private OptionDataKeyValue[] timeFormatOptionDataKeyValues;

    void Start()
    {
        SetupTimeFormatOptionData();
        SetupSetTimeDropDownLists();
    }

    void OnEnable()
    {
        SubscribeEvents();
    }

    void OnDisable()
    {
        UnsubscribeEvents();
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    public void SubscribeEvents()
    {
        UpdateManager.instance.userInterfaceUpdate += new Update(UpdateTimeFormatDropDownList);
    }

    public void UnsubscribeEvents()
    {
        UpdateManager.instance.userInterfaceUpdate -= new Update(UpdateTimeFormatDropDownList);
    }

    public void UpdateTimeDisplayTimeFormat()
    {
        timeDisplayTimeMode.chosenTimeFormat = timeFormatOptionDataKeyValues[timeFormatDropDown.value].key;
    }

    public void UpdateSetTimeDisplay()
    {
        bool showAmPm = DateTimeHelper.DoesTimeFormatShowAMAndPM(timeDisplayTimeMode.chosenTimeFormat);
        amPMDropDown.gameObject.SetActive(showAmPm);
        
        setHourDropDown.value = showAmPm ? 12 : timeDisplayTimeMode.getCurrentTime.Hour;
        setMinuteDropDown.value = timeDisplayTimeMode.getCurrentTime.Minute;
    }

    public void UpdateTime()
    {
        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();

        e.hours = setHourDropDown.value;
        e.minutes = setMinuteDropDown.value;
        e.seconds = DateTime.Now.Second; // The user cannot set seconds 

        timeDisplayTimeMode.OnUserEdited(e);
    }

    private void UpdateTimeFormatDropDownList(object sender)
    {
        UpdateTimeFormatDropDownList();
    }

    private void UpdateTimeFormatDropDownList()
    {
        for(int i = 0; i < timeFormatOptionDataKeyValues.Length; i++)
        {
            timeFormatOptionDataKeyValues[i].text = timeDisplayTimeMode.getCurrentTime.ToString(timeFormatOptionDataKeyValues[i].key);
            timeFormatDropDown.options[i].text = timeFormatOptionDataKeyValues[i].text;
        }

        timeFormatDropDown.value = Array.FindIndex(TimeDisplayTimeMode.TimeFormats, timeFormat => timeFormat == timeDisplayTimeMode.chosenTimeFormat);
    }

    private void SetupTimeFormatOptionData()
    {
        timeFormatOptionDataKeyValues = new OptionDataKeyValue[TimeDisplayTimeMode.TimeFormats.Length];

        for(int i = 0; i < timeFormatOptionDataKeyValues.Length; i++)
        {
            string optText = timeDisplayTimeMode.getCurrentTime.ToString(TimeDisplayTimeMode.TimeFormats[i]);

            timeFormatOptionDataKeyValues[i] = new OptionDataKeyValue (
                TimeDisplayTimeMode.TimeFormats[i],
                timeDisplayTimeMode.getCurrentTime.ToString(TimeDisplayTimeMode.TimeFormats[i])
            );
        }

        List<TMP_Dropdown.OptionData> timeFormatOptionData = timeFormatOptionDataKeyValues
            .Select(optDataKeyValue => optDataKeyValue.optionData)
            .ToList();   

        timeFormatDropDown.AddOptions(timeFormatOptionData);
    }

    private void SetupSetTimeDropDownLists()
    {
        List<TMP_Dropdown.OptionData> hoursOptData = new List<TMP_Dropdown.OptionData>();
        for(int i = 0; i <= DateTime.MaxValue.Hour; i++)
        {
            hoursOptData.Add(new TMP_Dropdown.OptionData(i.ToString()));
        }

        List<TMP_Dropdown.OptionData> minutesOptData = new List<TMP_Dropdown.OptionData>();
        for(int i = 0; i <= DateTime.MaxValue.Minute; i++)
        {
            minutesOptData.Add(new TMP_Dropdown.OptionData(i.ToString()));
        }

        setHourDropDown.AddOptions(hoursOptData);
        setMinuteDropDown.AddOptions(minutesOptData);

        //amPMDropDown.gameObject.SetActive(DateTimeHelper.DoesTimeFormatShowAMAndPM(timeDisplayTimeMode.chosenTimeFormat));
    }
}
