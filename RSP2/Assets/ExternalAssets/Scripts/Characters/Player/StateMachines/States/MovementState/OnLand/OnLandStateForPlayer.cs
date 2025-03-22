using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class OnLandStateForPlayer : MovementStateForPlayer
    {
        public OnLandStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
        }

        public override void CallUpdate()
        {
            base.CallUpdate();
        }

        public override void CallPhysicsUpdate()
        {
            base.CallPhysicsUpdate();
        }

        protected override void OnMoveInput(Vector2 moveInput)
        {
            base.OnMoveInput(moveInput);
        }

        protected override void OnJumpInput()
        {
            base.OnJumpInput();
        }

    }
}
