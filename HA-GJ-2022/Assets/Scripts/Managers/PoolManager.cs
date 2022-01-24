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
    public Dictionary<tags, Queue<GameObject>> poolDictionnary;

    private void Start()
    {
        poolDictionnary = new Dictionary<tags, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionnary.Add(pool.tag, objectPool);
        }
    }

    /// <summary>
    /// Spawns the object <paramref name="tag"/> at <paramref name="position"/> with <paramref name="rotation"/> 
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public GameObject SpawnFromPool(tags tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionnary.ContainsKey(tag))
        {
            Debug.LogError("Pool with tag " + tag + " doesn't exist");
            return null;
        }
        GameObject objToSpawn = poolDictionnary[tag].Dequeue();

        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;
        objToSpawn.SetActive(true);

        poolDictionnary[tag].Enqueue(objToSpawn);
        return objToSpawn;
    }


}