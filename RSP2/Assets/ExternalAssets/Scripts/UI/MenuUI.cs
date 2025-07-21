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

        public void Initialize(InGameManager _inGameManager)
        {
            inGameManager = _inGameManager;
            gameManager = GameManager.Instance;
        }

        public void SaveCall()
        {
            gameManager.InGameSceneSaveCall();
        }

        public void LoadCall()
        {
            // TO DO :: Open Save List Windows
        }

        public void TitleCall()
        {

        }

    }
}
