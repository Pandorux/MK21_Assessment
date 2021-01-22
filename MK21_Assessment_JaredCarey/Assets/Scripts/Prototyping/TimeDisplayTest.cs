using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDisplayTest : MonoBehaviour
{

    public TextMeshProUGUI test;

    // Update is called once per frame
    void Update()
    {
        test.text = DateTime.Now.ToLongTimeString();
    }
}
