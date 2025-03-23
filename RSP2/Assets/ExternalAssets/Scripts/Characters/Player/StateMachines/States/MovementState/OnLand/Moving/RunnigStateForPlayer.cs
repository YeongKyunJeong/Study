using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class RunnigStateForPlayer : HorizontalMovingStateForPlayer
    {
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
