using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]
    public class ProgressCutSceneKeyPair
    {
        public readonly int Progress;
        public readonly int CutSceneKey;
    }

    public class CutSceneManager : MonoBehaviour
    {
        private readonly int instantIdlingUpHash = Animator.StringToHash("CutScene.IdlingUp");
        private readonly int instantWalkingUpHash = Animator.StringToHash("CutScene.WalkingUp");
        private readonly int instantLayingHash = Animator.StringToHash("CutScene.Laying");
        private readonly int instantStandingUpHash = Animator.StringToHash("CutScene.StandingUp");

        [field: SerializeField] private List<ProgressCutSceneKeyPair> progressCutSceneKeyPairs;

        [field: SerializeField] private List<CutScene> cutScenes;


    }
}
