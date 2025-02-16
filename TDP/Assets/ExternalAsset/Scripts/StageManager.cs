using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{
    public class StageManager : MonoBehaviour
    {
        private GameManager gameManager;
        //private PlayerStats playerStat;
        [SerializeField] private StageUIManager stageUIManager;
        [SerializeField] private BuildManager buildManager;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private Shop shop;
        [SerializeField] private Node[] nodes;

        public void Initialize()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }
            //playerStat = gameManager.GetPlayerStats;

            if (stageUIManager == null)
            {
                Debug.Log("Stage UI Manager not assigned");
                stageUIManager = FindObjectOfType<StageUIManager>();
            }
            stageUIManager.Initialize();

            if (buildManager == null)
            {
                Debug.Log("Build Manager not assigned");
                buildManager = FindObjectOfType<BuildManager>();
            }
            buildManager.Initialize();

            if (enemySpawner == null)
            {
                Debug.Log("Enemy Spawner not assigned");
                enemySpawner = FindObjectOfType<EnemySpawner>();
            }
            enemySpawner.Initialize();
            //ChangeValue(StageUITMPType.WaveIndex, 0);
            //ChangeValue(StageUITMPType.WaveCountDown, 0.0f);

            if(shop == null)
            {
                Debug.Log("Shop not assigned");
                shop = FindObjectOfType<Shop>();
            }
            shop.Initialize();

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].Initialize();
            }
        }

        public void ChangeValue(StageUITMPType tagetTMP, float targetValue)
        {
            stageUIManager.ChangeValue(tagetTMP, targetValue);
        }

        public void ChangeValue(StageUITMPType tagetTMP, int targetValue)
        {
            stageUIManager.ChangeValue(tagetTMP, targetValue);
        }
    }
}