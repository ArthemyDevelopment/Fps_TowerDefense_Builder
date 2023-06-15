using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSlotController : GridTile
{
    
    public List<GameObject> NextBaseSlots = new List<GameObject>();

    int _adjacentIslands;
    public int AdjacentIslands
    {
        get => _adjacentIslands;
        set
        {
            _adjacentIslands = value;
            if(_adjacentIslands==0) gameObject.SetActive(false);
            else gameObject.SetActive(true);
        }
    }
    
    
    public override void SetSelected()
    {
        base.SetSelected();
    }

    public override void SetUnselected()
    {
        base.SetUnselected();
    }

    public bool TrySetNewIsland(IslandObjectSO island)
    {
        if (GameSystemsManager.current.Currency < island.Cost)
            return false;

        GameObject temp = Instantiate(island.IslandPrefab, transform.position, Quaternion.identity);
        IslandController isCon = temp.GetComponent<IslandController>();
        isCon.BaseBelonging = this;
        GameSystemsManager.current.editIslandCanvas.SetEditCanvas(isCon);
        ChangeNextSlots(true);
        GameSystemsManager.current.Currency -= island.Cost;
        return true;
        
    }

    public void ChangeNextSlots(bool b)
    {
        foreach (var slot in NextBaseSlots)
        {
            switch (b)
            {
                case true:
                    slot.GetComponent<BaseSlotController>().AdjacentIslands++;
                    break;
                case false:
                    slot.GetComponent<BaseSlotController>().AdjacentIslands--;
                    break;
            }
        }
    }

}
