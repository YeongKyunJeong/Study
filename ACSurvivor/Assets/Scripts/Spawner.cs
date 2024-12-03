using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnerTransform;
    public Transform[] spawnPoints;
    public Vector3[] spawnPointsPosition;

    private int spawnPointCount;
    private float timer = 0;

    private void Awake()
    {
        if (spawnerTransform == null)
        {
            spawnerTransform = transform;
        }
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("스폰 지점 할당 안 됨");
        }
        spawnPointCount = spawnPoints.Length;

    }

    int k = 0;

    private void Update()
    {
        spawnerTransform.eulerAngles = Vector3.zero;
        timer += Time.deltaTime;

        if (timer > 0.2f && k < 100)
        {
            k++;
            Spawn();
            timer = 0;
        }

    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.Get(0 /* Random.Range(0, 2) */);
        enemy.transform.position = spawnerTransform.position + spawnPointsPosition[Random.Range(0, spawnPointCount)];
    }



    private void OnValidate()
    {
        //if(spawnPoints.Length == 0)
        //{
        //    spawnPoints = new Transform[transform.childCount];
        //    spawnPointsPosition = new Vector3[transform.childCount];
        //    for (int i = 0; i < transform.childCount; i++)
        //    {
        //        spawnPoints[i] = transform.GetChild(i);
        //        spawnPointsPosition[i] = spawnPoints[i].localPosition;
        //    }
        //}
        //for (int i = 0; i < spawnPointsPosition.Length; i++)
        //{
        //    spawnPoints[i].localPosition = spawnPointsPosition[i];
        //}

    }

}
