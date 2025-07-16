using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public abstract class SceneInitializer : MonoBehaviour
    {
        /// <summary>
        /// 씬 초기화 및 실행 메서드
        /// </summary>
        public abstract void Initialize();
    }
}
