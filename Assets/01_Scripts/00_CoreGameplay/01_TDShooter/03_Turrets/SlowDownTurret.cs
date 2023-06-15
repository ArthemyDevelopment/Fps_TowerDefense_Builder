using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class SlowDownTurret : MonoBehaviour
{
    public float SlowDownRatio=0.5f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<AILerp>().speed *= SlowDownRatio;
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
            other.GetComponent<AILerp>().speed /= SlowDownRatio;
    }
}
