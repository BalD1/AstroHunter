using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Menus / UI")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] private GameObject endgameMenu;
    [SerializeField] private TextMeshProUGUI endgameTitle;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] public GameObject optionsMenu;
    [SerializeField] private GameObject nextWaveText;
    [SerializeField] private GameObject waveUI;
    [SerializeField] public GameObject tutoUI;

    [Header("Player related")]
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private Image playerHPBar;
    [SerializeField] private Image portrait;

    [System.Serializable]
    public struct Skins
    {
        public string name;
        public GameObject page;
        public Button button;
    }
    [SerializeField] private List<Skins> skinsList;

    [System.Serializable]
    public struct PlayerPortraits
    {
        public string name;
        public Sprite normal;
        public Sprite hurt;
    }
    public List<PlayerPortraits> playerPortraits;
    public int currentPortraitIndex;

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("UIManager instance not found");
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    public void windowManager(GameManager.GameStates GS)
    {
        switch (GS)
        {
            case GameManager.GameStates.MainMenu:
                playerHUD.SetActive(false);
                tutoUI.SetActive(false);
                mainMenu.SetActive(true);
                waveUI.SetActive(false);
                pauseMenu.SetActive(false);
                endgameMenu.SetActive(false);
                break;

            case GameManager.GameStates.InGame:
                optionsMenu.SetActive(false);
                playerHUD.SetActive(true);
                mainMenu.SetActive(false);
                waveUI.SetActive(true);
                pauseMenu.SetActive(false);
                endgameMenu.SetActive(false);
                break;

            case GameManager.GameStates.Pause:
                tutoUI.SetActive(false);
                playerHUD.SetActive(false);
                waveUI.SetActive(false);
                pauseMenu.SetActive(true);
                endgameMenu.SetActive(false);
                break;

            case GameManager.GameStates.Win:
                tutoUI.SetActive(false);
                endgameMenu.SetActive(true);
                endgameTitle.SetText("YOU WON !");
                break;

            case GameManager.GameStates.GameOver:
                tutoUI.SetActive(false);
                endgameTitle.SetText("GAME OVER");
                endgameMenu.SetActive(true);
                break;

            default:
                Debug.LogError(GS + " not found in switch statement.");
                break;
        }
    }

    public void FillBar(ref GameObject bar, float currentAmount, float maxAmount)
    {
        Image img = bar.GetComponent<Image>();
        if (img == null)
        {
            Debug.LogError(bar + " does not countain an Image.");
            return;
        }
        img.fillAmount = currentAmount / maxAmount;
    }

    public void SetWaveStateUI(bool isInWave)
    {
        nextWaveText.SetActive(!isInWave);
    }

    public void SetCurrentPortrait(string name)
    {
        for(int i = 0; i < playerPortraits.Count; i++)
        {
            if (playerPortraits[i].name.Equals(name))
                currentPortraitIndex = i;
        }
    }

    public void SetPlayerPortrait(bool hurt)
    {
        if (hurt)
            portrait.sprite = playerPortraits[currentPortraitIndex].hurt;
        else
            portrait.sprite = playerPortraits[currentPortraitIndex].normal;
    }

    public void OnButtonPress(string button)
    {
        AudioManager.Instance.Play2DSound(AudioManager.ClipsTags.clic);
        switch(button)
        {
            case "PLAY":
                GameManager.Instance.GameState = GameManager.GameStates.InGame;
                break;

            case "CONTINUE":
                GameManager.Instance.GameState = GameManager.GameStates.InGame;
                break;

            case "PLAYAGAIN":
                GameManager.Instance.GameState = GameManager.GameStates.InGame;
                break;

            case "OPTIONS":
                optionsMenu.SetActive(!optionsMenu.activeSelf);

                if (GameManager.Instance.GameState == GameManager.GameStates.MainMenu)
                    mainMenu.SetActive(!mainMenu.activeSelf);
                else if (GameManager.Instance.GameState == GameManager.GameStates.Pause)
                    pauseMenu.SetActive(!pauseMenu.activeSelf);
                break;

            case "MAINMENU":
                GameManager.Instance.GameState = GameManager.GameStates.MainMenu;
                break;

            case "selectskin_base":
                GameManager.Instance.getPlayerRef().GetComponent<Player>().ChangeSkin(Player.Skins.Base);
                break;

            case "selectskin_amogus":
                GameManager.Instance.getPlayerRef().GetComponent<Player>().ChangeSkin(Player.Skins.Amogus);
                break;

            case "selectskin_fanta":
                GameManager.Instance.getPlayerRef().GetComponent<Player>().ChangeSkin(Player.Skins.Fanta);
                break;

            case "selectskin_antoine":
                GameManager.Instance.getPlayerRef().GetComponent<Player>().ChangeSkin(Player.Skins.Antoine);
                break;

            case "selectskin_doge":
                GameManager.Instance.getPlayerRef().GetComponent<Player>().ChangeSkin(Player.Skins.Doge);
                break;

            case "SHOP":
                shopMenu.SetActive(!shopMenu.activeSelf);
                mainMenu.SetActive(!mainMenu.activeSelf);
                break;

            case "EXIT":
                Application.Quit();
                break;
        }
    }

    public void UnlockSkin(string _name)
    {
        foreach(Skins s in skinsList)
        {
            if (s.name.Equals(_name))
            {
                s.button.interactable = true;
                s.button.GetComponent<Image>().color = Color.white;
            }
        }
    }

    public Image GetPlayerHPBar() { return this.playerHPBar; }

}
