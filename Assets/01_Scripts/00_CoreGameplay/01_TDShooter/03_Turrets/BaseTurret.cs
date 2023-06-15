using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    public Transform _target;

    public Transform Target
    {
        get => _target;
        set
        {
            if (value == _target) return;
            if (value!=null) ActivateTurret.Invoke(); 
            else if(value==null)DeactivateTurret.Invoke();
            _target = value;
        }
    }
    public float Range;
    public Color GizmoColor;

    public delegate void TurretState();

    public TurretState ActivateTurret;
    public TurretState DeactivateTurret;



    private void Update()
    {
        Target = GameSystemsManager.current.ShooterManager.GetClosetsEnemy(this.transform,Range);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}

