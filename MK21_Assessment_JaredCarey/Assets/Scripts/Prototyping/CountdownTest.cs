using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class CountdownTest : MonoBehaviour
{
    public TMP_InputField countdownTextInput;
    private Stopwatch timeElapsed = new Stopwatch();
    private float countdownStartTime = 0;
    private bool isCountingDown;

    // Update is called once per frame
    void Update()
    {
        if(isCountingDown) {
            TimeSpan ts = timeElapsed.Elapsed;
            float timeRemaining = Mathf.Clamp((countdownStartTime - ts.Seconds), 0, Mathf.Infinity);
            countdownTextInput.text = timeRemaining.ToString();
        }
    }

    public void StartCountDown() 
    {
        isCountingDown = true;
        timeElapsed.Start();
    }

    public void StopCountDown() 
    {
        isCountingDown = false;
        timeElapsed.Stop();
    }

    public void ResetCountDown() 
    {
        isCountingDown = false;
        timeElapsed.Reset();
        countdownTextInput.text = countdownStartTime.ToString();
    }

    public void SetTime() 
    {
        countdownStartTime = int.Parse(countdownTextInput.text);
    }
}
