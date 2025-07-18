using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class TitleManager : MonoBehaviour
    {
        private GameManager gameManager;

        public void Initialize()
        {
            gameManager = GameManager.Instance;
            // TO DO 
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
