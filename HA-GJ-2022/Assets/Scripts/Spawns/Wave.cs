using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Wave : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnPointData
    {
        public string enemyName;
        public GameObject enemy;
        public bool randomSpawn;
        public Transform point;
    }
    [HideInInspector] public List<Transform> spawnPositions;

    [SerializeField] private List<SpawnPointData> spawnsPoints;

    [SerializeField] private float wave_CD = 1;

    public List<SpawnPointData> getData()
    {
        return this.spawnsPoints;
    }

    private void OnEnable()
    {
        GameManager.Instance.IsInWave = true;
        int count = 0;
        foreach(SpawnPointData sp in spawnsPoints)
        {
            Vector2 spawnPos = sp.point.position;
            if (sp.randomSpawn)
                spawnPos = spawnPositions[Random.Range(0, spawnPositions.Count - 1)].position;

            Enemies e = Instantiate(sp.enemy, spawnPos, Quaternion.identity).GetComponent<Enemies>();
            if (e.CanUseCounter())
                count++;
        }
        GameManager.Instance.EnemiesInWave += count;
        this.gameObject.SetActive(false);
    }

    public float GetWaveCooldown() { return this.wave_CD; }

}
