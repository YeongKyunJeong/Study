using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace RSP2
{
    public class MenuUI : MonoBehaviour
    {
        private GameManager gameManager;
        private InGameManager inGameManager;

        private bool isLoading;
        private Coroutine loadingCoroutine;

        public void Initialize(InGameManager _inGameManager)
        {
            inGameManager = _inGameManager;
            gameManager = GameManager.Instance;
            isLoading = false;
        }

        public void SaveCall()
        {
            if (isLoading) return;

            gameManager.InGameSceneSaveCall();
            loadingCoroutine = StartCoroutine(LoadingCoroutine());

        }

        public void LoadCall()
        {
            // TO DO :: Open Save List Windows
        }

        public void TitleCall()
        {
            if (isLoading) return;

            isLoading = true;
            gameManager.InGameSceneTitleCall();
        }

        private IEnumerator LoadingCoroutine()
        {
            isLoading = true;
            yield return new WaitForSeconds(2);

            isLoading = false;
            loadingCoroutine = null;
            yield return null;
        }

    }
}
