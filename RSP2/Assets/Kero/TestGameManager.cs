using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RSP2
{
    public class TestGameManager : MonoBehaviour
    {
        public static TestGameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 씬 전환 메서드
        /// </summary>
        /// <param name="sceneName">로드할 씬 이름</param>
        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            // 로딩 화면 표시 (선택 사항)
            Debug.Log($"Loading Scene: {sceneName}");

            // 씬 비동기 로드
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            // 씬 초기화
            InitializeScene();
        }

        /// <summary>
        /// 씬 초기화
        /// </summary>
        private void InitializeScene()
        {
            var initializer = FindObjectOfType<SceneInitializer>();
            if (initializer != null)
            {
                initializer.Initialize();
            }
            else
            {
                Debug.LogWarning("No SceneInitializer found in the current scene.");
            }
        }
    }

}
