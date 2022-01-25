﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera signsCamera;
    [SerializeField] private GameObject player;

    [HideInInspector] public bool isInLastWave = false;
    [HideInInspector] public int maxWave;

    [HideInInspector] public UnityEvent ev_ReloadEvent;

    #region Game State

    public enum GameStates
    {
        MainMenu,
        InGame,
        Pause,
        Win,
        GameOver,
    }
    private GameStates GS;
    public GameStates GameState
    {
        get
        {
            return GS;
        }
        set
        {
            switch (value)
            {
                case GameStates.MainMenu:
                    ev_ReloadEvent.Invoke();
                    break;

                case GameStates.InGame:
                    Time.timeScale = 1;
                    if (GS == GameStates.Win || GS == GameStates.GameOver)
                        ev_ReloadEvent.Invoke();
                    break;

                case GameStates.Pause: 
                    Time.timeScale = 0;
                    break;

                case GameStates.Win:
                    Time.timeScale = 0;
                    break;

                case GameStates.GameOver:
                    Time.timeScale = 0;
                    break;

                default:
                    Debug.LogError(value + " not found in switch statement.");
                    break;
            }
            UIManager.Instance.windowManager(value);
            GS = value;
        }
    }

    #endregion

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("GameManager instance not found.");
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        GameState = GameStates.MainMenu;
        EnemiesInWave = 0;
        IsInWave = false;

        ev_ReloadEvent = new UnityEvent();
        ev_ReloadEvent.AddListener(Reload);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (GameState)
            {
                case GameStates.InGame:
                    GameState = GameStates.Pause;
                    break;

                case GameStates.Pause:
                    GameState = GameStates.InGame;
                    break;
            }
        }
    }

    private bool isInWave;
    public bool IsInWave
    {
        get => isInWave;
        set
        {
            isInWave = value;
            UIManager.Instance.SetWaveStateUI(value);
        }
    }

    public int EnemiesInWave
    {
        get => enemiesInWave;
        set
        {
            enemiesInWave = value;

            if (enemiesInWave <= 0 && isInLastWave)
                GameState = GameStates.Win;
        }
    }
    private int enemiesInWave = 0;

    public Camera getMainCamera()
    {
        return mainCamera;
    }
    public Camera getSignsCamera()
    {
        return signsCamera;
    }

    public GameObject getPlayerRef()
    {
        return player;
    }

    public void Reload()
    {
        isInLastWave = false;
    }
}
