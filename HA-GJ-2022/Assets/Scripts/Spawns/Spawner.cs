using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject wavesContainer;
    [SerializeField] private GameObject spawnPositionsContainer;
    private bool firstWaveSpawned = false;
    private List<Wave> waves;
    private int wavesIndex = 0;
    private float nextWave_TIMER;


    private void Start()
    {
        waves = new List<Wave>();
        foreach (Transform child in wavesContainer.transform)
        {
            Wave w = child.GetComponent<Wave>();
            foreach(Transform spawnPosChild in spawnPositionsContainer.transform)
            {
                w.spawnPositions.Add(spawnPosChild);
            }
            waves.Add(w);
        }
        GameManager.Instance.maxWave = waves.Count;
        nextWave_TIMER = 1;
        GameManager.Instance.ev_ReloadEvent.AddListener(SpawnerReload);
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
        //Debug.Log("Spawning " + waves[wavesIndex].gameObject.name);
        if (wavesIndex >= waves.Count)
        {
            GameManager.Instance.isInLastWave = true;
            return;
        }

        nextWave_TIMER = waves[wavesIndex].GetWaveCooldown();
        waves[wavesIndex].gameObject.SetActive(true);
        GameManager.Instance.getPlayerRef().GetComponent<Player>().UpgradeWeapon(wavesIndex);
        wavesIndex++;
    }

    private void SpawnerReload()
    {
        wavesIndex = 0;
        nextWave_TIMER = 1;
    }
}
