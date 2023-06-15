
using UnityEngine;

public class BuildGridManager : MonoBehaviour, IGameController
{
    public GameSystemsManager SystemsManager;

    public IslandController SelectedBuildedIsland;
    public BaseSlotController SelectedSlot;

    public void ControllerUpdate()
    {
        if (SystemsManager.SelectedIsland != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.TryGetComponent<BaseSlotController>(out BaseSlotController cont))
                {
                    if (SelectedSlot == cont) return;

                    ResetSelected();
                    SelectedSlot = cont;
                    SelectedSlot.SetSelected();
                    
                }

                else
                {
                    ResetSelected();
                }

            }

        }
        else
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.TryGetComponent<IslandController>(out IslandController isl))
                {
                    if (SelectedBuildedIsland == isl) return;

                    ResetSelected();
                    SelectedBuildedIsland = isl;
                    SelectedBuildedIsland.SetSelected();
                }
                else
                {
                    ResetSelected();
                }
            }


        }
    }

    private void OnEnable()
    {
        SystemsManager.OnDropSelection.AddListener(CreateIsland);
    }

    public void CreateIsland()
    {
        if (SelectedSlot != null)
        {
            if(!SelectedSlot.TrySetNewIsland(SystemsManager.SelectedIsland))
            {
                //error message
            }
            
        }
    }

    void ResetSelected()
    {
        if (SelectedBuildedIsland != null)
        {
            SelectedBuildedIsland.SetUnselected();
        }
        if (SelectedSlot != null)
        {
            SelectedSlot.SetUnselected();
        }
        SelectedSlot = null;
        SelectedBuildedIsland = null;
    }

}

public abstract class GridTile : MonoBehaviour
{
    public bool isCenter;
    public Outline outline;

    public virtual void OnEnable()
    {
        outline = GetComponent<Outline>();
    }

    public virtual void SetSelected()
    {
        if (isCenter) return;
        outline.enabled = true;
    }

    public virtual void SetUnselected()
    {
        if (isCenter) return;
        outline.enabled = false;
    }
}
