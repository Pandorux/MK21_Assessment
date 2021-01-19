using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDisplaySettings : MonoBehaviour
{
    [SerializeField]
    private TimeDisplayTimeMode timeDisplayTimeMode;

    [SerializeField]
    private TMP_Dropdown timeFormatDropDown;

    private OptionDataKeyValue[] timeFormatOptionData;

    void Start()
    {
        SetupTimeFormatOptionData();
    }

    private void UpdateTimeFormatDropDownList()
    {
        for(int i = 0; i < timeFormatOptionData.Length; i++)
        {
            timeFormatOptionData[i].text = timeDisplayTimeMode.getCurrentTime.ToString(timeFormatOptionData[i].key);
        }
    }

    private void SetupTimeFormatOptionData()
    {
        timeFormatOptionData = new OptionDataKeyValue[TimeDisplayTimeMode.TimeFormats.Length];

        for(int i = 0; i < timeFormatOptionData.Length; i++)
        {
            timeFormatOptionData[i] = new OptionDataKeyValue (
                TimeDisplayTimeMode.TimeFormats[i],
                timeDisplayTimeMode.getCurrentTime.ToString(TimeDisplayTimeMode.TimeFormats[i])
            );
        }
    }
}
