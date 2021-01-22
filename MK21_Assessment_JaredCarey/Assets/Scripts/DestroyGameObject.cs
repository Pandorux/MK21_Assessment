using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public GameObject gameObjToBeDestroyed;

    public void DestroyProvidedGameObject()
    {
        Destroy(gameObjToBeDestroyed);
    }

}
