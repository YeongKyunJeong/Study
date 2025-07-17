using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [field: SerializeField] private InGameInitializer inGameInitializer { get; set; }



        private void Awake()
        {
            if (Instance == null) { }// Always false by MonoSingleton

            DontDestroyOnLoad(gameObject);
        }

        // To Do :: Scene Change Logic

        private void InitializeScene()
        {
            SceneInitializer sceneInitializer = FindObjectOfType<SceneInitializer>();
            if (sceneInitializer != null)
            {
                sceneInitializer.Initialize();
                return;
            }
            else
            {
                Debug.LogWarning("No Scene Initializer Found in This Scene");
            }

        }

    }
}
