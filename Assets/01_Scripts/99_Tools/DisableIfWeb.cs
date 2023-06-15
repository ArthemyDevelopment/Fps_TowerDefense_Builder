using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfWeb : MonoBehaviour
{
    private void OnEnable()
    {
        #if UNITY_WEBGL
        gameObject.SetActive(false);
        #endif
    }
}
