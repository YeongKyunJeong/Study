using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance { get { return instance; } private set { instance = value; } }

        private static StageManager stageManager;
        public static StageManager StageManager { get { return stageManager; } private set { stageManager = value; } }

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

            if(StageManager == null)
            {
                StageManager = FindObjectOfType<StageManager>();
            }
            StageManager.Initialize();
        }


    }
}
