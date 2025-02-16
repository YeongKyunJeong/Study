using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance { get { return instance; } private set { instance = value; } }

        [SerializeField] private PlayerStats playerStats;
        //public PlayerStats GetPlayerStats { get => playerStats; }

        private static StageManager stageManager;
        public static StageManager StageManager { get { return stageManager; } private set { stageManager = value; } }


        private bool gameEnded = false;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            playerStats = GetComponent<PlayerStats>();
            playerStats.Initialize();

            if (StageManager == null)
            {
                StageManager = FindObjectOfType<StageManager>();
            }
            StageManager.Initialize();
        }

        private void Update()
        {
            if (gameEnded)
            {
                return;
            }

            if (PlayerStats.Life <= 0)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            Debug.Log("Game Over!");

            Time.timeScale = 0;
        }
    }
}
