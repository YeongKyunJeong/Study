using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{
    public class StageManager : MonoBehaviour
    {
        private GameManager gameManager;
        //private PlayerStats playerStat;
        [SerializeField] private StageWorldSpaceUI stageWorldSpaceUIManager;
        [SerializeField] private BuildManager buildManager;
        [SerializeField] private WayPoints[] wayPointsInStage;
        [SerializeField] private EnemySpawner enemySpawner;

        [SerializeField] private Shop shop;
        [SerializeField] private Node[] nodes;


        [SerializeField] private GameOverPanel gameOverPanel;

        public void Initialize()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }
            //playerStat = gameManager.GetPlayerStats;

            if (stageWorldSpaceUIManager == null)
            {
                Debug.Log("Stage UI Manager not assigned");
                stageWorldSpaceUIManager = FindObjectOfType<StageWorldSpaceUI>();
            }
            stageWorldSpaceUIManager.Initialize();

            if (gameOverPanel == null)
            {
                Debug.Log("Game Over Panel not assigned");
                gameOverPanel = FindAnyObjectByType<GameOverPanel>();
            }
            gameOverPanel.Initialize();

            if (buildManager == null)
            {
                Debug.Log("Build Manager not assigned");
                buildManager = FindObjectOfType<BuildManager>();
            }
            buildManager.Initialize();

            if(wayPointsInStage== null || wayPointsInStage.Length == 0)
            {
                Debug.Log("Way Points not assigned");   // throw error;
            }
            WayPoints.ClearWayPointsList();
            foreach (WayPoints wayPoints in wayPointsInStage)
            {
                wayPoints.Initialize();
            }

            if (enemySpawner == null)
            {
                Debug.Log("Enemy Spawner not assigned");
                enemySpawner = FindObjectOfType<EnemySpawner>();
            }
            enemySpawner.Initialize();
            //ChangeValue(StageUITMPType.WaveIndex, 0);
            //ChangeValue(StageUITMPType.WaveCountDown, 0.0f);

            if (shop == null)
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
            stageWorldSpaceUIManager.ChangeValue(tagetTMP, targetValue);
        }

        public void ChangeValue(StageUITMPType tagetTMP, int targetValue)
        {
            stageWorldSpaceUIManager.ChangeValue(tagetTMP, targetValue);
        }

        public void EndGame()
        {
            gameOverPanel.OnEnableByManual(true);
        }
    }
}