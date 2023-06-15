using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameSystemsManager : SingletonManager<GameSystemsManager>
{

     
    [FoldoutGroup("Build Grid")] public BuildGridManager GridManager;
    [FoldoutGroup("Build Grid")] public UICreateManager UIManager;
    [FoldoutGroup("Build Grid")] [ShowInInspector]public List<IGameController> BDModeControllers= new List<IGameController>();
    [FoldoutGroup("Build Grid")] public CinemachineVirtualCamera BDCamera;
    [FoldoutGroup("Build Grid")] public GameObject BuildCanvas;
    [FoldoutGroup("Build Grid")] public EditIslandCanvasController editIslandCanvas;
    [FoldoutGroup("Build Grid")] public IslandObjectSO SelectedIsland;
    
    [FoldoutGroup("Build Grid")] public Button StartGame;
    [FoldoutGroup("Build Grid")] public List<CheckPathDetectionController> ConflictedPath;
    [FoldoutGroup("Build Grid")] public List<IslandController> IsolatedIslands;
    [FoldoutGroup("Build Grid")] public GameObject BaseTiles;
    [HideInInspector] public UnityEvent OnDropSelection = new UnityEvent();

    [FoldoutGroup("TDShooting")] public TDShooterManager ShooterManager;
    [FoldoutGroup("TDShooting")] [ShowInInspector]public List<IGameController> TDSModeControllers= new List<IGameController>();
    [FoldoutGroup("TDShooting")] public ObjectsPool Pool;
    [FoldoutGroup("TDShooting")] public List<EnemySpawnPoint> SpawnPoints;
    [FoldoutGroup("TDShooting")] public CinemachineVirtualCamera TDSCamera;
    [FoldoutGroup("TDShooting")] public GameObject ShootingCanvas;
    [FoldoutGroup("TDShooting")] public GameObject Player;

    [FoldoutGroup("Generic Systems")] 
    [ShowInInspector][FoldoutGroup("Generic Systems")] private List<IGameController> _activeControllers;
    [FoldoutGroup("Generic Systems")] public List<IGameController> ActiveControllers
    {
        get => _activeControllers;
        set
        {
            if (value != _activeControllers)
            {
                if (_activeControllers != null)
                {
                    foreach (var cont in _activeControllers)
                    {
                        cont.SetOffController();
                    }
                }

                _activeControllers = value;

                if (_activeControllers != null)
                {
                    foreach (var cont in _activeControllers)
                    {
                        cont.SetOnController();
                    }
                }
            }
        }
    }
    [FoldoutGroup("Generic Systems")] public Camera mainCamera;
    [FoldoutGroup("Generic Systems")] public float CameraBlendTime;
    [FoldoutGroup("Generic Systems")] public AstarPath pathfind;
    public enum GameModes
    {
        BuildGrid,
        TDShooter,
    }
    [FoldoutGroup("Generic Systems")] public GameModes ActiveMode;
    
    [FoldoutGroup("Economy")] public int InitialCurrency;

    int _Currency;
    [FoldoutGroup("Economy")] public int Currency
    {
        get => _Currency;
        set
        {
            _Currency = value;
            UpdateCurrencyTexts();
        }
    }
    [FoldoutGroup("Economy")] public TMP_Text ShootingCurrencyText;
    [FoldoutGroup("Economy")] public TMP_Text BuildingCurrencyText;
    


    private void OnEnable()
    {
        BDModeControllers.Add(GridManager);
        BDModeControllers.Add(UIManager);
        TDSModeControllers.Add(ShooterManager);
        Currency = InitialCurrency;
        ChangeModes();
    }


    public void ChangeModes()
    {
        switch (ActiveMode)
        {
            case GameModes.BuildGrid:
                ActiveMode = GameModes.TDShooter;
                Player.SetActive(true);
                for (int i = 0; i < SpawnPoints.Count; i++)
                {
                    if (SpawnPoints[i] == null)
                        SpawnPoints.Remove(SpawnPoints[i]);
                }
                TDSCamera.gameObject.SetActive(true);
                BDCamera.gameObject.SetActive(false);
                mainCamera.orthographic = false;
                ActiveControllers = TDSModeControllers;
                ShootingCanvas.SetActive(true);
                BuildCanvas.SetActive(false);
                BaseTiles.SetActive(false);
                pathfind.Scan();
                break;
            case GameModes.TDShooter:
                ActiveMode = GameModes.BuildGrid;
                Player.SetActive(false);
                TDSCamera.gameObject.SetActive(false);
                BDCamera.gameObject.SetActive(true);
                StartCoroutine(ChangeOrtCameraBlend());
                ActiveControllers = BDModeControllers;
                ShootingCanvas.SetActive(false);
                BuildCanvas.SetActive(true);
                BaseTiles.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    IEnumerator ChangeOrtCameraBlend()
    {
        yield return ScriptsTools.GetWait(CameraBlendTime);
        mainCamera.orthographic = true;
    }
    


    private void Update()
    {
        foreach (var con in ActiveControllers)
        {
            con.ControllerUpdate();
        }

        if (ConflictedPath.Count > 0 || SpawnPoints.Count==0 || IsolatedIslands.Count>0)
            StartGame.interactable = false;
        else
            StartGame.interactable = true;
    }
    
    private void UpdateCurrencyTexts()
    {
        ShootingCurrencyText.text = Currency.ToString();
        BuildingCurrencyText.text = Currency.ToString();
    }
}



public interface IGameController
{
    public void ControllerUpdate()
    {
        
    }

    public void SetOffController()
    {
        
    }

    public void SetOnController()
    {
        
    }
}
