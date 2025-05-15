using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class Enemy : CombatUnit
    {
        private GameManager gameManager;
        [field: SerializeField] public MoverForEnemy Mover { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public StatHandlerForEnemy StatHandler { get; private set; }
        [field: SerializeField] public CombatSystemForEnemy CombatSystem { get; private set; }
        [field: SerializeField] public AttackHitBoxForEnemy AttackHitBox { get; private set; }

        public ActionStateMachineForEnemy ActionStateMachine { get; private set; }


        // To do : Move these parameter to SO and RuntimeData
        public int EnemyKey;

        [field: SerializeField] public AudioClip attackSound;
        [field: SerializeField] public float SearchingDistance { get; private set; }
        public float SearchingDistanceSqr { get; private set; }
        [field: SerializeField] public LayerMask SearchingLayerMask { get; private set; }
        [field: SerializeField] public float FieldOfView { get; private set; }

        //[field: SerializeField][field: Range(0f, 25f)] public float ChasingSpeedModifier { get; private set; } = 4f;
        [field: SerializeField][field: Range(0f, 25f)] public float RotationSpeedModifier { get; private set; } = 6;
        public float AttackRange { get; set; }
        public float AttackRangeSqr { get; private set; }
        public ChasingTargetTpye ChasingTargetType = ChasingTargetTpye.PlayerOnly;

        public Vector3 AttackPositionModifier { get; private set; }

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

            if (StatHandler == null)
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
            AttackPositionModifier = new Vector3(0, AttackHitBox.HitBoxCollider.bounds.center.y, 0);
        }

        private void Start()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }

            //StatisticsHandler.InitializeByDefault();
            StatHandler.Initialize(gameManager.DataManager.TableDataLoader.StatLoaderForEnemy.GetByKey(2));

            CombatSystem.DamageEvent += OnHit;
            CombatSystem.DieEvent += OnDie;

            SearchingDistanceSqr = SearchingDistance * SearchingDistance;
        }

        private void Update()
        {
            ActionStateMachine.CallUpdate();
            Mover.CallUpdate();
        }

        public void SetAttackRange(float range)
        {
            AttackRange = range;
            AttackRangeSqr = range * range;
        }

        private void OnHit()
        {
            ActionStateMachine.OnHit();
        }

        private void OnDie()
        {
            ActionStateMachine.OnDie();
        }

    }
}
