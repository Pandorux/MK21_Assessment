using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

/// <summary>
/// Class for managing the UI that updates the Time Display
/// </summary>
public class TimeDisplaySettings : MonoBehaviour
{
    private readonly static string DayTimePeriod_AM = "AM";
    private readonly static string DayTimePeriod_PM = "PM";
    private readonly static Dictionary<string, int> DayTimePeriods = new Dictionary<string, int> {
        { DayTimePeriod_AM, 0 },
        { DayTimePeriod_PM, 1 }
    };

    // Variables
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

    private List<OptionDataKeyValue> timeFormatOptionDataKeyValues = new List<OptionDataKeyValue>();

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




    // Monobehaviours 
    void Start()
    {
        SetupTimeFormatOptionData();
        SetupSetTimeDropDownLists(getFormattedCurrentHour, DateTimeHelper.DoesTimeFormatShowAMAndPM(timeDisplayTimeMode.chosenTimeFormat));
    }

    void OnEnable()
    {
        UpdateSetTimeDisplay();
        UpdateTimeFormatDropDownList();
    }

    public void UpdateTimeDisplayTimeFormat()
    {
        timeDisplayTimeMode.chosenTimeFormat = timeFormatOptionDataKeyValues[timeFormatDropDown.value].key;
    }



    // UI Methods - Updating | Set Time 
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
        setHourDropDown.value = getFormattedCurrentHour;
        setMinuteDropDown.value = timeDisplayTimeMode.getCurrentTime.Minute;
        amPMDropDown.value = DateTimeHelper.IsItPM(timeDisplayTimeMode.getCurrentTime.Hour) ? DayTimePeriods[DayTimePeriod_PM] : DayTimePeriods[DayTimePeriod_AM];
    }




    // UI Methods - Updating | Time Format 
    private void UpdateTimeFormatDropDownList(object sender)
    {
        UpdateTimeFormatDropDownList();
    }

    private void UpdateTimeFormatDropDownList()
    {
        for(int i = 0; i < timeFormatOptionDataKeyValues.Count; i++)
        {
            timeFormatOptionDataKeyValues[i].text = timeDisplayTimeMode.getCurrentTime.ToString(timeFormatOptionDataKeyValues[i].key);
            timeFormatDropDown.options[i].text = timeFormatOptionDataKeyValues[i].text;
        }

        timeFormatDropDown.value = Array.FindIndex(TimeDisplayTimeMode.TimeFormats, timeFormat => timeFormat == timeDisplayTimeMode.chosenTimeFormat);
    }


    // UI Methods - Updating | Packages new time and sends it to Time Display Time Mode for consumption
    public void UpdateTime()
    {
        OnTimeUpdateEventArgs e = new OnTimeUpdateEventArgs();

        e.hours = setHourDropDown.value;
        e.minutes = setMinuteDropDown.value;
        e.seconds = DateTime.Now.Second; // The user cannot set seconds 

        timeDisplayTimeMode.OnUserEdited(e);
    }



    // UI Methods - Setup | Time Format
    private void SetupTimeFormatOptionData()
    {
        for(int i = 0; i < TimeDisplayTimeMode.TimeFormats.Length; i++)
        {
            string optText = timeDisplayTimeMode.getCurrentTime.ToString(TimeDisplayTimeMode.TimeFormats[i]);

            timeFormatOptionDataKeyValues.Add( 
                    new OptionDataKeyValue (
                TimeDisplayTimeMode.TimeFormats[i],
                timeDisplayTimeMode.getCurrentTime.ToString(TimeDisplayTimeMode.TimeFormats[i])
            ));
        }

        List<TMP_Dropdown.OptionData> timeFormatOptionData = timeFormatOptionDataKeyValues
            .Select(optDataKeyValue => optDataKeyValue.optionData)
            .ToList();   

        timeFormatDropDown.AddOptions(timeFormatOptionData);
    }



    // UI Methods - Setup | Set Time
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
    }
}
