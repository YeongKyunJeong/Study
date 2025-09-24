using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public class PlayerStateMachine : StateMachine
    {

        #region States

        private PlayerIdlingState idlingState;
        private PlayerWalkingState walkingState;

        #endregion


        public PlayerStateMachine(PlayerManager player) 
        {
            //player = _player;

            //mover = player.Mover;

            //forceReceiver = player.ForceReceiver;

            //animator = player.Animator;
        }

    }
}