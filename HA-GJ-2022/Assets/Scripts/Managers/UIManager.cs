using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesCounter;
    [SerializeField] private GameObject nextWaveText;

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
                break;

            case GameManager.GameStates.InGame:
                break;

            case GameManager.GameStates.Pause:
                break;

            case GameManager.GameStates.Win:
                break;

            case GameManager.GameStates.GameOver:
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

    public void UpdateEnemiesCounter(int count)
    {
        enemiesCounter.text = "Enemies left : \n" + count;
    }

    public void SetWaveStateUI(bool isInWave)
    {
        nextWaveText.SetActive(!isInWave);

        enemiesCounter.enabled = isInWave;
    }

    public void SetPlayerPortrait(bool hurt)
    {
        if (hurt)
            portrait.sprite = playerHurt;
        else
            portrait.sprite = playerNormal;
    }

}
