using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// A simple interface that has methods meant for containing event subscriptions for Monobehaviours such as
///
/// - OnDestroy
/// - OnEnable
/// - OnDisable
///<summary>
public interface IMonoBehaviourEventSubscriptions
{
    void SubscribeEvents();
    void UnsubscribeEvents();
}
