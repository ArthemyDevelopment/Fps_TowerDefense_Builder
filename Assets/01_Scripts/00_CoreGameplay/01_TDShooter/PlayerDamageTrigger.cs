using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDamageTrigger : MonoBehaviour
{
    public ShootingController PlayerController;
    public GameObject PlayerHitPrefab;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerHitPrefab.transform.position = other.transform.position;
            PlayerHitPrefab.SetActive(true);
            ObjectsPool.current.StoreEnemy(other.gameObject);
            GameSystemsManager.current.ShooterManager.EnemyRemoved(other.GetComponent<EnemyController>());
            PlayerController.CurrHealth--;
        }
    }
}
