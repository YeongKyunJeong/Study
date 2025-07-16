using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class GameplayInitializer : SceneInitializer
    {
        bool isInitialize = false;

        // 프리팹

        private void Awake()
        {
            if(!isInitialize)
            {
                Initialize();
            }
        }

        public override void Initialize()
        {
            Debug.Log("Gameplay Initialized");
            isInitialize = true;

            // 인게임매니저 탐색

            var gpManager = FindObjectOfType<GamePlayManager>();

#if UNITY_EDITOR
            if (gpManager != null)
            {
                // 없어?
                // 생성 -> 가데이터 셋팅
                // 빈오브젝트 생성 -> 매니저 add componenet
                // 가데이터 초기화
            }
#endif

        }
    }
}
