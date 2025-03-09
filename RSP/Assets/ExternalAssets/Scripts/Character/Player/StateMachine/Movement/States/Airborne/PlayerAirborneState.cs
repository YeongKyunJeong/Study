using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP
{
    public class PlayerAirborneState : PlayerMovementState
    {
        public PlayerAirborneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            ResetSprintingState();
        }
        #endregion


        #region Reusable Method
        protected override void OnContactWithGround(Collider collider)
        {
            base.OnContactWithGround(collider);

            stateMachine.ChangeState(stateMachine.LightLandingState);
        }

        protected virtual void ResetSprintingState()
        {
            stateMachine.ReusableData.ShouldSprint = false;
        }
        #endregion
    }
}
