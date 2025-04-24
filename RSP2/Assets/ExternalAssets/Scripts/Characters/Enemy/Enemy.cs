using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class Enemy : MonoBehaviour
    {
        private GameManager gameManager;
        [field: SerializeField] public EnemyMover Mover { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public StatisticsHandlerForEnemy StatisticsHandler { get; private set; }
        [field: SerializeField] public CombatSystemForEnemy CombatSystem { get; private set; }
        [field: SerializeField] public AttackHitBox AttackHitBox { get; private set; }

        public ActionStateMachineForEnemy ActionStateMachine { get; private set; }

        // To do : Move these parameter to SO and RuntimeData
        [field: SerializeField] public float SearchingDistance { get; private set; }
        [field: SerializeField] public LayerMask SearchingLayerMask { get; private set; }
        [field: SerializeField] public float FieldOfView { get; private set; }

        [field: SerializeField][field: Range(0f, 25f)] public float ChasingSpeedModifier { get; private set; } = 4f;
        [field: SerializeField][field: Range(0f, 25f)] public float RotationSpeedModifier { get; private set; } = 6;

        public CombatSystem Target { get; set; }


        private void Awake()
        {
            ActionStateMachine = new ActionStateMachineForEnemy(this);

            if (Mover == null)
            {
                throw new NotImplementedException("Enemy Input Reader Not Assigned");
            }
            Mover.Initialize(this);

            if (Controller == null)
            {
                throw new NotImplementedException("Enemy Character Controller Not Assigned");
            }

            if (Animator == null)
            {
                throw new NotImplementedException("Enemy Animator Not Assigned");
            }

            if (StatisticsHandler == null)
            {
                throw new NotImplementedException("Enemy StatisticsHandler Not Assigned");
            }

            if (CombatSystem == null)
            {
                throw new NotImplementedException("Player CombatSystem Not Assigned");
            }

            if (AttackHitBox == null)
            {
                throw new NotImplementedException("Player AttackHitBox Not Assigned");
            }
            AttackHitBox.Initialize(1 << LayerMask.NameToLayer("Combat Unit"));

        }

        private void Start()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }

            StatisticsHandler.InitializeByDefault();
        }

        private void Update()
        {
            ActionStateMachine.CallUpdate();
            Mover.CallUpdate();
        }

    }
}
