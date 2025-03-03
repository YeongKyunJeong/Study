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
        public PlayerDashingState DashingStates { get; }
        public PlayerWalkingState WalkingStates { get; }
        public PlayerRunningState RunningStates { get; }
        public PlayerSprintingState SprintingStates { get; }
        public PlayerLightStoppingState LightStoppingStates { get; }
        public PlayerMediumStoppingState MediumStoppingStates { get; }
        public PlayerHardStoppingState HardStoppingStates { get; }


        public PlayerMovementStateMachine(Player player)
        {
            Player = player;
            ReusableData = new PlayerStateReusableData();

            IdlingStates = new PlayerIdlingState(this);
            DashingStates = new PlayerDashingState(this);
            WalkingStates = new PlayerWalkingState(this);
            RunningStates = new PlayerRunningState(this);
            SprintingStates = new PlayerSprintingState(this);

            LightStoppingStates = new PlayerLightStoppingState(this);
            MediumStoppingStates = new PlayerMediumStoppingState(this);
            HardStoppingStates = new PlayerHardStoppingState(this);
        }
    }
}
