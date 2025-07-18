using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP2
{
    public class InGameInitializer : SceneInitializer
    {
        private bool isInitialize = false;
        [field: SerializeField] private static GameObject gameManagerPrefab { get; set; }
        [field: SerializeField] private GameObject inGameManagerPrefab { get; set; }

        [field: SerializeField] private InGameManager inGameManager { get; set; }

        private void Awake()
        {
            if (!isInitialize) Initialize();
        }

        public override void Initialize()
        {
            isInitialize = true;

#if UNITY_EDITOR
            if (GameManager.Instance == null)
            {
                Instantiate(gameManagerPrefab);
            }

            if (inGameManager == null)
            {
                Debug.Log("In Game Manager Not Assigned");
                inGameManager = FindObjectOfType<InGameManager>();

                if (inGameManager == null)
                {
                    Debug.LogWarning("In Game Manager Not Exists");
                    // TO DO :: Instantiate Prefab
                }

                inGameManager = Instantiate(inGameManagerPrefab).GetComponent<InGameManager>();
            }
#endif

            inGameManager.Initialize();

        }
    }
}
