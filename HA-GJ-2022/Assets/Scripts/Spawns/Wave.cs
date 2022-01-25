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
        int count = 0;
        foreach(SpawnPointData sp in spawnsPoints)
        {
            Enemies e = Instantiate(sp.enemy, sp.point.transform.position, Quaternion.identity).GetComponent<Enemies>();
            if (e.CanUseCounter())
                count++;
        }
        GameManager.Instance.EnemiesInWave = count;
        this.gameObject.SetActive(false);
    }

}
