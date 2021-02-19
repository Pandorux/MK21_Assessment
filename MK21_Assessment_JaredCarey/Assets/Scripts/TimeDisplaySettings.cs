using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;


public class TimeDisplaySettings : MonoBehaviour, IMonoBehaviourEventSubscriptions
{
    private readonly static string DayTimePeriod_AM = "AM";
    private readonly static string DayTimePeriod_PM = "PM";
    private readonly static Dictionary<string, int> DayTimePeriods = new Dictionary<string, int> {
        { DayTimePeriod_AM, 0 },
        { DayTimePeriod_PM, 1 }
    };

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

    int getFormattedCurrentHour {
        get 
        {
            if(DateTimeHelper.IsItPM(timeDisplayTimeMode.getCurrentTime.Hour) && DateTimeHelper.DoesTimeFormatShowAMAndPM(timeDisplayTimeMode.chosenTimeFormat))
            {
                return DateTimeHelper.ConvertHoursTo12HourClock(timeDisplayTimeMode.getCurrentTime.Hour);
            }

            return timeDisplayTimeMode.getCurrentTime.Hour;
        }
    }

    void Start()
    {
        SetupTimeFormatOptionData();
        SetupSetTimeDropDownLists(getFormattedCurrentHour, DateTimeHelper.DoesTimeFormatShowAMAndPM(timeDisplayTimeMode.chosenTimeFormat));
    }



    // Monobehaviour Events 
    void OnEnable()
    {
        SubscribeEvents();
        UpdateSetTimeDisplay();
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


    // UI Shenanigans
    private void UpdateSetTimeDisplay(object sender)
    {
        UpdateSetTimeDisplay();
    }

    public void UpdateSetTimeDisplay()
    {
        // Obtain Info that impacts how options are displayed
        bool showAmPm = DateTimeHelper.DoesTimeFormatShowAMAndPM(timeDisplayTimeMode.chosenTimeFormat);
        int totalHours = showAmPm ? DateTimeHelper.ConvertHoursTo12HourClock(DateTime.MaxValue.Hour) : DateTime.MaxValue.Hour;

        // Reset Displayed Options
        setHourDropDown.ClearOptions();
        setMinuteDropDown.ClearOptions();
        SetupSetTimeDropDownLists(totalHours, showAmPm);

        // Update Default Value
        //setHourDropDown.value = getFormattedCurrentHour;
        //setMinuteDropDown.value = timeDisplayTimeMode.getCurrentTime.Minute;
        //amPMDropDown.value = DateTimeHelper.IsItPM(timeDisplayTimeMode.getCurrentTime.Hour) ? DayTimePeriods[DayTimePeriod_PM] : DayTimePeriods[DayTimePeriod_AM];
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

    private void SetupSetTimeDropDownLists(int totalHours, bool showAmAndPm)
    {
        List<TMP_Dropdown.OptionData> hoursOptData = new List<TMP_Dropdown.OptionData>();
        for(int i = 0; i <= totalHours; i++)
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

        amPMDropDown.gameObject.SetActive(showAmAndPm);
        //amPMDropDown.gameObject.SetActive(DateTimeHelper.DoesTimeFormatShowAMAndPM(timeDisplayTimeMode.chosenTimeFormat));
    }
}
