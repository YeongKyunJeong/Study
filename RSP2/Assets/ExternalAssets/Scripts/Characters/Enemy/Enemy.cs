using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class Enemy : CombatUnit
    {
        protected GameManager gameManager;
        [field: SerializeField] public MoverForEnemy Mover { get; private set; }
        [field: SerializeField] public ForceReceiverForEnemy ForceReceiver { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; protected set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public StatHandlerForEnemy StatHandler { get; private set; }
        [field: SerializeField] public CombatSystemForEnemy CombatSystem { get; private set; }
        [field: SerializeField] public AttackHitBoxForEnemy AttackHitBox { get; private set; }

        public ActionStateMachineForEnemy ActionStateMachine { get; protected set; }

        public string Name;
        public int EnemyKey;

        [field: SerializeField] public AttackData[] AttackDataArray { get; protected set; }
        public RuntimeDataForEnemy RuntimeData { get; private set; }
        [field: SerializeField] protected float searchingDistance { get; set; }
        [field: SerializeField] public LayerMask SearchingLayerMask { get; protected set; }
        [field: SerializeField] public float FieldOfView { get; protected set; }


        [field: SerializeField][field: Range(0f, 25f)] public float RotationSpeedModifier { get; protected set; } = 6;



        protected virtual void Awake()
        {
            RuntimeData = new RuntimeDataForEnemy();
            ActionStateMachine = new ActionStateMachineForEnemy(this);

            if (Mover == null)
            {
                throw new NotImplementedException("Enemy Mover Not Assigned");
            }
            Mover.Initialize(this);

            if (ForceReceiver == null)
            {
                throw new NotImplementedException("Enemy ForceReceiver Not Assigned");
            }

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
            RuntimeData.AttackPositionModifier = new Vector3(0, AttackHitBox.HitBoxCollider.bounds.center.y, 0);
        }

        protected virtual void Start()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }

            //StatisticsHandler.InitializeByDefault();
            StatHandler.Initialize(gameManager.DataManager.TableDataLoader.StatLoaderForEnemy.GetByKey(EnemyKey));

            CombatSystem.DamageEvent += OnHit;
            CombatSystem.DieEvent += OnDie;

            RuntimeData.SearchingDistance = searchingDistance;
            RuntimeData.SearchingDistanceSqr = searchingDistance * searchingDistance;

            RuntimeData.IsHostile = true;
        }

        private void Update()
        {
            ActionStateMachine.CallUpdate();
            ForceReceiver.CallUpdate();
            Mover.CallUpdate();
        }

        public void SetAttackRange(float range)
        {
            RuntimeData.AttackRange = range;
            RuntimeData.AttackRangeSqr = range * range;
        }

        protected void OnHit()
        {
            ActionStateMachine.OnHit();
        }

        protected void OnDie()
        {
            ActionStateMachine.OnDie();
            gameManager.EnemyDie(this);
            this.enabled = false;
            Controller.enabled = false;
            AttackHitBox.Deactivate();
        }

    }
}
