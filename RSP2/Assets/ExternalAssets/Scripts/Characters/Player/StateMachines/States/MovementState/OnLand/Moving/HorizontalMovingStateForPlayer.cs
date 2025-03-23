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

        public override void CallPhysicsUpdate()
        {
            base.CallPhysicsUpdate();

            if (moveInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                Debug.Log("No Input : Horizontal Moving State");
            }

            horizontalMovementVector = movementStateData.MovementSpeedModifier * InputToDirectionVectorConverter.ConvertInputToMovementDirectionVector(moveInput);
            runtimeData.HorizontalMovementVector = horizontalMovementVector;

            mover.UpdateNextHorizontalMovementVector(horizontalMovementVector);
        }

        //protected override void OnMoveInput(Vector2 _moveInput)
        //{
        //    base.OnMoveInput(_moveInput);

        //    moveInput = _moveInput;
        //    runtimeData.MoveInput = moveInput;
        //}



        //static Vector3 horizontalMovementVector;
        //private Vector3 forward;
        //private Vector3 right;

        //private Vector3 ConvertInputToMovementDirectionVector(Vector3 input)
        //{
        //    forward = mainCameraTransform.forward;
        //    right = mainCameraTransform.right;

        //    forward.y = 0f;
        //    right.y = 0f;

        //    forward.Normalize();
        //    right.Normalize();

        //    return (forward * input.y + right * input.x);
        //}
    }

}


