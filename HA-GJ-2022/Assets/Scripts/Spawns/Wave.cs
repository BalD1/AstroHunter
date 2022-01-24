using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnPointData
    {
        public string enemyName;
        public GameObject enemy;
        public Transform point;
    }
    [SerializeField] private List<SpawnPointData> spawnsPoints;
    public List<SpawnPointData> getData()
    {
        return this.spawnsPoints;
    }

    private void OnEnable()
    {
        GameManager.Instance.IsInWave = true;
        foreach(SpawnPointData sp in spawnsPoints)
        {
            Instantiate(sp.enemy, sp.point.transform.position, Quaternion.identity);
            GameManager.Instance.EnemiesInWave++;
        }
        this.gameObject.SetActive(false);
    }

}
