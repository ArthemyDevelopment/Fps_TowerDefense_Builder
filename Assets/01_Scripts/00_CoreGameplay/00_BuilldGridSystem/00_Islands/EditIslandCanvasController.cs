using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditIslandCanvasController : MonoBehaviour
{
    public GameObject Canvas;
    public IslandController SelectedIsland;
    private IslandController temp;
    
    public void SetEditCanvas(IslandController island)
    {
        if (island == SelectedIsland) return;
        gameObject.transform.position = island.transform.position;
        ActiveCanvas();
        SelectedIsland = island;
    }

    private void Update()
    {
        if (GameSystemsManager.current.GridManager.SelectedBuildedIsland != SelectedIsland)
        {
            if (SelectedIsland == null) return;
            DeactiveCanvas();
            SelectedIsland = null;
        }
    }

    public void ActiveCanvas()
    {
        Canvas.SetActive(true);

    }
    
    public void DeactiveCanvas()
    {
        Canvas.SetActive(false);

    }

    public void RotateIslandLeft()
    {
        SelectedIsland.transform.rotation = Quaternion.Euler(SelectedIsland.transform.rotation.eulerAngles - Vector3.up*60);
    }
    public void RotateIslandRight()
    {
        SelectedIsland.transform.rotation = Quaternion.Euler(SelectedIsland.transform.rotation.eulerAngles+Vector3.up * 60);
        
    }

    public void DeleteIsland()
    {
        SelectedIsland.transform.position-= Vector3.down*100;
        DeactiveCanvas();
        temp = SelectedIsland;
        SelectedIsland.BaseBelonging.ChangeNextSlots(false);
        GameSystemsManager.current.GridManager.SelectedBuildedIsland = null;
        SelectedIsland = null;
        StartCoroutine(DestroyIslandCoroutine());
    }

    IEnumerator DestroyIslandCoroutine()
    {
        yield return ScriptsTools.GetWait(0.1f);
        Destroy(temp.gameObject);
        temp = null;
    }
}
