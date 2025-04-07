using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class Player : MonoBehaviour
    {
        private GameManager gameManager;
        [field: SerializeField] public PlayerInputReader InputReader { get; private set; }
        [field: SerializeField] public PlayerMover Mover { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Transform MainCameraTransform { get; private set; }
        [field: SerializeField] public PlayerScriptableObject SOData { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Collider AttackHitBox { get; private set; }

        public MovementStateMachineForPlayer MovementStateMachine { get; private set; }
        public StatisticsForPlayer Statistics { get; private set; }
        public RuntimeDataForPlayer RuntimeData { get; private set; }

        private void Awake()
        {
            RuntimeData = new RuntimeDataForPlayer();
            MovementStateMachine = new MovementStateMachineForPlayer(this);
            Statistics = new StatisticsForPlayer(this);

            if (SOData == null)
            {
                throw new NotImplementedException("Player Scriptable Object Not Assigned");
            }

            if (InputReader == null)
            {
                throw new NotImplementedException("Player Input Reader Not Assigned");
            }
            InputReader.Initialize(this);

            if (Mover == null)
            {
                throw new NotImplementedException("Player Input Reader Not Assigned");
            }
            Mover.Initialize();

            if (Controller == null)
            {
                throw new NotImplementedException("Character Controller Not Assigned");
            }

            if (MainCameraTransform == null)
            {
                throw new NotImplementedException("Main Camera Transform Not Assigned");
            }

            if (AttackHitBox == null)
            {
                throw new NotImplementedException("Main Camera Transform Not Assigned");
            }
            AttackHitBox.enabled = false;
        }

        private void Start()
        {
            if (gameManager == null)
            {
                if (GameManager.Instance == null)
                {
                    // Instantiate if there is no GameManager
                    GameManager.InstantiateGameManager();

                }

                gameManager = GameManager.Instance;
            }

        }

        private void Update()
        {
            MovementStateMachine.CallUpdate();
            Mover.CallUpdate();
        }

        private void FixedUpdate()
        {
            MovementStateMachine.CallPhysicsUpdate();
            Mover.CallFixedUpdate();
        }

    }

}
