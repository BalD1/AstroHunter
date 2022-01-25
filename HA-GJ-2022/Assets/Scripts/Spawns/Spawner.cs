﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject wavesContainer;
    private List<Wave> waves;
    private int wavesIndex = 0;

    private void Start()
    {
        waves = new List<Wave>();
        foreach(Transform child in wavesContainer.transform)
        {
            waves.Add(child.GetComponent<Wave>());
        }
    }

    private void Update()
    {
        if (GameManager.Instance.GameState == GameManager.GameStates.InGame && !GameManager.Instance.IsInWave)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextWave();
            }
        }
    }

    private void NextWave()
    {
        waves[wavesIndex].gameObject.SetActive(true);
        wavesIndex++;
        if (wavesIndex > waves.Count)
            GameManager.Instance.GameState = GameManager.GameStates.Win;
    }
}
