using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCast : MonoBehaviour
{
    public GameObject Shadow;
    
    // Update is called once per frame
    void Update()
    {
        Cast();
    }
    
    
    void Cast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 15))
        {
            if(!Shadow.activeSelf)
                Shadow.SetActive(true);
            
            Shadow.transform.position = hit.point +(Vector3.up * 0.02f);
        }
        else if(Shadow.activeSelf)
            Shadow.SetActive(false);
        
    }
}
