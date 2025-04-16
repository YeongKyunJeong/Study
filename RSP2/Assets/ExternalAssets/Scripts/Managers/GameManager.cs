using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class GameManager : MonoSingleton<GameManager>
    {

        [SerializeField] private static GameObject gameManagerPrefab;

        public DataManager DataManager { get; private set; }

        private void Awake()
        {
            DataManager = DataManager.Instance;

            DataManager.Initialize();
        }


    }
}
