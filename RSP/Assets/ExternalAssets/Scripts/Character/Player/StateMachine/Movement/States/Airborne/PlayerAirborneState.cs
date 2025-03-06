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

        #region Reusable Method
        protected override void OnContactWithGround(Collider collider)
        {
            base.OnContactWithGround(collider);

            stateMachine.ChangeState(stateMachine.IdlingStates);
        }
        #endregion
    }
}
