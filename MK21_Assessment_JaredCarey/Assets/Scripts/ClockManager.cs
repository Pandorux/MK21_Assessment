using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    [SerializeField]
    private List<TimeModeManager> clocks;

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

    public void SpawnClock() 
    {
        GameObject obj = Instantiate(clockTemplate, scrollView.transform);
        obj.SetActive(true);
        obj.name = $"Clock {clocks.Count}";

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
