using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IslandController : GridTile
{

    public IslandObjectSO IslandStats;
    public BaseSlotController BaseBelonging;

    public List<IslandController>ConectedIslands = new List<IslandController>();

    public override void SetSelected()
    {
        base.SetSelected();
        if (isCenter) return;
        GameSystemsManager.current.editIslandCanvas.SetEditCanvas(this);
    }

    public override void SetUnselected()
    {
        base.SetUnselected();
    }

    private void Update()
    {
        if (ConectedIslands.Count == 0)
        {
            if (GameSystemsManager.current.IsolatedIslands.Contains(this)) return;
            
            GameSystemsManager.current.IsolatedIslands.Add(this);
        }
        else
        {
            if (!GameSystemsManager.current.IsolatedIslands.Contains(this)) return;

            GameSystemsManager.current.IsolatedIslands.Remove(this);
        }
    }

   
    

    private void OnDestroy()
    {
        GameSystemsManager.current.IsolatedIslands.Remove(this);
        GameSystemsManager.current.Currency += IslandStats.Cost;
    }

    public IslandController GetNextDestination(IslandController islandController)
    {

        IslandController tempIsland = null;
        List<IslandController> tempList = ConectedIslands;
        tempList.Remove(islandController);
        int tempRan = Random.Range(0, tempList.Count);
        tempIsland = tempList[tempRan];

        return tempIsland;
    }
}
