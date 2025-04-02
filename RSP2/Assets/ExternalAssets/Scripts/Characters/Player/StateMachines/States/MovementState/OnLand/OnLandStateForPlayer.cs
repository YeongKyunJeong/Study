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
        private Vector3 slopeDetectingRayVector;
        private float slopeDetectingRayMaxDistance;
        private Vector3 slopeDetectingRayStartHeightVector;
        private Vector3 floatingHeightVector;
        private LayerMask groundLayer;

        protected Vector3 slopeNormalVecor;

        public OnLandStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
            //isFallingCount = 0;
            playerTransform = _player.transform;
            //fallingThreshold = Physics.gravity.y * movementStateData.FallingThreshoildMultiplier;
            slopeDetectingRayStartHeightVector = Vector3.up * movementStateData.SlopeDetectingRayStartHeight;
            slopeDetectingRayVector = Vector3.down * (movementStateData.RaycastDistance + movementStateData.SlopeDetectingRayStartHeight);
            slopeDetectingRayMaxDistance = movementStateData.RaycastDistance + movementStateData.SlopeDetectingRayStartHeight;
            floatingHeightVector = Vector3.up * (movementStateData.FloatingHeight);

            groundLayer = movementStateData.GroundLayer;
        }


        #region IStateMethods

        public override void Enter()
        {
            base.Enter();

            SetAnimatorOnLandParameter(true);
        }

        public override void Exit()
        {
            base.Exit();

            SetAnimatorSelfStateParameter(false);
        }

        #endregion


        protected override void OnJumpInput()
        {
            base.OnJumpInput();

            SetAnimatorOnLandParameter(false);

            stateMachine.ChangeState(stateMachine.JumpingState);
        }

        protected override void OnDashInput()
        {
            base.OnDashInput();

            stateMachine.ChangeState(stateMachine.LandDashingState);
        }

        protected override void OnAttackInput()
        {
            base.OnAttackInput();

            SetAnimatorOnLandParameter(false);

            stateMachine.ChangeState(stateMachine.LandAttackState);
        }


        protected virtual bool CheckFalling(Vector3 fallingVelocityVector, Vector3 slopeNormalVector)
        {
            if (slopeNormalVecor.y < -0.98f)
            {
                fallingThreshold =  5*Physics.gravity.y * Time.deltaTime;
                if (!controller.isGrounded && (fallingVelocityVector.y < fallingThreshold))
                {
                    return true;
                }

            }

            return false;
        }

        protected virtual Vector3 CheckIsSlope(bool stickFloor = true)
        {
            //Debug.DrawRay(playerTransform.position + slopeDetectingRayStartHeightVector, slopeDetectingRayVector, Color.green);

            if(Physics.Raycast(playerTransform.position + slopeDetectingRayStartHeightVector, Vector3.down, out hit, slopeDetectingRayMaxDistance,
                groundLayer))
            {
                //Debug.Log("hit");
                slopeNormalVector = hit.normal;
                //if (stickFloor)
                //{
                //    //mover.SetHeightManually(hit.point + floatingHeightVector);

                //    //controller.enabled = false;
                //    //playerTransform.position = hit.point + floatingHeightVector;
                //    //controller.enabled = true;
                //    //mover.
                //}
            }
            else
            {
                //Debug.Log("no floor");
                slopeNormalVector = Vector3.down;
            }

            return slopeNormalVector;
        }


    }
}
