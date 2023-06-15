
using UnityEngine;


public class CheckPathDetectionController : MonoBehaviour
{
    public IslandController belogingIsland;
    private Collider thisCollider;
    public EnemySpawnPoint Spawn;
    public bool isConflicted;
    public bool isSpawner;
    public bool isPath;
    

    private void Start()
    {
        thisCollider = GetComponent<BoxCollider>();
        if (Spawn != null)
        {
            if (GameSystemsManager.current.SpawnPoints.Contains(Spawn)) return;
            isSpawner = true;
            GameSystemsManager.current.SpawnPoints.Add(Spawn);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Island"))
        {
            IslandController temp = other.GetComponent<IslandController>();
            if (!belogingIsland.ConectedIslands.Contains(temp)) return;
            if (belogingIsland == temp) return;
            
            belogingIsland.ConectedIslands.Remove(temp);


        }
        
        if (other.CompareTag("IslandSide/Grass"))
        {
            isConflicted = false;
            if(GameSystemsManager.current.ConflictedPath.Contains(this))
                GameSystemsManager.current.ConflictedPath.Remove(this);   
        }
        if (other.CompareTag("IslandSide/Path"))
        {
            if (GameSystemsManager.current.SpawnPoints.Contains(Spawn)) return;
            isSpawner = true;
            GameSystemsManager.current.SpawnPoints.Add(Spawn);
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isPath) return;

        if (other.CompareTag("Island"))
        {
            IslandController temp = other.GetComponent<IslandController>();
            if (belogingIsland.ConectedIslands.Contains(temp)) return;
            if (belogingIsland == temp) return;
            belogingIsland.ConectedIslands.Add(temp);


        }

        if (other.CompareTag("IslandSide/Grass"))
        {
            isConflicted = true;
            if(!GameSystemsManager.current.ConflictedPath.Contains(this))
                GameSystemsManager.current.ConflictedPath.Add(this);
        }

        else if (other.CompareTag("IslandSide/Path"))
        {
            isConflicted = false;
            if(GameSystemsManager.current.ConflictedPath.Contains(this))
                GameSystemsManager.current.ConflictedPath.Remove(this);
            if (Spawn != null)
            {
                isSpawner = false;
                GameSystemsManager.current.SpawnPoints.Remove(Spawn);
            }
            

            


        }
    }

    private void OnDestroy()
    {
        GameSystemsManager.current.ConflictedPath.Remove(this);
        GameSystemsManager.current.SpawnPoints.Remove(Spawn);
        
    }
    
    
}
