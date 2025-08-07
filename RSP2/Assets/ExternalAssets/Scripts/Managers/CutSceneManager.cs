using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]
    public class ProgressCutSceneKeyPair
    {
        public int Progress;
        public int CutsceneKey;
    }

    public class CutsceneManager : MonoBehaviour
    {


        private InGameManager inGameManager;

        [field: SerializeField] private List<ProgressCutSceneKeyPair> progressCutsceneKeyPairs;

        [field: SerializeField] private List<Cutscene> cutScenes;

        public void Initialize(InGameManager _inGameManager)
        {
            inGameManager = _inGameManager;
        }

        public void CallCutsceneStart(int progress)
        {
            if (progressCutsceneKeyPairs == null) return;

            if (progressCutsceneKeyPairs.Count == 0) return;
        }

        public void PlayerCutscene(int newProgress)
        {
            foreach (ProgressCutSceneKeyPair keyPair in progressCutsceneKeyPairs)
            {
                if (keyPair.Progress == newProgress)
                {
                    cutScenes[keyPair.CutsceneKey].Play();
                }
            }
        }
    }

}
