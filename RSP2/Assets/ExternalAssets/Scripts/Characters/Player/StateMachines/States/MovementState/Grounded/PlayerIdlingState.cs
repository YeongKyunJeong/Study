using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
            //defaultSpeedModifier = 0;
            defaultSpeedModifier = 5f;
            //rotationTime = 0.14f;
            rotationSpeedModifier = 8;
        }

        public override void Enter()
        {
            base.Enter();

            player.RuntimeData.MovementSpeedModifier = defaultSpeedModifier;
            player.RuntimeData.RotationSpeedModifier = rotationSpeedModifier;
            //player.RuntimeData.TimeToReachTargetYRotation.y = rotationTime;
            //player.RuntimeData.RotationLerpUpdate = Time.fixedDeltaTime/(rotationTime);
        }
    }
}
