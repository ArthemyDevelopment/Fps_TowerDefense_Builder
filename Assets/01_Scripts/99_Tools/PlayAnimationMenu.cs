using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationMenu : MonoBehaviour
{
    private Animator anim;


    private void OnEnable()
    {
        anim = GetComponent<Animator>();
    }


    public void PlayAnimation(string s)
    {
        anim.Play(s,0);
    }
    
}
