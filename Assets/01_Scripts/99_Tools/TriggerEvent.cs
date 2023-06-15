using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Generic trigger events, allows to easy trigger detection for generic purposes without extra coding. 
/// </summary>
public class TriggerEvent : MonoBehaviour
{

    [TagSelector]public string TargetTag;
    
    public UnityEvent OnEnter;
    public UnityEvent OnStay;
    public UnityEvent OnExit;


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(TargetTag))
        {
            OnEnter.Invoke();
            //Debug.Log( col.gameObject.name+ " enter into " + gameObject.name, col.gameObject);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag(TargetTag))
        {
            OnExit.Invoke();
            //Debug.Log( col.gameObject.name+ " exit from " + gameObject.name, col.gameObject);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag(TargetTag))
        {
            OnStay.Invoke();
        }
    }
}
