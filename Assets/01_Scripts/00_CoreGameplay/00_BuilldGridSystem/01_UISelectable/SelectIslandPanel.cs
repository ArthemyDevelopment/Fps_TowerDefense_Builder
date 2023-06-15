using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectIslandPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UICreateManager controller;
    
    public IslandObjectSO thisIsland;

    public TMP_Text Name;
    public TMP_Text ShortDesc;
    public TMP_Text LongDesc;
    public Image Preview;
    public TMP_Text cost;

    private void OnEnable()
    {
        if(thisIsland!=null)
            ConfigurePanel();
    }

    void ConfigurePanel()
    {
        Name.text = thisIsland.IslandName;
        ShortDesc.text= thisIsland.IslandShortDesc;
        LongDesc.text= thisIsland.IslandLongDesc;
        Preview.sprite = thisIsland.IslandPreview;
        cost.text = thisIsland.Cost.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        controller.SelectIslandToBuild(thisIsland);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        controller.UnSelectIsland();
    }
}
