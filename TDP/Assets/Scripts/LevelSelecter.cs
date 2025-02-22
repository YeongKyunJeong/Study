using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{
    public class LevelSelecter : MonoBehaviour
    {
        public SceneFader sceneFader;

        public void Select(int targetLevel)
        {
            sceneFader.FadeTo(SceneType.Stage, targetLevel);
        }

    }
}
