using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectIconController : MonoBehaviour
{
    public Image IconImage;

    private void OnEnable()
    {
        TurnOffIcon();
    }

    public void SetIcon(IslandObjectSO island)
    {
        IconImage.sprite = island.IslandPreview;
        IconImage.enabled = true;
    }

    public void TurnOffIcon()
    {
        IconImage.enabled = false;
    }
    
    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
