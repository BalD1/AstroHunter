﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

}
