using System.Collections;
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
    [HideInInspector] public int currentWave;

    [HideInInspector] public UnityEvent ev_ReloadEvent;

    [Header("Skins unlock conditions")]
    public bool wonGameOnce;
    public bool wonGameOnceNoHit;
    public int killsCount;
    public int deathsCount;

    [System.Serializable]
    public struct UnlockableSkins
    {
        public string name;
        public SkinsUnlock conditions;
    }
    public List<UnlockableSkins> unlockableSkins;

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
                    foreach (UnlockableSkins s in unlockableSkins)
                        s.conditions.setCondition();

                    CheckUnlockedSkins();
                    ev_ReloadEvent.Invoke();
                    player.GetComponent<Player>().Render(false);

                    if (killsCount > PlayerPrefs.GetInt("KillsCount"))
                        PlayerPrefs.SetInt("KillsCount", killsCount);
                    break;

                case GameStates.InGame:
                    Time.timeScale = 1;
                    if (GS == GameStates.Win || GS == GameStates.GameOver)
                        ev_ReloadEvent.Invoke();
                    player.GetComponent<Player>().Render(true);
                    break;

                case GameStates.Pause: 
                    Time.timeScale = 0;
                    break;

                case GameStates.Win:
                    Time.timeScale = 0;
                    wonGameOnce = true;
                    PlayerPrefs.SetInt("WonGame", 1);
                    break;

                case GameStates.GameOver:
                    Time.timeScale = 0;
                    deathsCount++;
                    PlayerPrefs.SetInt("DeathsCount", deathsCount);
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

        PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("WonGame"))
            wonGameOnce = PlayerPrefs.GetInt("WonGame") >= 1 ? true : false;
        if (PlayerPrefs.HasKey("WonGameNoHit"))
            wonGameOnceNoHit = PlayerPrefs.GetInt("WonGameNoHit") >= 1 ? true : false;
        if (PlayerPrefs.HasKey("KillsCount"))
            killsCount = PlayerPrefs.GetInt("KillsCount");
        if (PlayerPrefs.HasKey("DeathsCount"))
            deathsCount = PlayerPrefs.GetInt("DeathsCount");

        GameState = GameStates.MainMenu;
        EnemiesInWave = 0;
        IsInWave = false;

        ev_ReloadEvent = new UnityEvent();
        ev_ReloadEvent.AddListener(Reload);

    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic();
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

        if (Input.GetKeyDown(KeyCode.A))
            if (player.GetComponent<Player>().GetStats().currentHP == player.GetComponent<Player>().GetStats().maxHP)
            {
                PlayerPrefs.SetInt("WonGameNoHit", 1);
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

    public void CheckUnlockedSkins()
    {
        foreach (UnlockableSkins s in unlockableSkins)
        {
            if (s.conditions.canUnlockSkin())
                UIManager.Instance.UnlockSkin(s.name);
        }
    }
}
