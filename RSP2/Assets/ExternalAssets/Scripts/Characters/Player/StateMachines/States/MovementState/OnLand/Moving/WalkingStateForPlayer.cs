using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class WalkingStateForPlayer : HorizontalMovingStateForPlayer
    {
        public WalkingStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
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
    }
}
