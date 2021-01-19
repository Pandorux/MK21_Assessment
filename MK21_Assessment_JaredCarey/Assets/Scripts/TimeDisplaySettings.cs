using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;


public class TimeDisplaySettings : MonoBehaviour
{
    [SerializeField]
    private TimeDisplayTimeMode timeDisplayTimeMode;

    [SerializeField]
    private TMP_Dropdown timeFormatDropDown;

    private OptionDataKeyValue[] timeFormatOptionDataKeyValues;

    void Start()
    {
        SetupTimeFormatOptionData();
    }

    void Update()
    {
        UpdateTimeFormatDropDownList();
    }

    public void UpdateTimeDisplayTimeFormat()
    {
        timeDisplayTimeMode.chosenTimeFormat = timeFormatOptionDataKeyValues[timeFormatDropDown.value].key;
    }

    private void UpdateTimeFormatDropDownList()
    {
        for(int i = 0; i < timeFormatOptionDataKeyValues.Length; i++)
        {
            timeFormatOptionDataKeyValues[i].text = timeDisplayTimeMode.getCurrentTime.ToString(timeFormatOptionDataKeyValues[i].key);
            timeFormatDropDown.options[i].text = timeFormatOptionDataKeyValues[i].text;
        }
    }

    private void SetupTimeFormatOptionData()
    {
        timeFormatOptionDataKeyValues = new OptionDataKeyValue[TimeDisplayTimeMode.TimeFormats.Length];

        for(int i = 0; i < timeFormatOptionDataKeyValues.Length; i++)
        {
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
}
