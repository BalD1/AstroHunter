using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public tags tag;
        public GameObject prefab;
        public int size;
    }

    #region instances
    private static PoolManager instance;
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("PoolManager Instance not found");

            return instance;
        }
    }
    #endregion

    private void Awake()
    {
        instance = this;
    }

    public enum tags
    {
        Laser,
    }

    public List<Pool> pools;
    public Dictionary<tags, List<GameObject>> poolDictionnary;

    private void Start()
    {
        poolDictionnary = new Dictionary<tags, List<GameObject>>();

        foreach (Pool pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            poolDictionnary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(tags tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionnary.ContainsKey(tag))
        {
            Debug.LogError("Pool with tag " + tag + " doesn't exist");
            return null;
        }
        foreach(GameObject obj in poolDictionnary[tag])
        {
            if (obj.activeSelf == false)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(poolDictionnary[tag][0], position, rotation);
        poolDictionnary[tag].Add(newObj);
        return newObj;
    }


}