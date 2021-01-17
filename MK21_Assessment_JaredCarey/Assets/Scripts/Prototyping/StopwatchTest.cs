using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class StopwatchTest : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    private Stopwatch stopWatch = new Stopwatch();

    // Update is called once per frame
    void Update()
    {
        if(timeDisplay) 
        {
            timeDisplay.text = stopWatch.Elapsed.ToString();
        }
    }

    public void StartStopWatch() 
    {
        stopWatch.Start();
    }

    public void StopStopWatch() 
    {
        stopWatch.Stop();
    }

    public void ResetStopWatch() 
    {
        stopWatch.Reset();
    }
}
