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
        [field: SerializeField] public StatisticsHandlerForPlayer StatisticsHandler { get; private set; }
        [field: SerializeField] public BattleSystemForPlayer BattleSystem { get; private set; }
        [field: SerializeField] public AttackHitBox AttackHitBox { get; private set; }

        
        public Collider AttackHitBoxCollider { get; private set; }
        public MovementStateMachineForPlayer MovementStateMachine { get; private set; }
        public StatisticsForPlayer BaseStatistics { get; private set; }
        public StatisticsForPlayer CurrentStatistics { get; private set; }
        public RuntimeDataForPlayer RuntimeData { get; private set; }

        private void Awake()
        {
            BaseStatistics = new StatisticsForPlayer();
            CurrentStatistics = new StatisticsForPlayer();
            RuntimeData = new RuntimeDataForPlayer();
            MovementStateMachine = new MovementStateMachineForPlayer(this);

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

            if (Animator == null)
            {
                throw new NotImplementedException("Animator Not Assigned");
            }

            if (StatisticsHandler == null)
            {
                throw new NotImplementedException("StatisticsHandler Not Assigned");
            }

            if (BattleSystem == null)
            {
                throw new NotImplementedException("BattleSystem Not Assigned");
            }

            if (AttackHitBox == null)
            {
                throw new NotImplementedException("AttackHitBox Not Assigned");
            }
        }

        private void Start()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }

            BaseStatistics.SetStatisticsFromLoader(gameManager.DataManager.TableDataLoader.StatisticsLoaderForPlayer.GetStatistics());
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
