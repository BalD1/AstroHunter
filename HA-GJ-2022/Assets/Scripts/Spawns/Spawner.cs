using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum EnemiesTags
    {
        Snake,
    }
    public static Dictionary<EnemiesTags, GameObject> enemiesByTag;
    public static List<EnemiesTags> enemiesByTagList;
        
    [SerializeField] private List<Wave> waves;
    private int wavesIndex = 0;

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
