﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject wavesContainer;
    private bool firstWaveSpawned = false;
    private List<Wave> waves;
    private int wavesIndex = 0;
    private float nextWave_TIMER;


    private void Start()
    {
        waves = new List<Wave>();
        foreach(Transform child in wavesContainer.transform)
        {
            waves.Add(child.GetComponent<Wave>());
        }
        nextWave_TIMER = 1;
    }

    private void Update()
    {
        if (!firstWaveSpawned)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GameManager.Instance.GameState == GameManager.GameStates.InGame)
                {
                    NextWave();
                    firstWaveSpawned = true;
                }
            }
        }
        else
        {
            if (nextWave_TIMER > 0)
            {
                nextWave_TIMER -= Time.deltaTime;
                if (GameManager.Instance.EnemiesInWave <= 0)
                    NextWave();
            }
            else
                NextWave();
        }
    }

    private void NextWave()
    {
        nextWave_TIMER = waves[wavesIndex].GetWaveCooldown();
        waves[wavesIndex].gameObject.SetActive(true);
        wavesIndex++;
        if (wavesIndex >= waves.Count)
            GameManager.Instance.GameState = GameManager.GameStates.Win;
    }
}
