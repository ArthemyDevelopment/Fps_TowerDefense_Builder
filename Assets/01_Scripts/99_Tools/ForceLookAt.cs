using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceLookAt : MonoBehaviour
{
    public Transform Target;

    private void OnEnable()
    {
        Target = GameSystemsManager.current.mainCamera.transform;
    }

    private void Update()
    {
        transform.LookAt(Target);
    }
}
