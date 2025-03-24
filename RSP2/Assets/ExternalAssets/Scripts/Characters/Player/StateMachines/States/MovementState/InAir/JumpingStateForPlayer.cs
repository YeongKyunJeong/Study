using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class JumpingStateForPlayer : InAirStateForPlayer
    {
        public JumpingStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            verticalVelocityVector += new Vector3(0, movementStateData.JumpForceModifier, 0);
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

            if (verticalVelocityVector.y <= 0)
            {
                runtimeData.VerticalVelocityVector = Vector3.zero;
                stateMachine.ChangeState(stateMachine.FallingState);
                return;
            }
        }
    }
}
