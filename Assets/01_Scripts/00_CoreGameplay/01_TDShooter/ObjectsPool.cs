using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectsPool : SingletonManager<ObjectsPool>
{
    public GameObject PlayerBulletPrefab;
    public GameObject TurretBulletPrefab;
    public GameObject Enemy_1;
    public GameObject Enemy_2;
    public GameObject Enemy_3;
    
    Queue<GameObject> PlayerBulletsPool= new Queue<GameObject>();
    Queue<GameObject> TurretBulletsPool= new Queue<GameObject>();
    Queue<GameObject> EnemiesPool= new Queue<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        InitiatePools();
    }

    public void InitiatePools()
    {
        for (int i = 0; i < 10; i++)
        {
            StorePlayerBullet(Instantiate(PlayerBulletPrefab));
        }
        
        for (int i = 0; i < 10; i++)
        {
            AddRandomEnemiesToPool();
        }
    }

    public void AddRandomEnemiesToPool()
    {
        int ran = Random.Range(0, 3);
        switch (ran)
        {
            case 0:
                StoreEnemy(Instantiate(Enemy_1));
                break;
            
            case 1:
                StoreEnemy(Instantiate(Enemy_2));
                break;
            
            case 2:
                StoreEnemy(Instantiate(Enemy_3));
                break;
            
            default:
                break;
        }
    }
    
    


    public GameObject GetPlayerBullet()
    {
        GameObject temp = null;
        if (PlayerBulletsPool.Count != 0)
            temp = PlayerBulletsPool.Dequeue();
        else
        {
            temp = Instantiate(PlayerBulletPrefab, Vector3.zero, Quaternion.identity);
            temp.SetActive(false);
        }

        return temp;
    }

    public void StorePlayerBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        PlayerBulletsPool.Enqueue(bullet);
    }
    
    public GameObject GetTurretBullet()
    {
        GameObject temp = null;
        if (TurretBulletsPool.Count != 0)
            temp = TurretBulletsPool.Dequeue();
        else
        {
            temp = Instantiate(TurretBulletPrefab, Vector3.zero, Quaternion.identity);
            temp.SetActive(false);
        }

        return temp;
    }

    public void StoreTurretBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        TurretBulletsPool.Enqueue(bullet);
    }
    
    

    public GameObject GetEnemy()
    {
        GameObject temp = null;
        if (EnemiesPool.Count != 0)
            temp = EnemiesPool.Dequeue();
        else
        {
            AddRandomEnemiesToPool();
            temp = EnemiesPool.Dequeue();
        }
        return temp;
    }

    public void StoreEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        EnemiesPool.Enqueue(enemy);
    }


}
