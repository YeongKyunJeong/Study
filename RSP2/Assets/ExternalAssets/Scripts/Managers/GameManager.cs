using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class GameManager : MonoSingleton<GameManager>
    {

        [SerializeField] private static GameObject gameManagerPrefab;
        public Player Player { get; set; }
        public CinemachineInputProvider CinemachineInputProvider { get; private set; }
        public CanvasUIManager CanvasUIManager { get; private set; }
        public DataManager DataManager { get; private set; }
        public ProjectileManager ProjectileManager { get; private set; }

        public event Action<Enemy> EnemyDieEvent; 

        private void Awake()
        {
            Player = FindObjectOfType<Player>();
            CinemachineInputProvider = FindObjectOfType<CinemachineInputProvider>();
            CanvasUIManager = FindObjectOfType<CanvasUIManager>();

            DataManager = DataManager.Instance;
            ProjectileManager = ProjectileManager.Instance;

            CanvasUIManager.Initialize(this);
            DataManager.Initialize();
        }

        private void Start()
        {
            LockCursor(true);
        }

        private void Update()
        {
            ProjectileManager.CallUpdate();
        }

        public void OnInventoryUIOpen(bool isOn)
        {
            EnablePlayerInput(!isOn);
            EnableCinemachinInput(!isOn);
            LockCursor(!isOn);
        }

        private void EnablePlayerInput(bool isOn)
        {
            Player.InputReader.EnablePlayerInput(isOn);
        }

        private void EnableCinemachinInput(bool isOn)
        {
            CinemachineInputProvider.enabled = isOn;
        }

        public void LockCursor(bool isLock)
        {
            if (isLock)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
        
        public void EnemyDie(Enemy diedEnemy)
        {
            EnemyDieEvent?.Invoke(diedEnemy);
        }
    }
}
