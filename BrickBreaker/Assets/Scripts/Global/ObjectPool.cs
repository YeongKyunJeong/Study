using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string key;
    public GameObject prefab;
    public int size;
}

public class ObjectPool : MonoBehaviour
{
    [SerializeField] List<Pool> pools = new List<Pool>();
    Dictionary<string, List<GameObject>> poolDictionary = new Dictionary<string, List<GameObject>>();

    void Start()
    {
        foreach (var pool in pools)
        {
            poolDictionary[pool.key] = new List<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject go = Instantiate(pool.prefab);
                go.SetActive(false);
                poolDictionary[pool.key].Add(go);
            }
        }
    }

    public T GetObject<T>(string key) where T : MonoBehaviour
    {
        List<GameObject> pool;
        if (poolDictionary.TryGetValue(key, out pool))
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if(pool[i].activeInHierarchy)
                    return pool[i].GetComponent<T>();
            }
            
            // 추가 생성
        }

        return null;
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













