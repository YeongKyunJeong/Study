using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{

    public class EnemySpawner : MonoBehaviour
    {
        private GameManager gameManager;
        private StageManager stageManager;

        [SerializeField] private ObjectPool enemyPool;

        public Transform enemyPrefab;

        public Transform spawnPoint;

        public Enemy enemyInitializer;
        public float timeBetweenWaves = 2f;
        private float countDown = 2f;
        private bool countDownGoing = true;

        private int waveIndex = 0;

        private Coroutine coroutineField;
        private float spawnBySpawnTimeFloat = 0.5f;
        private WaitForSeconds spawnBySpawnTime;

        public void Initialize()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }

            if (stageManager == null)
            {
                stageManager = GameManager.StageManager;
            }

            if (enemyPool == null)
            {
                transform.GetComponent<ObjectPool>();
            }
            enemyPool.Initialize();
            spawnBySpawnTime = new WaitForSeconds(spawnBySpawnTimeFloat);

            countDownGoing = true;
        }


        private void Update()
        {
            if (countDownGoing)
            {
                if (countDown <= 0f)
                {
                    SpawnWave();
                    countDown = timeBetweenWaves;
                }
                stageManager.ChangeValue(StageUITMPType.WaveCountDown, countDown);
                countDown -= Time.deltaTime;
            }


        }

        void SpawnWave()
        {
            coroutineField = StartCoroutine(SpawnWaveCoroutine());
        }

        void SpawnEnemy()
        {
            enemyInitializer = enemyPool.GetObject<Enemy>(PoolObjectType.Enemy);
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemyInitializer.SetEnemy(0, 10f, 10, 50);
        }

        IEnumerator SpawnWaveCoroutine()
        {

            countDownGoing = false;
            int thisWaveIndexMax = ++waveIndex; // To do : Change enemy per wave number variation logic 
            stageManager.ChangeValue(StageUITMPType.WaveIndex, waveIndex);

            for (int i = 0; i < thisWaveIndexMax; i++)
            {
                SpawnEnemy();
                yield return spawnBySpawnTime;
            }

            countDownGoing = true;
            yield return null;
        }

    }
}