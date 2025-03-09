using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP
{
    public class PlayerFallingState : PlayerAirborneState
    {
        private PlayerFallData fallData;

        private Vector3 playerPositionOnEnter;

        public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            fallData = airborneData.FallData;
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            playerPositionOnEnter = stateMachine.Player.transform.position;

            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            ResetVerticalVelocity();
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            LimitVerticalVelocity();
        }

        #endregion


        #region Reusable Methods

        protected override void ResetSprintingState()
        {
            // Not to reset shouldSprintingState only when enter Jumping state & Falling state
        }

        protected override void OnContactWithGround(Collider collider)
        {
            float fallDistance = Mathf.Abs(playerPositionOnEnter.y - stateMachine.Player.transform.position.y);

            if (fallDistance < fallData.MinimumDistanceToBeConsideredHardFall)
            {
                stateMachine.ChangeState(stateMachine.LightLandingState);

                return;
            }

            // Plyaer can't transition rolling state after falling if player walk toggle was on 
            // except player was sprinting before transit to falling state
            if (stateMachine.ReusableData.ShouldWalk && !stateMachine.ReusableData.ShouldSprint 
                ||stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.HardLandingState);

                return;
            }

            stateMachine.ChangeState(stateMachine.RollingState);

        }

        #endregion


        #region Main Methods

        private void LimitVerticalVelocity()
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            if (playerVerticalVelocity.y >= -fallData.FallSpeedLimit)
            {
                return;
            }

            Vector3 toLimitedVelocity = new Vector3(0f, -fallData.FallSpeedLimit - playerVerticalVelocity.y, 0f);

            stateMachine.Player.Rigidbody.AddForce(toLimitedVelocity, ForceMode.VelocityChange);
        }

        #endregion
    }
}
