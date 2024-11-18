using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    private List<GameObject>[] spawnedPools;
}
