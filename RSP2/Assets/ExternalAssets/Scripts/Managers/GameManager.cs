using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
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
        }
    }
}
