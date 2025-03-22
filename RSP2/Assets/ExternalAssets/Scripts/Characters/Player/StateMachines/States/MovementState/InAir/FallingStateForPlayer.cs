using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class FallingStateForPlayer : InAirStateForPlayer
    {
        public FallingStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
            verticalVelocityVector =  Vector3.zero;

        }

        public override void CallPhysicsUpdate()
        {
            base.CallPhysicsUpdate();

            if (isFirstFixedUpdate)
            {
                isFirstFixedUpdate = false;
            }
            else
            {
                ApplyFallingToVector(ref verticalVelocityVector, Time.fixedDeltaTime);
            }

            runtimeData.VerticalVelocityVector = verticalVelocityVector;

            mover.UpdateNextHorizontalMovementVector(horizontalMomentum);
            mover.UpdateNextVerticalVelocityVector(verticalVelocityVector);

            if (controller.isGrounded)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
            }
        }
    }
}
