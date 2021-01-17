using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManagerTest : MonoBehaviour
{   
    [SerializeField]
    private List<ClockTest> clocks;

    [SerializeField]
    private GameObject clockPrefab;

    [SerializeField]
    private GameObject scrollView;

    [SerializeField]
    private int minimumNumberOfClocks = 1;

    [SerializeField]
    private Transform clockSpawnPoint;

    [SerializeField]
    private float clockSpacing = 100f;

    [SerializeField]
    [Range(0, 50)]
    private float clockPadding = 20f;

    void Start() 
    {
        for(int i = clocks.Count; i < minimumNumberOfClocks; i++) 
        {
            SpawnClock();
        }
    }

    public void SpawnClock() 
    {
        Vector3 spawnPos = new Vector3(
            clockSpawnPoint.position.x,
            -((clockSpawnPoint.position.y + clockSpacing) * clocks.Count + (clockPadding * Mathf.Clamp01(clocks.Count))),
            Vector3.zero.z
        );

        GameObject obj = Instantiate(clockPrefab, spawnPos, Quaternion.identity, scrollView.transform);
        clocks.Add(obj.GetComponent<ClockTest>());
    }

    public void RemoveAllClocks()
    {
        List<ClockTest> objsToBeDeleted = clocks.GetRange(minimumNumberOfClocks, clocks.Count - minimumNumberOfClocks);
        clocks.RemoveRange(minimumNumberOfClocks, clocks.Count - minimumNumberOfClocks);

        for(int i = 0; i < objsToBeDeleted.Count; i++) 
        {
            Destroy(objsToBeDeleted[i].gameObject);
        }
    }
}
