using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace LLL
{
    public enum SceneType
    {
        Null,
        TitleScene,
        StageSelectScene,
        InStageScene
    }


    public class GameManager : MonoSingleton<GameManager>
    {

        public const string TITLE_SCENE_NAME_STR = "TitleScene";
        public const string STAGE_SELECT_SCENE_NAME_STR = "StageSelectScene";
        public const string IN_STAGE_SCENE_STR = "InStageScene";

        private SceneType currentSceneType;

        public SceneType CurrentSceneType
        {
            get
            {
                if (currentSceneType == SceneType.Null) GetCurrentSceneType();
                return currentSceneType;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            if (Instance == null) { } // Always False by MonoSingleton;

            InitializeGlobalManagers();


        }

        public void InitializeGlobalManagers()
        {
            DontDestroyOnLoad(gameObject);


        }

        private SceneType GetCurrentSceneType()
        {
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName.Contains(IN_STAGE_SCENE_STR))
            {
                return currentSceneType = SceneType.InStageScene;
            }
            else if (sceneName.Contains(STAGE_SELECT_SCENE_NAME_STR))
            {
                return currentSceneType = SceneType.StageSelectScene;
            }
            else if (sceneName.Contains(TITLE_SCENE_NAME_STR))
            {
                return currentSceneType = SceneType.TitleScene;
            }

            return currentSceneType = SceneType.Null;
        }

        /// <param name="sceneName">로드할 씬 이름</param>
        public void LoadScene(string sceneName, int stageNumber = 0)
        {
            if (sceneName == IN_STAGE_SCENE_STR) sceneName = $"{IN_STAGE_SCENE_STR}_{stageNumber:00}";

            StartCoroutine(LoadSceneAsync(sceneName));
            // TO DO : Add Loading Logic;
        }

        public void LoadScene(SceneType sceneType, int stageNumber = 0)
        {
            string sceneName = string.Empty;
            switch (sceneType)
            {
                case SceneType.InStageScene:
                    {
                        sceneName = $"{IN_STAGE_SCENE_STR}_{stageNumber:00}";
                        break;
                    }
                case SceneType.StageSelectScene:
                    {
                        sceneName = STAGE_SELECT_SCENE_NAME_STR;
                        break;
                    }
                case SceneType.TitleScene:
                    {
                        sceneName = TITLE_SCENE_NAME_STR;
                        break;
                    }
                default:
                    {
                        Debug.LogError($"Scene Named {sceneType} Not Exists");
                        return;
                    }
            }

            StartCoroutine(LoadSceneAsync(sceneName));
            // TO DO : Add Loading Logic;
        }

        private IEnumerator LoadSceneAsync(string sceneName, int stageNumber = 0)
        {
            if (sceneName == IN_STAGE_SCENE_STR) sceneName = $"{sceneName}_{stageNumber:00}";
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            GetCurrentSceneType();



            yield return null;
        }

    }

}