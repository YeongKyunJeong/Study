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
        public PlayerIdlingState IdlingState { get => idlingState; }
        private PlayerWalkingState walkingState;
        public PlayerWalkingState WalkingState { get => walkingState; }

        #endregion


        public PlayerStateMachine(PlayerManager _player)
        {
            player = _player;

            idlingState = new PlayerIdlingState(_player, this);
            walkingState = new PlayerWalkingState(_player, this);

            ChangeState(idlingState);
        }

    }
}