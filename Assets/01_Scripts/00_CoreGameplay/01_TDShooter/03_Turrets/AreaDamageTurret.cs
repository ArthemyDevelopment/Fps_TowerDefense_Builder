using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageTurret : MonoBehaviour
{

    public float Damage;
    public float WaitTime;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().AreaDamage(Damage,WaitTime);
        }
    }
}
