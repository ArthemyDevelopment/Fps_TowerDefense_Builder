using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public AILerp pathfind;
    public float BaseSpeed;
    public int MaxHealth;
    private float currHealth;
    public Image HealthBar;
    public int CurrencyValue;
    
    private bool canReciveAreaDamage=true;
    
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            currHealth-=other.GetComponent<BulletController>().Damage;
            CheckHealth();
        }
    }

    void CheckHealth()
    {
        if (currHealth <= 0)
        {
            GameSystemsManager.current.Currency += CurrencyValue;
            GameSystemsManager.current.ShooterManager.EnemyRemoved(this);
            ObjectsPool.current.StoreEnemy(gameObject);
        }
        SetHealthBar();
    }

    void SetHealthBar()
    {
        HealthBar.fillAmount = ScriptsTools.MapValues(currHealth, 0, MaxHealth, 0, 1);
    }


    public void Reset()
    {
        currHealth = MaxHealth;
        SetHealthBar();
        gameObject.SetActive(true);
        pathfind.speed = BaseSpeed;
        GameSystemsManager.current.ShooterManager.ActiveEnemiesList.Add(this);
    }


    public void AreaDamage(float damage, float time)
    {
        if (!canReciveAreaDamage) return;
        currHealth -= damage;
        CheckHealth();
        canReciveAreaDamage = false;
        StartCoroutine(WaitForAreaDamage(time));
    }

    IEnumerator WaitForAreaDamage(float time)
    {
        yield return ScriptsTools.GetWait(time);
        canReciveAreaDamage = true;
    }
    
}
