using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP2
{
    public class GameManager : MonoSingleton<GameManager>
    {

        [field: SerializeField] private static GameObject gameManagerPrefab { get; set; }
        public Player Player { get; set; }
        [field: SerializeField] private PlayerInput PlayerInput { get; set; }
        public CinemachineInputProvider CinemachineInputProvider { get; private set; }
        public CanvasUIManager CanvasUIManager { get; private set; }
        public DataManager DataManager { get; private set; }
        public ProjectileManager ProjectileManager { get; private set; }
        public InteractionManager InteractionManager { get; private set; }

        public event Action<Enemy> EnemyDieEvent;

        private void Awake()
        {
            Player = FindObjectOfType<Player>();
            PlayerInput = Player.GetComponent<PlayerInput>();
            CinemachineInputProvider = FindObjectOfType<CinemachineInputProvider>();
            CanvasUIManager = FindObjectOfType<CanvasUIManager>();

            DataManager = DataManager.Instance;
            ProjectileManager = ProjectileManager.Instance;
            InteractionManager = InteractionManager.Instance;

            CanvasUIManager.Initialize(this);
            DataManager.Initialize();
            InteractionManager.Initialize(this);
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
