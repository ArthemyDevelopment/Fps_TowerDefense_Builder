using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class TDShooterManager : MonoBehaviour , IGameController
{
    [FoldoutGroup("Stats")]public int StartingEnemiesInWave;
    [FoldoutGroup("Stats")]public float increaseFactor;
    [FoldoutGroup("Stats")]public int CurrentEnemiesInWave;
    [FoldoutGroup("Stats")]public int EnemiesLeftInWave;
    [FoldoutGroup("Stats")]public int RemeaningEnemies;
    [FoldoutGroup("Stats")]public float StartingTimeBetweenEnemies;
    [FoldoutGroup("Stats")]public float reducingFactor;
    [FoldoutGroup("Stats")]public float CurrentTimeBetweenEnemies;
    [FoldoutGroup("Stats")]public float PreWaitTime;
    [FoldoutGroup("Stats")]public float PostWaitTime;
    private int Round=0;

    [FoldoutGroup("CanvasRefs")] public TMP_Text CurrEnemiesCount;
    [FoldoutGroup("CanvasRefs")] public TMP_Text TotalEnemiesCount;

    [FoldoutGroup("TurretsAutoAim")] public List<EnemyController> ActiveEnemiesList;
    [FoldoutGroup("TurretsAutoAim")] public float MaxSearchDistance;
    

    public void SetOnController()
    {
        UpdateRoundStats();
        Round++;
        TotalEnemiesCount.text = CurrentEnemiesInWave.ToString();
        RemeaningEnemies = EnemiesLeftInWave=CurrentEnemiesInWave;
        UpdateTexts();
        StartCoroutine(CoroutineSpawnEnemies());
        StartCoroutine(CoroutineEndPhase());
    }
    
    public void ControllerUpdate()
    {

    }

    void UpdateRoundStats()
    {
        CurrentEnemiesInWave = (int)(CurrentEnemiesInWave + StartingEnemiesInWave + (CurrentEnemiesInWave * increaseFactor));
        CurrentTimeBetweenEnemies = (CurrentTimeBetweenEnemies - (CurrentTimeBetweenEnemies * reducingFactor));
    }

    public void UpdateTexts()
    {
        CurrEnemiesCount.text = RemeaningEnemies.ToString();
    }

    public void EnemyRemoved(EnemyController enemy)
    {
        ActiveEnemiesList.Remove(enemy);
        RemeaningEnemies--;
        UpdateTexts();
    }

    private IEnumerator CoroutineSpawnEnemies()
    {
        yield return ScriptsTools.GetWait(PreWaitTime);

        while (EnemiesLeftInWave > 0)
        {
            yield return ScriptsTools.GetWait(CurrentTimeBetweenEnemies);
            int ran = Random.Range(0, GameSystemsManager.current.SpawnPoints.Count);

            GameSystemsManager.current.SpawnPoints[ran].InstantiateEnemy(ObjectsPool.current.GetEnemy());
            EnemiesLeftInWave--;
        }

       
    }
    
    IEnumerator CoroutineEndPhase()
    {
        while (RemeaningEnemies > 0) yield return null;
        yield return ScriptsTools.GetWait(PostWaitTime);
        GameSystemsManager.current.ChangeModes();
    }

    public Transform GetClosetsEnemy(Transform origin,float MaxRange)
    {
        Transform tempReturn = null;
        float shortDis= MaxRange;
        for (int i = 0; i < ActiveEnemiesList.Count; i++)
        {
            float tempDis = Vector3.Distance(origin.position, ActiveEnemiesList[i].transform.position);
            if (tempDis < shortDis)
            {
                shortDis = tempDis;
                tempReturn = ActiveEnemiesList[i].transform;
            }
        }

        return tempReturn;
    }

}
