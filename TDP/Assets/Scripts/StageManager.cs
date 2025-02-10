using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner enemySpawner;

        private void Awake()
        {
            if(enemySpawner == null)
            {
                enemySpawner = FindObjectOfType<EnemySpawner>();
            }
            enemySpawner.Initialize();
        }
    }
}