using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace RSP2
{
    public class BaseAttackingStateForEnemy : ActionStateForEnemy
    {
        protected readonly int attackHash = Animator.StringToHash("@Attack");
        protected string animatorAttackStateTag = "Attack State";
        private readonly int instantAttackHash = Animator.StringToHash("Attack.BaseAttack");
        //private readonly int isAttackingHash = Animator.StringToHash("isAttacking");



        protected float normalizedPassedTime;

        protected CombatSystem combatSystem;


        protected bool isAnimationEnd;

        public BaseAttackingStateForEnemy(Enemy _enemy, ActionStateMachineForEnemy _stateMachine) : base(_enemy, _stateMachine)
        {
            combatSystem = _enemy.CombatSystem;


        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.IsInAttackingState = true;

            SetAnimatorIsAttackingParameter(true);
            SetAnimatorSelfStateParameter(true);
            SetAnimatorPlayingSpeed();

            mover.UpdateNextHorizontalMovementVector(Vector3.zero);

            isAnimationEnd = false;
        }

        public override void Exit()
        {
            base.Exit();

            stateMachine.IsInAttackingState = false;

            SetAnimatorIsAttackingParameter(false);
            //SetAnimatorSelfStateParameter(false);
            SetAnimatorPlayingSpeed(true);

        }


        public override void CallUpdate()
        {
            base.CallUpdate();

            UpdateNormalizedPassedTime();

            if (isAnimationEnd)
            {
                EndAttackState();
                return;
            }

            //horizontalMomentum = CalculateThisUpdateMomentum();

            //runtimeData.HorizontalMovementVector = horizontalMomentum;

            //mover.UpdateNextHorizontalMovementVector(horizontalMomentum);
        }

        protected virtual void UpdateNormalizedPassedTime()
        {
            normalizedPassedTime = GetNormalizedTime(animator, animatorAttackStateTag);
            //if (normalizedPassedTime >= minimumDuration)
            //{
            //    isCancelable = true;
            //}
            //if (normalizedPassedTime >= 1)
            //{
            //    isAnimationEnd = true;
            //}
        }


        private void EndAttackState()
        {
            //if (CheckIsSlope().y < -0.98) // No collider detected
            //{
            //    stateMachine.ChangeState(stateMachine.FallingState);
            //    return;
            //}

            if (SearchForTaget())
            {
                if (IsInAttackRange())
                {
                    stateMachine.ChangeState(stateMachine.AttackingState);
                    return;
                }

                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }

            stateMachine.ChangeState(stateMachine.IdlingState);
            return;

        }

        protected override void SetAnimatorSelfStateParameter(bool isOn)
        {
            //base.SetAnimatorSelfStateParameter(isOn);
            if (animator.IsInTransition(0))
            {
                animator.CrossFadeInFixedTime(instantAttackHash, 0.25f);
            }
        }

        protected virtual void SetAnimatorIsAttackingParameter(bool isOn)
        {
            animator.SetBool(attackHash, isOn);
        }

        protected virtual Vector3 CalculateThisUpdateMomentum() { return Vector3.zero; }

        protected virtual void SetAnimatorPlayingSpeed(bool isExit = false)
        {
            if (isExit)
            {
                animator.speed = 1;
                return;
            }

            animator.speed = enemy.StatisticsHandler.CurrentStatistics.AttackSpeed / 5;
            return;
        }

        protected virtual void SetHitBoxShape(bool isExit = false)
        {
            if (isExit)
            {
                // TODO:: Reset collider size;
                return;
            }
            // TODO:: Add changing collider size logic

            // TODO:: Add other shape collider case
            //switch (attackData.DetectionType)
            //{
            //    case DetectionType.SphereCollider:
            //        {
            //            SphereCollider sphereCollider = attackHitBox.HitBoxCollider as SphereCollider;
            //            sphereCollider.radius = attackData.ColliderSize.x * currentWeapon.WeaponData.RangeModifier;
            //            sphereCollider.center = attackData.ColliderPosition;

            //            break;
            //        }

            //    case DetectionType.BoxCollider:
            //        {
            //            BoxCollider sphereCollider = attackHitBox.HitBoxCollider as BoxCollider;
            //            sphereCollider.size = attackData.ColliderSize;
            //            sphereCollider.center = attackData.ColliderPosition;
            //            break;
            //        }

            //    default:
            //        break;
            //}
        }
    }
}
