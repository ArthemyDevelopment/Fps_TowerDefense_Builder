using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

using UnityEngine;


public class EnemySpawnPoint : MonoBehaviour
{
    public IslandController BelongingIsland;

    public void InstantiateEnemy(GameObject enemy)
    {
        enemy.transform.position = transform.position;
        AILerp temp = enemy.GetComponent<AILerp>();
        temp.destination = Vector3.zero;
        temp.SearchPath();
        EnemyController tempController = enemy.GetComponent<EnemyController>();
        tempController.Reset();
    }

    
}
