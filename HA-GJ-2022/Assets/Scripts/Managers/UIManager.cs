using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Menus / UI")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endgameMenu;
    [SerializeField] private TextMeshProUGUI endgameTitle;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject nextWaveText;
    [SerializeField] private GameObject waveUI;

    [Header("Player related")]
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private Image playerHPBar;
    [SerializeField] private Image portrait;
    [SerializeField] private Sprite playerNormal;
    [SerializeField] private Sprite playerHurt;


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
                mainMenu.SetActive(true);
                waveUI.SetActive(false);
                pauseMenu.SetActive(false);
                endgameMenu.SetActive(false);
                break;

            case GameManager.GameStates.InGame:
                playerHUD.SetActive(true);
                mainMenu.SetActive(false);
                waveUI.SetActive(true);
                pauseMenu.SetActive(false);
                endgameMenu.SetActive(false);
                break;

            case GameManager.GameStates.Pause:
                playerHUD.SetActive(false);
                waveUI.SetActive(false);
                pauseMenu.SetActive(true);
                endgameMenu.SetActive(false);
                break;

            case GameManager.GameStates.Win:
                endgameMenu.SetActive(true);
                endgameTitle.SetText("YOU WON !");
                break;

            case GameManager.GameStates.GameOver:
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

    public void SetPlayerPortrait(bool hurt)
    {
        if (hurt)
            portrait.sprite = playerHurt;
        else
            portrait.sprite = playerNormal;
    }

    public void OnButtonPress(string button)
    {
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

    public Image GetPlayerHPBar() { return this.playerHPBar; }

}
