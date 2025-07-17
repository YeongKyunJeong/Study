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
        [field: SerializeField] private static GameObject GameManagerPrefab { get; set; }
        [field: SerializeField] private GameObject InGameManagerPrefab { get; set; }

        [field: SerializeField] private InGameManager InGameManager { get; set; }


        [field: SerializeField] private CameraManager CameraManager { get; set; }
        [field: SerializeField] private DataManager DataManager { get; set; }
        [field: SerializeField] private ProjectileManager ProjectileManager { get; set; }
        [field: SerializeField] private InteractionManager InteractionManager { get; set; }
        [field: SerializeField] private VFXManager VFXManager { get; set; }
        [field: SerializeField] private SFXManager SFXManager { get; set; }
        [field: SerializeField] private DayNightManager DayNightManager { get; set; }
        [field: SerializeField] private QuestManager QuestManager { get; set; }
        [field: SerializeField] private CanvasUIManager CanvasUIManager { get; set; }

        [field: SerializeField] private PlayerInput PlayerInput { get; set; }
        public Player Player { get; set; }
        public CinemachineInputProvider CinemachineInputProvider { get; set; }


        private void Awake()
        {
            if (!isInitialize) Initialize();
        }

        public override void Initialize()
        {
            isInitialize = true;


#if UNITY_EDITOR
            if (InGameManager == null)
            {
                Debug.Log("In Game Manager Not Assigned");
                InGameManager = FindObjectOfType<InGameManager>();

                if (InGameManager != null)
                {
                    // TO DO :: Instantiate Prefab
                }
            }

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
            if (CanvasUIManager == null)
            {
                Debug.Log("Canvas UI Manager Not Assigned");
                CanvasUIManager = FindObjectOfType<CanvasUIManager>();
            }

            if (Player == null)
            {
                Debug.Log("Player Not Assigned");
                Player = FindObjectOfType<Player>();
            }
            if (PlayerInput == null)
            {
                Debug.Log("Player Input Not Assigned");
                {
                    PlayerInput = FindObjectOfType<PlayerInput>();
                }
            }
            if (PlayerInput == null)
            {
                Debug.Log("Player Input Not Assigned");
                {
                    PlayerInput = FindObjectOfType<PlayerInput>();
                }
            }
            if (CinemachineInputProvider == null)
            {
                Debug.Log("Cinemachine Input Provider Not Assigned");
                CinemachineInputProvider= FindObjectOfType<CinemachineInputProvider>();
            }
#endif

            CameraManager.Initialize(InGameManager);
            DataManager.Initialize();
            ProjectileManager.Initialize(InGameManager);
            InteractionManager.Initialize(InGameManager, CameraManager, CanvasUIManager);
            VFXManager.Initialize(InGameManager);
            SFXManager.Initialize(InGameManager);
            DayNightManager.Initialize();
            QuestManager.Initialize(InGameManager);
            CanvasUIManager.Initialize(InGameManager);



        }
    }
}
