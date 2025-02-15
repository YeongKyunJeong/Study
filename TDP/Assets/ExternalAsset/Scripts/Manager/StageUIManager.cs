using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{

    public class StageUIManager : MonoBehaviour
    {
        [SerializeField] StageUITMPSetter stageUITMPSetter;
        [SerializeField] AspectRatioEnforcer aspectRatioEnforcer;

        public void Initialize()
        {
            if (aspectRatioEnforcer == null)
            {
                aspectRatioEnforcer = GetComponent<AspectRatioEnforcer>();
            }

            aspectRatioEnforcer.Initialize();
        }

        public void ChangeValue(StageUITMPType tagetTMP, float targetValue)
        {
            stageUITMPSetter.ChangeValue(tagetTMP, targetValue);
        }

        public void ChangeValue(StageUITMPType tagetTMP, int targetValue)
        {
            stageUITMPSetter.ChangeValue(tagetTMP, targetValue);
        }
    }
}
