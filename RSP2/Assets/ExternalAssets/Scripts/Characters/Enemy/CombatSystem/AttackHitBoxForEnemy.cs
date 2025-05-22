using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class AttackHitBoxForEnemy : AttackHitBox
    {
        private Enemy enemy;
        private bool isInAttacking;

        public event Action<CombatSystem> TargetDetectingEvent;

        protected override void Awake()
        {

        }

        public override void Initialize(LayerMask _targetLayerMask)
        {
            base.Initialize(_targetLayerMask);
            Activate();
            enemy = GetComponentInParent<Enemy>();
            SphereCollider thisCollider = hitBoxCollider as SphereCollider;
            enemy.SetAttackRange(thisCollider.center.z + thisCollider.radius);

            isInAttacking = false;
            enemy.ActionStateMachine.AttackingEvent += OnAttacking;

        }

        protected void OnAttacking(bool isStart)
        {
            isInAttacking = isStart;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (isInAttacking)
            {
                base.OnTriggerEnter(other);
                return;
            }

            // TODO:: Add Attack target detecting logic;
            //TargetDetectingEvent?.Invoke();
            return;

        }

    }
}
