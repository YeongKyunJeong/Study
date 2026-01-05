using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RSP2
{
    public class LoadingUI : MonoBehaviour
    {
        [field: SerializeField] private Image loadingImg;
        [field: SerializeField] private TextMeshProUGUI loadingTMP;
        [field: SerializeField] private TextMeshProUGUI percentMarkTMP;
        private GameManager gameManager;
        private bool isActive;
        private Coroutine setActiveCoroutine;
        private Coroutine loadingBarChangeCoroutine;
        public Queue<int> ChangeQueue
        {
            get
            {
                if (changeQueue == null) changeQueue = new Queue<int>();
                return changeQueue;
            }
        }
        private Queue<int> changeQueue;
        private static float lastPer;

        public void Initialize(GameManager _gameManager)
        {
            gameManager = _gameManager;
            setActiveCoroutine = null;
            SetLoadingPercent(0);
            SetActive(false);
        }

        public void SetLoadingPercent(int per)
        {
            if (!isActive) SetActive(true);

            //loadingImg.fillAmount = (float)per / 100;
            //loadingTMP.text = $"{per}";
            SetLoadingBarQueue(per);

        }

        public void SetLoadingPercent(float per)
        {
            if (!isActive) SetActive(true);
            int perInt = (int)per;
            //loadingImg.fillAmount = per / 100;
            //loadingTMP.text = $"{perInt}";
            SetLoadingBarQueue(perInt);
            //changeQueue.Enqueue(perInt);
            //if (loadingBarChangeCoroutine != null)
            //{
            //    loadingBarChangeCoroutine = StartCoroutine(LoadingBarChange());
            //}
        }

        public void SetActive(bool isActive, bool needDelay = false)
        {
            if (needDelay)
            {
                if (setActiveCoroutine != null)
                {
                    StopCoroutine(setActiveCoroutine);
                    setActiveCoroutine = null;
                }
                setActiveCoroutine = StartCoroutine(SetActiveContinuously(isActive));
                return;
            }


            if (isActive)
            {
                this.isActive = true;
                loadingImg.color = new Color(1, 1, 1, 1);
                loadingTMP.alpha = 1;
                percentMarkTMP.alpha = 1;
                return;
            }

            this.isActive = false;
            loadingTMP.alpha = 0;
            loadingImg.color = new Color(1f, 1f, 1f, 0);
            percentMarkTMP.alpha = 0;

        }

        private IEnumerator SetActiveContinuously(bool isActive)
        {
            while (loadingBarChangeCoroutine != null) yield return null;

            float t = isActive ? 0 : 1;
            float onTime = 0.5f;
            float T = 1 / onTime;
            while (true)
            {
                t = isActive ? t + T * Time.deltaTime : t - T * Time.deltaTime;
                loadingTMP.alpha = t;
                loadingImg.color = new Color(1f, 1f, 1f, t);
                percentMarkTMP.alpha = t;
                if (t < 0 || t > 1) break;
                yield return new WaitForSecondsRealtime(Time.deltaTime);
            }

            t = isActive ? 1 : 0;
            loadingTMP.alpha = t;
            loadingImg.color = new Color(1f, 1f, 1f, t);
            percentMarkTMP.alpha = t;

            yield return null;
        }

        private void SetLoadingBarQueue(int targetPer)
        {
            if (targetPer == 0) lastPer = 0;
            ChangeQueue.Enqueue(targetPer);

            if (loadingBarChangeCoroutine == null)
            {
                loadingBarChangeCoroutine = StartCoroutine(LoadingBarChange());
            }

        }

        private IEnumerator LoadingBarChange()
        {
            while (ChangeQueue.Any())
            {
                float targetPer = (float)ChangeQueue.Dequeue();
                float passed = 0;
                while (passed < 0.25f)
                {
                    passed += Time.deltaTime;
                    float t = Mathf.Clamp01(passed * 4);

                    float thisFrame = Mathf.Lerp(lastPer, targetPer, passed);

                    loadingImg.fillAmount = thisFrame / 100;
                    loadingTMP.text = $"{thisFrame:F0}";
                    yield return null;
                }

                lastPer = targetPer;
                loadingImg.fillAmount = targetPer / 100;
                loadingTMP.text = $"{targetPer:F0}";

                yield return new WaitForSeconds(0.1f);
            }

            loadingBarChangeCoroutine = null;
            yield return null;
        }
    }
}
