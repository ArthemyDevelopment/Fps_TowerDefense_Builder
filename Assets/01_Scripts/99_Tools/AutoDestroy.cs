using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float destroytime;
    
     void OnEnable()
     {
         StartCoroutine(timeToDestroy());
     }

     IEnumerator timeToDestroy()
     {
         yield return ScriptsTools.GetWait(destroytime);
         gameObject.SetActive(false);
     }
}
