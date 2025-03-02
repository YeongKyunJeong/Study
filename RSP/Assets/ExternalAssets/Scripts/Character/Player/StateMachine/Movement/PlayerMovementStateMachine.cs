using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public Player Player { get; }
        public PlayerStateReusableData ReusableData { get; }
        public PlayerIdlingState IdlingStates { get; }
        public PlayerWalkingState WalkingStates { get; }
        public PlayerRunningState RunningStates { get; }
        public PlayerSprintingState SprintingStates { get; }

        public PlayerMovementStateMachine(Player player)
        {
            Player = player;
            ReusableData = new PlayerStateReusableData();
            
            IdlingStates = new PlayerIdlingState(this);

            WalkingStates = new PlayerWalkingState(this);
            RunningStates = new PlayerRunningState(this);
            SprintingStates = new PlayerSprintingState(this);
        }
    }
}
