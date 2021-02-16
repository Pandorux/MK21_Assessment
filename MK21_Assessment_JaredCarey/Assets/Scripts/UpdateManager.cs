using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Update(object sender);

public class UpdateManager : MonoBehaviour
{
    [HideInInspector]
    public static UpdateManager instance = null;

    [SerializeField]
    [Range(0, 5)]
    private float userInterfaceUpdateDelay = 0.3f;
    private float userInterfaceUpdateTimer;

    private bool canUserInterfaceBeUpdated
    {
        get
        {
            if(Time.time > userInterfaceUpdateTimer)
            {
                userInterfaceUpdateTimer = Time.time + userInterfaceUpdateDelay;
                return true;
            }

            return false;
        }
    }

    public event Update userInterfaceUpdate;
    public void UserInterfaceUpdated() 
    {
        if(userInterfaceUpdate != null)
            userInterfaceUpdate(this);
    }

    void Awake()
    {
        // Lazy Init for Singleton Pattern
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if(canUserInterfaceBeUpdated) 
        {
            UserInterfaceUpdated();
        }
    }

}
