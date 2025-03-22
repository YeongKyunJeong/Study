using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class InAirStateForPlayer : MovementStateForPlayer
    {
        public InAirStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
        }
    }
}
