using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject player;

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
                    break;

                case GameStates.InGame:
                    break;

                case GameStates.Pause: 
                    break;

                case GameStates.Win:
                    break;

                case GameStates.GameOver:
                    break;

                default:
                    Debug.LogError(value + " not found in switch statement.");
                    break;
            }
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
    }

    public Camera getMainCamera()
    {
        return mainCamera;
    }

    public GameObject getPlayerRef()
    {
        return player;
    }
}
