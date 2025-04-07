using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class GameManager : MonoBehaviour // Use MonoSingleton
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private static GameObject gameManagerPrefab;

        public DataManager DataManager { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            DataManager = new DataManager();

            DataManager.Initialize();
        }

        public static void InstantiateGameManager()
        {
            if (Instance == null)
            {
                Instance = Instantiate(gameManagerPrefab).GetComponent<GameManager>();
            }

            return ;
        }

    }
}
