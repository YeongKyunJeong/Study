using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public PlayerIdlingState IdlingStates { get; }
        public PlayerWalkingState WalkingStates { get; }
        public PlayerRunningState RunningStates { get; }
        public PlayerSprintingState SprintingStates { get; }

        public PlayerMovementStateMachine()
        {
            IdlingStates = new PlayerIdlingState();

            WalkingStates = new PlayerWalkingState();
            RunningStates = new PlayerRunningState();
            SprintingStates = new PlayerSprintingState();
        }
    }
}
