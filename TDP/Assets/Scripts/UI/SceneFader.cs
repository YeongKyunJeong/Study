using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TDP
{
    public enum SceneType
    {
        MainMenu,
        Stage,
        Retry
    }

    public class SceneFader : MonoBehaviour
    {
        [SerializeField] private Image screenImg;
        [SerializeField] private float fadingStartTime = 0.5f;
        [SerializeField] private float fadingTime = 1f;
        private float t;
        private float a;
        public AnimationCurve animationCurve;
        private GameManager gameManager;

        public const string MAIN_MENU_SCENE_NAME_STR = "MainMenuScene";

        public const string STAGE_SCENE_NAME_STR = "StageScene";
        private string targetSceneName;

        private void Start()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }
            StartCoroutine(FadeIn());
        }

        public void FadeTo(SceneType targetSceneType)
        {
            StartCoroutine(FadeOut(targetSceneType));
        }

        IEnumerator FadeIn()
        {
            screenImg.color = new Color(0f, 0f, 0f, 1);
            yield return new WaitForSeconds(fadingStartTime);
            t = 1;
            while (t > 0f)
            {
                t -= Time.deltaTime / fadingTime;
                a = animationCurve.Evaluate(t);
                screenImg.color = new Color(0f, 0f, 0f, t);

                yield return 0;
            }
        }

        IEnumerator FadeOut(SceneType targetSceneType)
        {
            yield return new WaitForSeconds(fadingStartTime);
            t = 0;
            while (t > 0f)
            {
                t += Time.deltaTime / fadingTime;
                a = animationCurve.Evaluate(t);
                screenImg.color = new Color(0f, 0f, 0f, t);

                yield return 0;
            }

            switch (targetSceneType)
            {
                case SceneType.MainMenu:
                    {
                        targetSceneName = MAIN_MENU_SCENE_NAME_STR;
                        break;
                    }
                case SceneType.Stage:
                    {
                        targetSceneName = STAGE_SCENE_NAME_STR;
                        break;
                    }
                default:
                    {
                        targetSceneName = SceneManager.GetActiveScene().name;
                        break;
                    }
            }
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
