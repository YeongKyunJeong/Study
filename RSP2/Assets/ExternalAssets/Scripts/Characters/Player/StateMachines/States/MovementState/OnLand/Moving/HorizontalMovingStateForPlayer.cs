using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class HorizontalMovingStateForPlayer : OnLandStateForPlayer
    {
        protected Vector3 horizontalMovementVector;
        //protected Vector2 moveInput;

        public HorizontalMovingStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            moveInput = runtimeData.MoveInput;
        }

        public override void CallUpdate()
        {
            base.CallUpdate();
            runtimeData.VerticalVelocityVector = new Vector3(0, controller.velocity.y, 0);

            if (CheckFalling(runtimeData.VerticalVelocityVector))
            {
                stateMachine.ChangeState(stateMachine.FallingState);
                return;
            }

            if (moveInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                //Debug.Log("No Input : Horizontal Moving State");
            }

            horizontalMovementVector = ApplySpeedModifierToMovementVector() * InputToDirectionVectorConverter.ConvertInputToMovementDirectionVector(moveInput);
            runtimeData.HorizontalMovementVector = horizontalMovementVector;

            mover.UpdateNextHorizontalMovementVector(horizontalMovementVector);
        }

        protected virtual float ApplySpeedModifierToMovementVector()
        {
            return 1;
        }

    }

}


