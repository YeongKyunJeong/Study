using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class IdlingStateForPlayer : OnLandStateForPlayer
    {
        public IdlingStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
            //defaultSpeedModifier = 0;
            //rotationTime = 0.14f;

            //defaultSpeedModifier = 5f;
            //rotationSpeedModifier = 8;
        }

        public override void Enter()
        {
            base.Enter();

            mover.UpdateNextHorizontalMovementVector(Vector3.zero);

            //player.RuntimeData.MovementSpeedModifier = defaultSpeedModifier;
            //player.RuntimeData.RotationSpeedModifier = rotationSpeedModifier;

            //player.RuntimeData.TimeToReachTargetYRotation.y = rotationTime;
            //player.RuntimeData.RotationLerpUpdate = Time.fixedDeltaTime/(rotationTime);
        }

        protected override void OnMoveInput(Vector2 moveInput)
        {
            base.OnMoveInput(moveInput);
            runtimeData.MoveInput = moveInput;

            if (runtimeData.IsWalking)
            {
                stateMachine.ChangeState(stateMachine.WalkingState);
            }

            stateMachine.ChangeState(stateMachine.RunnigState);
        }

        protected override void OnJumpInput()
        {
            base.OnJumpInput();
        }
    }
}
