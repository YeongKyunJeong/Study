using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class OnLandStateForPlayer : ActionStateForPlayer
    {
        //protected int isFallingCount;
        protected int fallingThresholdCount = 5;
        protected float fallingThreshold;
        protected Vector3 slopeNormalVector;

        protected readonly int onLandHash = Animator.StringToHash("@OnLand");



        public OnLandStateForPlayer(Player _player, ActionStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
            //isFallingCount = 0;
            //fallingThreshold = Physics.gravity.y * movementStateData.FallingThreshoildMultiplier;

        }


        #region IStateMethods

        public override void Enter()
        {
            base.Enter();

            SetAnimatorOnLandParameter(true);
        }

        public override void Exit()
        {
            base.Exit();

            SetAnimatorSelfStateParameter(false);
        }

        #endregion


        protected override void OnJumpInput()
        {
            base.OnJumpInput();

            SetAnimatorOnLandParameter(false);

            stateMachine.ChangeState(stateMachine.JumpingState);
        }

        protected override void OnDashInput()
        {
            base.OnDashInput();

            stateMachine.ChangeState(stateMachine.LandDashingState);
        }

        protected override void OnAttackInput()
        {
            base.OnAttackInput();

            SetAnimatorOnLandParameter(false);

            stateMachine.ChangeState(stateMachine.BasicMeleeAttackingState);
        }

        protected virtual void SetAnimatorOnLandParameter(bool isOn)
        {
            animator.SetBool(onLandHash, isOn);
        }






    }
}
