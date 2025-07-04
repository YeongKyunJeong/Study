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
        [field: SerializeField] private PlayerInput PlayerInput { get; set; }

        [field: SerializeField] private CameraManager CameraManager { get; set; }
        [field: SerializeField] private DataManager DataManager { get; set; }
        [field: SerializeField] private ProjectileManager ProjectileManager { get; set; }
        [field: SerializeField] private InteractionManager InteractionManager { get; set; }
        [field: SerializeField] private VFXManager VFXManager { get; set; }
        [field: SerializeField] private SFXManager SFXManager { get; set; }
        [field:SerializeField] private DayNightManager DayNightManager { get; set; }
        
        [field: SerializeField] private CanvasUIManager CanvasUIManager { get; set; }

        public Player Player { get; set; }

        public CinemachineInputProvider CinemachineInputProvider { get; private set; }


        public event Action<Enemy> EnemyDieEvent;

        private void Awake()
        {
            CanvasUIManager = FindObjectOfType<CanvasUIManager>();
            Player = FindObjectOfType<Player>();
            PlayerInput = Player.GetComponent<PlayerInput>();
            CinemachineInputProvider = FindObjectOfType<CinemachineInputProvider>();

            if (CameraManager == null)
            {
                Debug.Log("Camera Manager Not Assigned");
                CameraManager = FindObjectOfType<CameraManager>();
            }
            if (DataManager == null)
            {
                Debug.Log("Data Manager Not Assigned");
                DataManager = FindObjectOfType<DataManager>();
            }
            if (ProjectileManager == null)
            {
                Debug.Log("Projectile Manager Not Assigned");
                ProjectileManager = FindObjectOfType<ProjectileManager>();
            }
            if (InteractionManager == null)
            {
                Debug.Log("Interaction Manager Not Assigned");
                InteractionManager = FindObjectOfType<InteractionManager>();
            }
            if (VFXManager == null)
            {
                Debug.Log("VFX Manager Not Assigned");
                VFXManager = FindObjectOfType<VFXManager>();
            }
            if (SFXManager == null)
            {
                Debug.Log("SFX Manager Not Assigned");
                SFXManager = FindObjectOfType<SFXManager>();
            }
            if (DayNightManager == null)
            {
                Debug.Log("Day Night Manager Not Assigned");
                DayNightManager = FindObjectOfType<DayNightManager>();
            }

            CameraManager.Initialize(this);
            DataManager.Initialize();
            ProjectileManager.Initialize(this);
            InteractionManager.Initialize(this, CameraManager, CanvasUIManager);
            VFXManager.Initialize();
            SFXManager.Initialize();
            DayNightManager.Initialize();

            CanvasUIManager.Initialize(this);

        }

        private void Start()
        {
            LockCursor(true);
        }

        private void Update()
        {
            ProjectileManager.CallUpdate();
        }

        private void FixedUpdate()
        {
            DayNightManager.CallPhysicsUpdate();
        }

        public void OnInventoryUIOpen(bool isOn)
        {
            if (isOn)
            {
                EnableInputActionMap(ActionMap.UI);
            }
            else
            {
                EnableInputActionMap(ActionMap.Field);
            }
            EnableCinemachineInput(!isOn);
            LockCursor(!isOn);
        }

        public void OnInteractionUIOpen(bool isOn)
        {
            if (isOn)
            {
                EnableInputActionMap(ActionMap.Interaction);
            }
            else
            {
                EnableInputActionMap(ActionMap.Field);
            }
            EnableCinemachineInput(!isOn);
        }

        // TO DO :: Standardize by making using Action

        //public bool CheckNowInteractable()
        //{
        //    if (Player.ActionStateMachine.isDead) return false;

        //    if (!Player.ActionStateMachine.isOnLand) return false;

        //    return true;
        //}

        private void EnableInputActionMap(ActionMap targetMap, bool isOnly = true)
        {
            switch (targetMap)
            {
                case ActionMap.Field:
                    {
                        Player?.InputReader.EnableFieldInput(isOnly); break;
                    }
                case ActionMap.UI:
                    {
                        Player?.InputReader.EnableUIInput(isOnly); break;
                    }
                case ActionMap.Interaction:
                    {
                        Player?.InputReader.EnableInteractionInput(isOnly); break;
                    }
            }
        }

        private void EnableCinemachineInput(bool isOn)
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
