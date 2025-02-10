using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{
    public class StageManager : MonoBehaviour
    {
        private GameManager gameManager;
        [SerializeField] private StageUIManager stageUIManager;
        [SerializeField] private EnemySpawner enemySpawner;

        public void Initialize()
        {
            if(gameManager == null)
            {
                gameManager = GameManager.Instance;
            }

            if (stageUIManager == null)
            {
                stageUIManager = FindObjectOfType<StageUIManager>();
            }
            stageUIManager.Initialize();

            if(enemySpawner == null)
            {
                enemySpawner = FindObjectOfType<EnemySpawner>();
            }
            enemySpawner.Initialize();

            ChangeValue(StageUITMPType.WaveIndex, 0);
            ChangeValue(StageUITMPType.WaveCountDown, 0.0f);
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