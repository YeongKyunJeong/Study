using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class ChasingState : OnLandStateForEnemy
    {
        private readonly int isChasingHash = Animator.StringToHash("IsChasing");
        private readonly int isMovingHash = Animator.StringToHash("IsMoving");
        private readonly int instantChasingHash = Animator.StringToHash("OnLand.Chasing");

        private bool isMoving;
        private float distance;

        public ChasingState(Enemy _enemy, ActionStateMachineForEnemy _stateMachine) : base(_enemy, _stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            SetAnimatorSelfStateParameter(true);

            moveDir = Vector3.zero;

            isMoving = true;
            SetAnimatorIsMovingParameter(true);
            mover.StartChasing(statHandler.CurrentStatistics.MovementSpeed);

            if (animator.IsInTransition(0))
            {
                animator.CrossFadeInFixedTime(instantChasingHash, 0.25f);
            }
        }

        public override void Exit()
        {
            base.Exit();
            if (isMoving)
            {
                isMoving = false;
                SetAnimatorIsMovingParameter(false);
                mover.StopChasing();
            }
            SetAnimatorSelfStateParameter(false);
        }


        public override void CallUpdate()
        {
            base.CallUpdate();
            runtimeData.VerticalVelocityVector = new Vector3(0, controller.velocity.y, 0);

            if ((runtimeData.Target != null))
            {
                if ((TargetDistanceSqr >= runtimeData.SearchingDistanceSqr * 1.2f) || runtimeData.Target.IsDead)
                    SearchForTarget();
            }

            if (runtimeData.Target == null)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }

            if (runtimeData.IsAttackReady)
            {
                if (DoAttackAndReturn()) return;
            }

            if (TargetDistanceSqr <= runtimeData.MinChasingDistanceSqr)
            {
                if (isMoving)
                {
                    isMoving = false;
                    SetAnimatorIsMovingParameter(false);
                    mover.StopChasing();
                }

                mover.SetOnlyRotateThisFrame();
                return;
            }

            if (isMoving) return; // Already is chasing;

            isMoving = true;
            SetAnimatorIsMovingParameter(true);
            mover.StartChasing(statHandler.CurrentStatistics.MovementSpeed);

            return;
        }

        protected override void SetAnimatorSelfStateParameter(bool isOn)
        {
            if (animator.IsInTransition(0))
            {
                animator.CrossFadeInFixedTime(instantChasingHash, 0.25f);
            }
            animator.SetBool(isChasingHash, isOn);
        }

        protected virtual void SetAnimatorIsMovingParameter(bool isOn)
        {
            float isOnToFloat = 0;
            if (isOn) isOnToFloat = 1;
            animator.SetFloat(isMovingHash, isOnToFloat);
        }

    }
}
