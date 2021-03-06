﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class that manages the creation and delection of all of the clocks through the TimeModeManager class
/// <summary>
public class ClockManager : MonoBehaviour
{
    [SerializeField]
    private List<TimeModeManager> clocks;

    // NOTE: the clock template in the scene may not have information that a prefab could possibly have
    //       i.e. clock UI could use the remove clock method that would not be accesible in a clock prefab
    [SerializeField]
    private GameObject clockTemplate;

    [SerializeField]
    private GameObject scrollView;

    [SerializeField]
    private int minimumNumberOfClocks = 1;

    void Start() 
    {
        for(int i = clocks.Count; i < minimumNumberOfClocks; i++) 
        {
            SpawnClock();
        }

        if(clocks.Count > minimumNumberOfClocks)
        {
            UnprotectClocksFromUserDestruction();
        }
        else
        {
            ProtectClocksFromUserDestruction();
        }
    }



    // Clock creation and deletion methods
    public void SpawnClock() 
    {
        GameObject obj = Instantiate(clockTemplate, scrollView.transform);
        obj.SetActive(true);
        obj.name = $"Clock {clocks.Count}";

        // TODO: Do I need to make this null safe? I believe this failing should be desired behaviour for development
        //       as it is a clear indication that something is wrong with the setup of the scene

        //       NOTE: I am not changing the template object setup due to the lack of time, and it will require many
        //             additional changes throughout the project base. Please see report for more information.
        clocks.Add(obj.GetComponent<TimeModeManager>());

        if(clocks.Count > minimumNumberOfClocks)
        {
            UnprotectClocksFromUserDestruction();
        }
    }

    public void RemoveAllClocks()
    {
        List<TimeModeManager> objsToBeDeleted = clocks.GetRange(minimumNumberOfClocks, clocks.Count - minimumNumberOfClocks);
        clocks.RemoveRange(minimumNumberOfClocks, clocks.Count - minimumNumberOfClocks);

        for(int i = 0; i < objsToBeDeleted.Count; i++) 
        {
            Destroy(objsToBeDeleted[i].gameObject);
        }

        ProtectClocksFromUserDestruction();
    }

    private void ProtectClocksFromUserDestruction()
    {
        for(int i = 0; i < clocks.Count; i++)
        {
            clocks[i].UserCannotDestroy();
        }
    }

    private void UnprotectClocksFromUserDestruction()
    {
        for(int i = 0; i < clocks.Count; i++)
        {
            clocks[i].UserCanDestroy();
        }
    }

    public void RemoveClock(TimeModeManager manager)
    {
        clocks.Remove(manager);

        if(clocks.Count > minimumNumberOfClocks) 
        {
            UnprotectClocksFromUserDestruction();
        }
        else
        {
            ProtectClocksFromUserDestruction();
        }
    }
}
