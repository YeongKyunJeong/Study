using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RSP2
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [field: SerializeField] private InGameInitializer inGameInitializer { get; set; }

        [field: SerializeField] private SceneInitializer sceneInitializer;

        public bool IsInitialized { get; private set; }

        [field: SerializeField] private SceneType currentSceneType { get; set; }

        public const string TITLE_SCENE_NAME_STR = "TitleScene";
        public const string IN_GAME_SCENE_NAME_STR = "InGameScene_";

        private void Awake()
        {
            if (Instance == null) { }// Always false by MonoSingleton

            DontDestroyOnLoad(gameObject);
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

            InitializeScene();
            yield return null;
        }

        private void InitializeScene()
        {
            SceneInitializer sceneInitializer = FindObjectOfType<SceneInitializer>();
            if (sceneInitializer != null)
            {
                sceneInitializer.Initialize();
                return;
            }
            else
            {
                Debug.LogWarning("No Scene Initializer Found in This Scene");
            }

        }

        public void TitleSceneContinueCall(/*string sceneSubName*/)
        {
            //LoadScene(string.Concat(IN_GAME_SCENE_NAME_STR, sceneSubName));
        }

        public void TitleSceneStartCall(string sceneSubName)
        {
            LoadScene(string.Concat(IN_GAME_SCENE_NAME_STR, sceneSubName));
        }

        public void TitleSceneQuitCall()
        {
            QuitGame();
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

    }
}
