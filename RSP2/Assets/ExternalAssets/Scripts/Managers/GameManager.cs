using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RSP2
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [field: SerializeField] private DataManager DataManager { get; set; }

        //[field: SerializeField] private InGameInitializer inGameInitializer { get; set; }

        //[field: SerializeField] private SceneInitializer sceneInitializer;


        [field: SerializeField] private SceneType currentSceneType { get; set; }
        public UserData CurrentUserData { get; private set; }


        public const string TITLE_SCENE_NAME_STR = "TitleScene";
        public const string IN_GAME_SCENE_NAME_STR = "InGameScene_";


        protected override void Awake()
        {
            base.Awake();

            if (Instance == null) { }// Always false by MonoSingleton

            if (DataManager == null)
            {
                Debug.Log("Data Manager Not Assigned");
                DataManager = FindObjectOfType<DataManager>();
            }

            DataManager.Initialize();

            CurrentUserData = DataManager.UserDataLoader.LoadUserData(0);

            currentSceneType = GetCurrentSceneType();

            DontDestroyOnLoad(gameObject);
        }

        private SceneType GetCurrentSceneType(string sceneName = "")
        {
            if (sceneName.Length == 0)
            {
                sceneName = SceneManager.GetActiveScene().name;
            }

            if (sceneName.Contains(TITLE_SCENE_NAME_STR))
            {
                return SceneType.TitleScene;
            }
            else if (sceneName.Contains(IN_GAME_SCENE_NAME_STR))
            {
                return SceneType.InGameScene;
            }
            // TO DO :: Add if Other sceneType is Add

            return SceneType.InGameScene;
        }


        /// <param name="sceneName">로드할 씬 이름</param>
        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            // TO DO :: Add Loading Screen
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            currentSceneType = GetCurrentSceneType(sceneName);
            InitializeScene();

            yield return null;
        }

        private void InitializeScene()
        {
            SceneInitializer sceneInitializer = FindObjectOfType<SceneInitializer>();
            if (sceneInitializer != null)
            {
                sceneInitializer.Initialize(); // Just to Ensure SceneInitializer Assigned
                return;
            }
            else
            {
                Debug.LogWarning("No Scene Initializer Found in This Scene");
            }


        }

        #region Title Scene
        public void TitleSceneContinueCall(/*string sceneSubName*/)
        {
            PlayerSaveData lastSaveData = DataManager.UserDataLoader.CurrentSaveDataList[DataManager.UserDataLoader.CurrentSaveDataList.Count - 1];
            if (lastSaveData.SceneNumber == 0)
            {
                // TO DO :: Add Each Scene Name Finding by SceneNumber Logic
                LoadScene(string.Concat(IN_GAME_SCENE_NAME_STR, "TestRoom"));
                return;
            }

            LoadScene(string.Concat(IN_GAME_SCENE_NAME_STR, lastSaveData.SceneNumber.ToString("D2")));
            return;
        }

        public void TitleSceneStartCall(string sceneSubName)
        {
            LoadScene(string.Concat(IN_GAME_SCENE_NAME_STR, sceneSubName));
        }

        public void TitleSceneQuitCall()
        {
            QuitGame();
        }

        public PlayerSaveData CallSaveDataLoading(int userID = 0, int saveNumber = -1)
        {
            return DataManager.LoadSaveData(userID, saveNumber);
        }

        public void QuitGame()
        {
            // TO DO:: Add Game Save Logic

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        #endregion

        #region In Game Scene
        public void InGameSceneSaveCall()
        {
            DataManager.UserDataWriter.SaveCurrentData();
        }

        public void InGameSceneLoadCall(int saveNumber)
        {
            PlayerSaveData targetSaveData = DataManager.LoadSaveData(CurrentUserData.UserID, saveNumber);

            if (targetSaveData == null) return;

            LoadScene(string.Concat(IN_GAME_SCENE_NAME_STR, targetSaveData.SceneNumber.ToString("D2")));
            return;
        }

        public void InGameSceneTitleCall()
        {
            LoadScene(string.Concat(TITLE_SCENE_NAME_STR));
        }

        #endregion
    }
}
