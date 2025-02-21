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

        private static StageManager stageManager;
        public static StageManager StageManager { get { return stageManager; } private set { stageManager = value; } }

        private static MainMenu mainMenu;

        [SerializeField] private SceneFader sceneFader;
        public const string MAIN_MENU_SCENE_NAME_STR = "MainMenuScene";

        [SerializeField] private PlayerStats playerStats;
        //public PlayerStats GetPlayerStats { get => playerStats; }
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

            if (SceneManager.GetActiveScene().name == MAIN_MENU_SCENE_NAME_STR)
            {
                if (mainMenu == null)
                {
                    mainMenu = FindFirstObjectByType<MainMenu>();
                    mainMenu.Initialize();
                }
            }
            else
            {

                if (StageManager == null)
                {
                    StageManager = FindObjectOfType<StageManager>();
                }
                StageManager.Initialize();
            }

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
            CallSceneFader(SceneType.Retry);
        }

        public void MenuCall()
        {
            CallSceneFader(SceneType.MainMenu);
        }


        public void MainMenuPlayCall()
        {
            PlayGame();
        }

        private void PlayGame()
        {
            CallSceneFader(SceneType.Stage);
            //SceneManager.LoadScene(STAGE_SCENE_NAME_STR);
        }

        private void CallSceneFader(SceneType targetSceneType)
        {
            if (sceneFader == null)
            {
                sceneFader = FindObjectOfType<SceneFader>();
            }
            sceneFader.FadeTo(targetSceneType);
        }

        public void MainMenuQuitCall()
        {
            QuitGame();
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
