using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public class PlayerStateMachine : StateMachine
    {
        private PlayerManager player;

        #region States

        private PlayerIdlingState idlingState;
        private PlayerWalkingState walkingState;

        #endregion


        public PlayerStateMachine(PlayerManager _player) 
        {
            player = _player;

            idlingState = new PlayerIdlingState(_player);
            walkingState = new PlayerWalkingState(_player);
            //animator = player.Animator;
        }

    }
}