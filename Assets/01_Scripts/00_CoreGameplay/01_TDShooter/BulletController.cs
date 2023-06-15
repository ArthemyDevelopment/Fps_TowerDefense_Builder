using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float Speed;
    public float MaxTravel;
    public float Damage;
    public Rigidbody rb;
    private Vector3 StartPosition;


    public enum BulletType
    {
        Player,
        Turret,
    }
    public BulletType type;

    private void OnEnable()
    {
        StartPosition = transform.position;
        rb.velocity= transform.forward*Speed;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, StartPosition) >= MaxTravel)
        {
            switch (type)
            {
                case BulletType.Player:
                    ObjectsPool.current.StorePlayerBullet(this.gameObject);
                    break;
                case BulletType.Turret:
                    ObjectsPool.current.StoreTurretBullet(this.gameObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.isTrigger)
            switch (type)
            {
                case BulletType.Player:
                    ObjectsPool.current.StorePlayerBullet(this.gameObject);
                    break;
                case BulletType.Turret:
                    ObjectsPool.current.StoreTurretBullet(this.gameObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
    }
}
