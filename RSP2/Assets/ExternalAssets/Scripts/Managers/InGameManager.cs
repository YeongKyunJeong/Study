using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace RSP2
{
    public enum SceneType
    {
        None,
        TitleScene,
        GameScene
    }

    public class InGameManager : MonoSingleton<InGameManager>
    {
        //[field: SerializeField] private SceneInitializer sceneInitializer;

        //public bool IsInitialized { get; private set; }

        //[field: SerializeField] private SceneType currentSceneType { get; set; }
        [field: SerializeField] public bool IsInitialized { get; private set; }
        //public const string TITLE_SCENE_NAME_STR = "TitleScene";
        //public const string GAME_SCENE_NAME_STR = "GameScene";

        [field: SerializeField] private PlayerInput PlayerInput { get; set; }

        [field: SerializeField] private CameraManager CameraManager { get; set; }
        [field: SerializeField] private DataManager DataManager { get; set; }
        [field: SerializeField] private ProjectileManager ProjectileManager { get; set; }
        [field: SerializeField] private InteractionManager InteractionManager { get; set; }
        [field: SerializeField] private VFXManager VFXManager { get; set; }
        [field: SerializeField] private SFXManager SFXManager { get; set; }
        [field: SerializeField] private DayNightManager DayNightManager { get; set; }
        [field: SerializeField] private QuestManager QuestManager { get; set; }

        [field: SerializeField] private CanvasUIManager CanvasUIManager { get; set; }

        public Player Player { get; set; }

        public CinemachineInputProvider CinemachineInputProvider { get; set; }


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
            if (QuestManager == null)
            {
                Debug.Log("Quest Manager Not Assigned");
                QuestManager = FindObjectOfType<QuestManager>();
            }

            CameraManager.Initialize(this);
            DataManager.Initialize();
            ProjectileManager.Initialize(this);
            InteractionManager.Initialize(this, CameraManager, CanvasUIManager);
            VFXManager.Initialize(this);
            SFXManager.Initialize(this);
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
            //if (!IsInitialized) return;

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

        //private void CheckScene()
        //{
        //    string sceneName = SceneManager.GetActiveScene().name;
        //    switch (sceneName)
        //    {
        //        case TITLE_SCENE_NAME_STR:
        //            {
        //                IsInitialized = false;
        //                break;
        //            }
        //        default:
        //            {
        //                if (sceneName.Contains(GAME_SCENE_NAME_STR))
        //                {
        //                    IsInitialized = true;
        //                }
        //                else
        //                {
        //                    Debug.LogError("Scene Name Is Not Correct");
        //                    IsInitialized = false;
        //                }
        //                break;
        //            }
        //    }
        //}

        public PlayerSaveData GetCurrentDataToSave()
        {
            PlayerSaveData newSaveData = new PlayerSaveData();

            Player.GetPlayerDataForSave(newSaveData);
            QuestManager.GetQuestDataForSave(newSaveData);
            // TO DO:: NPCs Data

            return newSaveData;
        }

        public void GetDataFromSave(int userNumber = 0, int saveNumber = -1)
        {
            PlayerSaveData loadedSaveData = DataManager.CallLoadingSaveData(userNumber, saveNumber);

            Player.SetPlayerDataFromSave(loadedSaveData);
            QuestManager.SetQuestDataFromSave(loadedSaveData);
            // TO DO:: NPCs Data
        }

        public void TitleSceneContinueCall()
        {

        }

        public void TitleSceneStartCall()
        {

        }

        public void TitleSceneQuitCall()
        {

        }

        public void QuitGame()
        {
            // TO DO:: Add Game Save Logic

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
