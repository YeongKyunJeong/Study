using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class LandDashingStateForPlayer : OnLandStateForPlayer
    {
        private Vector3 dashMovementVector;
        private Vector3 dampedDashVector;
        private float passedTime;

        private float lerpModifier;
        private float durationTime;
        private float fallingStartTime;
        private Vector3 dampedGravity;
        private Vector3 dampedFallingVelocity;

        public LandDashingStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
            //dampedGravity = Physics.gravity*movementStateData.DashFallingModifier;
        }

        public override void Enter()
        {
            base.Enter();

            dampedGravity = Physics.gravity * movementStateData.DashFallingModifier;
            dampedFallingVelocity = Vector3.zero;

            lerpModifier = movementStateData.DashingLerpModifier;
            durationTime = movementStateData.DashingDurationTime;
            fallingStartTime = movementStateData.DashFallingDelay * durationTime;

            if (moveInput == Vector2.zero)
            {
                dashMovementVector = movementStateData.DashingSpeedModifier * player.transform.forward.normalized;
            }
            else
            {
                moveInput = runtimeData.MoveInput;
                dashMovementVector = movementStateData.DashingSpeedModifier * InputToDirectionVectorConverter.ConvertInputToMovementDirectionVector(moveInput);
            }

            runtimeData.HorizontalMovementVector = dashMovementVector;
            dampedDashVector = movementStateData.DashingEndSpeedModifier * dashMovementVector;
            passedTime = 0;
        }

        public override void CallUpdate()
        {
            base.CallUpdate();

            mover.UpdateNextHorizontalMovementVector(dashMovementVector);
            runtimeData.HorizontalMovementVector = dashMovementVector;


            passedTime += Time.deltaTime;

            if (passedTime < fallingStartTime)
            {
                mover.UpdateNextVerticalVelocityVector(dampedFallingVelocity);

            }
            else
            {
                dashMovementVector = Vector3.Lerp(dashMovementVector, dampedDashVector, lerpModifier);


                if (passedTime > durationTime)
                {
                    if (CheckFalling(dampedFallingVelocity))
                    {
                        stateMachine.ChangeState(stateMachine.FallingState);
                        return;

                    }

                    if (moveInput == Vector2.zero)
                    {
                        stateMachine.ChangeState(stateMachine.IdlingState);

                        return;
                    }

                    if (runtimeData.IsWalking)
                    {
                        stateMachine.ChangeState(stateMachine.WalkingState);
                    }

                    stateMachine.ChangeState(stateMachine.RunnigState);

                    return;
                }

            }

            //horizontalMovementVector = ApplySpeedModifierToMovementVector() * InputToDirectionVectorConverter.ConvertInputToMovementDirectionVector(moveInput);
            //runtimeData.HorizontalMovementVector = horizontalMovementVector;

            //mover.UpdateNextHorizontalMovementVector(horizontalMovementVector);
        }

        private void ApplyDampedFalling()
        {

        }
    }
}
