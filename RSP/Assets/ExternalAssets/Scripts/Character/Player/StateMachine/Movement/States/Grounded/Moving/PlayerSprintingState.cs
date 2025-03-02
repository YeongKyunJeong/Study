using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP
{
    public class PlayerSprintingState : PlayerMovingState
    {
        public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
    }
}
