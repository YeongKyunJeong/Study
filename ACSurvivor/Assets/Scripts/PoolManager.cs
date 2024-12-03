using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform spawnParent;

    private int enemyTypeCount;

    private List<GameObject>[] spawnedPools;

    private void Awake()
    {
        enemyTypeCount = enemyPrefabs.Length;
        spawnedPools = new List<GameObject>[enemyTypeCount];
        for (int i = 0; i < enemyTypeCount; i++)
        {
            spawnedPools[i] = new List<GameObject>();
        }
    }
    public GameObject Get(int index)
    {
        GameObject selected = null;

        foreach (var item in spawnedPools[index]) // 죽은 유닛이 있으면 재활용
        {
            if (!item.gameObject.activeSelf)
            {
                item.SetActive(true);
                return item;
            }
        }
        // 이미 있는 유닛 중 죽은 유닛이 없음

        selected = Instantiate(enemyPrefabs[index], spawnParent);
        spawnedPools[index].Add(selected);

        return selected;
    }

}
