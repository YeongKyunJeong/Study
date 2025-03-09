using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP
{
    public class PlayerLandingState : PlayerGroundedState
    {
        public PlayerLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }


        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            // Not to transit to Idling State
        }

        #endregion
    }
}
