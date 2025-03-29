using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class RunnigStateForPlayer : HorizontalMovingStateForPlayer
    {
        //private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
        //private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

        public RunnigStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
        }


        protected override void OnWalkToggleInput()
        {
            base.OnWalkToggleInput();

            stateMachine.ChangeState(stateMachine.WalkingState);
        }


        protected override float ApplySpeedModifierToMovementVector()
        {
            return movementStateData.RunningSpeedModifier;
        }

    }
}
