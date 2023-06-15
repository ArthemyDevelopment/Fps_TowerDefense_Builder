using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEventTrigger : MonoBehaviour
{
    public UnityEvent OnTrigger;

    public void TriggerEvent()
    {
        OnTrigger.Invoke();
    }
}
