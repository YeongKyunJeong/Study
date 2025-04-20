using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RSP2
{
    public class ActionStateForEnemy : IState
    {
        protected Enemy enemy;

        protected ActionStateMachineForEnemy stateMachine;
        protected EnemyMover mover;
        protected CharacterController controller;
        protected Animator animator;

        private Transform enemyTransform;

        private Collider[] hitColliders;
        private CombatSystem detectedCombatSystem;
        protected Vector3 moveDir;

        protected readonly int inAirHash = Animator.StringToHash("@InAir");
        protected readonly int attackHash = Animator.StringToHash("@Attack");

        public ActionStateForEnemy(Enemy _enemy, ActionStateMachineForEnemy _stateMachine)
        {
            enemy = _enemy;

            stateMachine = _stateMachine;
            mover = enemy.Mover;
            controller = enemy.Controller;
            animator = enemy.Animator;

            enemyTransform = enemy.transform;
        }

        #region IState Methods

        public virtual void Enter()
        {
            //moveInput = runtimeData.MoveInput;
        }

        public virtual void Exit()
        {

        }

        public virtual void CallUpdate()
        {
        }

        public virtual void CallPhysicsUpdate()
        {
        }


        public virtual void OnAnimationEnterEvent()
        {
        }

        public virtual void OnAnimationExitEvent()
        {
        }

        public virtual void OnAnimationTransitEvent()
        {
        }

        #endregion

        protected virtual void SetAnimatorSelfStateParameter(bool isOn) { }

        protected bool SearchForTaget()
        {
            // To Do : Save result and return that if called more than once within one frame

            hitColliders = Physics.OverlapSphere(enemyTransform.position,
                enemy.SearchingDistance, enemy.SearchingLayerMask);

            foreach (Collider hit in hitColliders)
            {
                detectedCombatSystem = hit.GetComponent<CombatSystem>();

                if (detectedCombatSystem != null
                    /*&& !detectedCombatSystem.IsDead*/
                    && detectedCombatSystem.MyFaction != enemy.CombatSystem.MyFaction)
                {
                    Debug.Log($"Target detected : {detectedCombatSystem.name}");
                    enemy.Target = detectedCombatSystem;
                    return true;
                }

            }

            enemy.Target = null;
            return false;
        }

        protected bool IsInAttackRange()
        {
            if (enemy.Target == null) return false;

            if (enemy.Target.IsDead) return false;

            //if (stateMachine.CurrentAttackInfo == null)
            //    SelectAttack();

            float playerDistanceSqr = (enemy.Target.transform.position - enemy.transform.position).sqrMagnitude;

            //switch (stateMachine.CurrentAttackInfo.DetectionType)
            //{
            //    case DetectionType.WeaponCollider:
            //        return playerDistanceSqr <= 1.5f;

            //    case DetectionType.BoxCast:
            //        return playerDistanceSqr <= stateMachine.CurrentAttackInfo.BoxCastSize.z * stateMachine.CurrentAttackInfo.BoxCastSize.z;
            //}

            return false;
        }

        protected virtual bool IsInSight()
        {
            if (enemy.Target == null) return false;

            if (enemy.Target.IsDead) return false;

            Vector3 directionToTarget = enemy.Target.transform.position - enemy.transform.position;
            directionToTarget.y = 0;
            directionToTarget.Normalize();

            Vector3 forward = enemy.transform.forward;
            forward.y = 0;
            forward.Normalize();

            float angleToTaget = Vector3.Angle(forward, directionToTarget);


            if (angleToTaget <= enemy.FieldOfView / 2f)
            {
                return true;
            }

            return false;
        }


    }
}
