using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class FallingStateForPlayer : InAirStateForPlayer
    {
        public FallingStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            verticalVelocityVector = runtimeData.VerticalVelocityVector;
            Debug.Log("    "+runtimeData.VerticalVelocityVector);
            mover.UpdateNextVerticalVelocityVector(verticalVelocityVector);
        }

        public override void CallUpdate()
        {
            base.CallUpdate();

            CheckIsFirstUpdate();

            SendHorizontalMovementData();

            CheckIsGrounded();
        }

        private void CheckIsFirstUpdate()
        {
            if (isFirstFixedUpdate)
            {
                isFirstFixedUpdate = false;
            }
            else
            {
                ApplyFallingToVector(ref verticalVelocityVector, Time.deltaTime);
            }
        }

        private void CheckIsGrounded()
        {
            if (controller.isGrounded)
            {
                runtimeData.VerticalVelocityVector = Physics.gravity * Time.deltaTime;

                if (moveInput == Vector2.zero)
                {
                    Debug.Log("No Input : Falling State");
                    stateMachine.ChangeState(stateMachine.IdlingState);
                    return;
                }

                if (runtimeData.IsWalking)
                {
                    stateMachine.ChangeState(stateMachine.WalkingState);
                    return;
                }
                stateMachine.ChangeState(stateMachine.RunnigState);


            }
        }

        private void SendHorizontalMovementData()
        {
            runtimeData.VerticalVelocityVector = verticalVelocityVector;

            mover.UpdateNextHorizontalMovementVector(horizontalMomentum);
            mover.UpdateNextVerticalVelocityVector(verticalVelocityVector);
        }


        #region Movement Input Method

        protected override void OnMoveInput(Vector2 _moveInput)
        {
            base.OnMoveInput(_moveInput);
        }

        #endregion
    }
}
