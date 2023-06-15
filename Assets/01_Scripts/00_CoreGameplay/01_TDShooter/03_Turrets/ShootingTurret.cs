using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTurret : MonoBehaviour
{
    public BaseTurret TurretController;
    public float FireRate;
    public float Damage;
    public Transform ShootingPoint;
    private bool TurretShooting;
    private Coroutine shootingCoroutine;
    public Transform PivoteGun;

    private void OnEnable()
    {
        TurretController.ActivateTurret += OnTurretOn;
        TurretController.DeactivateTurret += OnTurretOff;
    }

    private void Update()
    {
        if(TurretController.Target!=null)
            PivoteGun.rotation= Quaternion.Euler(PivoteGun.rotation.eulerAngles.x,ScriptsTools.GetRotation(PivoteGun, TurretController.Target),PivoteGun.rotation.eulerAngles.z);
    }

    public void OnTurretOn()
    {
        TurretShooting = true;
        if(shootingCoroutine==null)
            shootingCoroutine = StartCoroutine(ShootingCoroutine());
    }
    
    public void OnTurretOff()
    {
        TurretShooting = false;
        StopCoroutine(shootingCoroutine);
        shootingCoroutine = null;
    }

    IEnumerator ShootingCoroutine()
    {
        while (TurretShooting)
        {
            GameObject Bullet = null;
            BulletController tempController = null;
            Bullet = ObjectsPool.current.GetTurretBullet();
            tempController = Bullet.GetComponent<BulletController>();
            Bullet.transform.position = ShootingPoint.position;
            Bullet.transform.LookAt(TurretController.Target);
            tempController.Damage = Damage;
            Bullet.SetActive(true);
            yield return ScriptsTools.GetWait(FireRate);
        }
    }
}
