using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(Player _player, PlayerMovementStateMachine _stateMachine) : base(_player, _stateMachine)
        {
            //defaultSpeedModifier = 0;
            defaultSpeedModifier = 0.1f;
        }

        public override void Enter()
        {
            base.Enter();

            player.RuntimeData.MovementSpeedModifier = defaultSpeedModifier;
        }
    }
}
