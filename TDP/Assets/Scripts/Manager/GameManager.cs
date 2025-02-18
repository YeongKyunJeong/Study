using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


        public static bool isGameOver = false;



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

            isGameOver = false;
        }

        private void Update()
        {
            if (isGameOver)
            {
                return;
            }

            //if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    EndGame();
            //}

            if (PlayerStats.Life <= 0)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            isGameOver = true;

            stageManager.EndGame();
            //Debug.Log("Game Over!");

            //Time.timeScale = 0;
        }

        public void RetryCall()
        {
            Retry();
        }
        private void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Game Restarted");
        }

        public void MenuCall()
        {
            Menu();
        }
        private void Menu()
        {
            Debug.Log("Go to Menu"); // To do : Add main menu secen;
        }

    }
}
