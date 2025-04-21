using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class AttackStateForPlayer : ActionStateForPlayer
    {
        protected string animatorAttackStateTag = "Attack State";

        protected Vector3 horizontalMomentum;
        //protected float passedTime;
        protected float normalizedPassedTime;

        protected float minimumDuration;
        protected float attackAnimationTime;

        protected bool isCancelable;
        protected bool isAnimationEnd;

        protected readonly int attackHash = Animator.StringToHash("@Attack");

        public AttackStateForPlayer(Player _player, ActionStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {

        }


        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            SetAnimatorIsAttackingParameter(true);

            animator.speed *= attackStateData.BaseAttackData.AttackSpeed;

            horizontalMomentum = runtimeData.HorizontalMovementVector;

            //passedTime = 0;
            isCancelable = false;
            isAnimationEnd = false;
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

            if (!isCancelable)
            {
                isCancelable = CheckIsCancelable();
            }

            horizontalMomentum = CalculateThisUpdateMomentum();

            runtimeData.HorizontalMovementVector = horizontalMomentum;

            mover.UpdateNextHorizontalMovementVector(horizontalMomentum);
        }


        public override void Exit()
        {
            base.Exit();

            animator.speed = 1;

            SetAnimatorIsAttackingParameter(false);
        }

        #endregion


        #region Input Methods

        protected override void OnDashInput()
        {
            if (isCancelable)
            {
                base.OnDashInput();

                // To Do: Check is Landing

                stateMachine.ChangeState(stateMachine.LandDashingState);
            }
        }

        protected override void OnJumpInput()
        {
            if (isCancelable)
            {
                base.OnJumpInput();

                // To Do: Check is Landing

                stateMachine.ChangeState(stateMachine.JumpingState);
                return;
            }

        }

        #endregion


        private void EndAttackState()
        {
            SetAnimatorIsAttackingParameter(false);

            if (CheckIsSlope().y < -0.98) // No collider detected
            {
                stateMachine.ChangeState(stateMachine.FallingState);
                return;
            }

            if (moveInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }

            if (runtimeData.IsWalking)
            {
                stateMachine.ChangeState(stateMachine.WalkingState);
                return;
            }
            stateMachine.ChangeState(stateMachine.RunnigState);
            return;
        }

        protected virtual bool CheckIsCancelable()
        {
            return false;
        }


        protected virtual void UpdateNormalizedPassedTime()
        {
            normalizedPassedTime = GetNormalizedTime(animator, "Attack State");
            if (normalizedPassedTime >= minimumDuration)
            {
                isCancelable = true;
            }
            if (normalizedPassedTime >= 1)
            {
                isAnimationEnd = true;
            }
        }


        protected virtual void SetAnimatorIsAttackingParameter(bool isOn)
        {
            animator.SetBool(attackHash, isOn);
        }


        protected virtual Vector3 CalculateThisUpdateMomentum() { return Vector3.zero; }

        protected override void SetAnimatorSelfStateParameter(bool isOn)
        {
            //base.SetAnimatorSelfStateParameter(isOn);

        }

    }





}
