using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class JumpingStateForPlayer : InAirStateForPlayer
    {
        public JumpingStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}
