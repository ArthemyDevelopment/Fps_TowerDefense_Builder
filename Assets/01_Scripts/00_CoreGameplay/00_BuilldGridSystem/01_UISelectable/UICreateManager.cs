using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UICreateManager : MonoBehaviour, IGameController
{
    public GameSystemsManager SystemManager;
    public SelectIconController Icon;
    public ScrollRect ScrollView;
    
    
    public void ControllerUpdate()
    {
        
    }


    public void SelectIslandToBuild(IslandObjectSO island)
    {
        SystemManager.SelectedIsland = island;
        Icon.SetIcon(island);
        ScrollView.enabled = false;
    }

    public void UnSelectIsland()
    {
        Icon.TurnOffIcon();
        ScrollView.enabled = true;
        SystemManager.OnDropSelection.Invoke();
        SystemManager.SelectedIsland = null;
    }
    

}
