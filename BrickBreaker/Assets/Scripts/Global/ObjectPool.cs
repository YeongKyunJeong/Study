using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PoolObjectType
{
    Ball,
    DroppingItemBox
}

[System.Serializable]
public class Pool
{
    public PoolObjectType key;
    public GameObject prefab;
    public int size;
}

public interface IObjectPool
{
    void GetInitialize();
    void Release();
}

public class ObjectPool : MonoBehaviour
{
    [SerializeField] List<Pool> pools = new List<Pool>();
    Dictionary<PoolObjectType, Pool> poolDic = new Dictionary<PoolObjectType, Pool>();
    
    private List<GameObject> pooledGameObjects;
    private GameObject newPooledGameObject;
    Dictionary<PoolObjectType, List<GameObject>> poolDictionary = new Dictionary<PoolObjectType, List<GameObject>>();

    public void Initialize()
    {
        for (int i = 0; i < pools.Count; i++)
        {
            List<GameObject> list = new List<GameObject>();
            
            poolDictionary[pools[i].key] = list;

            for (int j = 0; j < pools[i].size; j++)
            {
                GameObject go = Instantiate(pools[i].prefab);
                go.SetActive(false);
                list.Add(go);
            }

            poolDic[pools[i].key] = pools[i];
        }
        
        // Ball[] preGeneratedBalls = GameObject.FindObjectsByType<Ball>(FindObjectsSortMode.None);
        //
        // for (int i = 0; i < pools.Count; i++)
        // {
        //     poolDictionary[pools[i].key] = new List<GameObject>();
        //
        //     if (pools[i].key == PoolObjectType.Ball)
        //     {
        //         if (preGeneratedBalls.Length > 0)
        //         {
        //             pools[i].size = preGeneratedBalls.Length;
        //             for (int j = 0; j < preGeneratedBalls.Length; j++)
        //             {
        //                 poolDictionary[PoolObjectType.Ball].Add(preGeneratedBalls[j].gameObject);
        //             }
        //         }
        //         Debug.Log($"Detected balls : {preGeneratedBalls.Length}");
        //     }
        // }
    }



    //void Start()
    //{
    //    foreach (var pool in pools)
    //    {
    //        poolDictionary[pool.key] = new List<GameObject>();
    //        for (int i = 0; i < pool.size; i++)
    //        {
    //            GameObject go = Instantiate(pool.prefab);
    //            go.SetActive(false);
    //            poolDictionary[pool.key].Add(go);
    //        }
    //    }
    //}

    public T GetObject<T>(PoolObjectType key) where T : MonoBehaviour, IObjectPool
    {
        if (poolDictionary.TryGetValue(key, out pooledGameObjects))
        {
            for (int i = 0; i < pooledGameObjects.Count; i++)
            {
                if (!pooledGameObjects[i].activeInHierarchy)
                {
                    pooledGameObjects[i].SetActive(true);
                    T component = pooledGameObjects[i].GetComponent<T>();
                    component.GetInitialize();
                    return component;
                }
            }

            var pool = poolDic[key];
            newPooledGameObject = Instantiate(pool.prefab);
            pooledGameObjects.Add(newPooledGameObject);
        }
        
        return newPooledGameObject.GetComponent<T>();
    }

    public void Release<T>(PoolObjectType key, T obj) where T : MonoBehaviour
    {
        obj.gameObject.SetActive(false);
        // obj.Release();
    }

    // public T Add<T>(T a, T b)
    // {
    //     return a + b;
    // }
    //
    // public int Add(int a, int b)
    // {
    //     return a + b;
    // }
    //
    // public float Add(float a, float b)
    // {
    //     return a + b;
    // }
    //
    // public void Test()
    // {
    //     // 오버로드
    //     Add(1, 1);
    //     Add(1.0f, 1.0f);
    //     
    // }
}

// public class MonoSingleton<TestManager> : MonoBehaviour where T : MonoSingleton<T>
// public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
// {
//     private static T instance;
//     public static T Instance{get { return instance; }}
//
//     public void SetInstance(T instance)
//     {
//         MonoSingleton<T>.instance = instance;
//     }
// }
//
// public class TestManager : MonoSingleton<TestManager>
// {
//     public void Start()
//     {
//         SetInstance(this);
//     }
// }
//
// public class GameMgr : MonoBehaviour
// {
//     private static GameMgr instance;
//     public static GameMgr Instance{get { return instance; }}
//
//     public static GameMgr GetInstance()
//     {
//         return instance;
//     }
//     
//     private void Awake()
//     {
//         instance = this;
//     }
// }


// public class TestManager2 : MonoSingleton<TestManager>
// {
//     public void Start()
//     {
//         instance = this;
//     }
// }













