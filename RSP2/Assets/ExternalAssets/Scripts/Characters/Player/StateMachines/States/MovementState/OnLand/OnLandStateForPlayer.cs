using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class OnLandStateForPlayer : MovementStateForPlayer
    {
        //protected int isFallingCount;
        protected int fallingThresholdCount = 5;
        protected float fallingThreshold;
        private Vector3 slopeNormalVector;
        private Transform playerTransform;

        private RaycastHit hit;
        private LayerMask groundLayer;

        public OnLandStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
            //isFallingCount = 0;
            playerTransform = _player.transform;
            fallingThreshold = Physics.gravity.y * fallingThreshold;

            groundLayer = movementStateData.GroundLayer;
        }

        public override void Enter()
        {
            base.Enter();

            //isFallingCount = 0;
        }

        protected override void OnJumpInput()
        {
            base.OnJumpInput();

            stateMachine.ChangeState(stateMachine.JumpingState);
        }

        protected override void OnDashInput()
        {
            base.OnDashInput();

            stateMachine.ChangeState(stateMachine.LandDashingState);
        }

        protected virtual bool CheckFalling(Vector3 fallingVelocityVector)
        {
            if(!controller.isGrounded && (fallingVelocityVector.y < fallingThreshold))
            {
                return true;
            }

            return false;
        }

        protected virtual Vector3 CheckIsSlope()
        {

            return slopeNormalVector;
        }
    }
}
