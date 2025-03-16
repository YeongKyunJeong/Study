using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class PlayerGroundedState : PlayerMovementState
    {
        public PlayerGroundedState(Player _player, PlayerMovementStateMachine _stateMachine) : base(_player, _stateMachine)
        {
        }
    }
}
