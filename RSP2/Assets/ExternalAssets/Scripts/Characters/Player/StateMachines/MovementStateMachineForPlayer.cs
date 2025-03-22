using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class MovementStateMachineForPlayer : StateMachine
    {
        private Player player;
        
        public IState CurrentState { get; private set; }
        
        #region OnLand States
        public IdlingStateForPlayer IdlingState { get; private set; }
        public WalkingStateForPlayer WalkingState { get; private set; }
        public RunnigStateForPlayer RunnigState { get; private set; }
        #endregion

        #region InAir States
        public JumpingStateForPlayer JumpingState { get; private set; }

        public FallingStateForPlayer FallingState { get; private set; }

        #endregion


        public MovementStateMachineForPlayer(Player _player)
        {
            player = _player;

            // To Do : Intialize States
            IdlingState = new IdlingStateForPlayer(_player, this);

            WalkingState = new WalkingStateForPlayer(_player, this);

            RunnigState = new RunnigStateForPlayer(_player, this);

            JumpingState = new JumpingStateForPlayer(_player, this);

            FallingState = new FallingStateForPlayer(_player, this);

            SetDefaultState();
        }

        private void SetDefaultState()
        {
            ChangeState(IdlingState);
        }
    }
}
