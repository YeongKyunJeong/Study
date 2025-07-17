using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class TitleScene : MonoBehaviour
    {
        private InGameManager gameManager;

        private void Awake()
        {
            if (gameManager == null)
            {
                gameManager = InGameManager.Instance;
            }
        }

        public void ContinueCall()
        {
            gameManager.TitleSceneContinueCall();
        }

        public void StartCall()
        {
            gameManager.TitleSceneStartCall();
        }
        public void OptionCall()
        {
            // TO DO:: Add option window
        }

        public void QuitCall()
        {
            gameManager.TitleSceneQuitCall();
        }
    }
}
