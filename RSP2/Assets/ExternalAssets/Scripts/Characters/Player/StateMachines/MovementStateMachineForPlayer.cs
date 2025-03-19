using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class MovementStateMachineForPlayer : StateMachine
    {
        private Player player;

        #region Grounded States



        #endregion
        PlayerIdlingState IdlingState;

        #region Airborne States



        #endregion


        public MovementStateMachineForPlayer(Player _player)
        {
            player = _player;

            // To Do : Intialize States
            IdlingState = new PlayerIdlingState(_player, this);
         
            SetDefaultState();
        }

        private void SetDefaultState()
        {
            ChangeState(IdlingState);
        }
    }
}
