using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class JumpingStateForPlayer : InAirStateForPlayer
    {
        protected readonly int isJumpingHash = Animator.StringToHash("IsJumping");

        public JumpingStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            SetAnimatorSelfStateParameter(true);

            verticalVelocityVector += new Vector3(0, movementStateData.JumpForceModifier, 0);
        }

        public override void Exit()
        {
            base.Exit();

            SetAnimatorSelfStateParameter(false);
        }

        public override void CallUpdate()
        {
            base.CallUpdate();

            if (isFirstFixedUpdate)
            {
                isFirstFixedUpdate = false;
            }
            else
            {
                 ApplyFallingToVector(ref verticalVelocityVector, Time.deltaTime);
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

        protected override void SetAnimatorSelfStateParameter(bool isOn)
        {
            //base.SetAnimatorSelfStateParameter(isOn);

            animator.SetBool(isJumpingHash, isOn);
        }
    }
}
