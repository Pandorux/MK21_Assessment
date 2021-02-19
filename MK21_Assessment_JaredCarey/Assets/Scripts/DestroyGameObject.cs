using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple component for destroying game objects with a UI event
/// <summary>
public class DestroyGameObject : MonoBehaviour
{
    public GameObject gameObjToBeDestroyed;

    public void DestroyProvidedGameObject()
    {
        Destroy(gameObjToBeDestroyed);
    }

}
