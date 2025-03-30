using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class WalkingStateForPlayer : HorizontalMovingStateForPlayer
    {

        protected readonly int isWalkingHash = Animator.StringToHash("IsWalking");

        public WalkingStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            SetAnimatorSelfStateParameter(true);
        }

        public override void Exit()
        {
            base.Exit();

            SetAnimatorSelfStateParameter(false);
        }

        protected override void OnWalkToggleInput()
        {
            base.OnWalkToggleInput();

            stateMachine.ChangeState(stateMachine.RunnigState);
        }


        protected override float ApplySpeedModifierToMovementVector()
        {
            return movementStateData.WalkingSpeedModifier;
        }

        protected override void SetAnimatorSelfStateParameter(bool isOn)
        {
            //base.SetAnimatorSelfStateParameter(isOn);

            animator.SetBool(isWalkingHash, isOn);
        }
    }
}
