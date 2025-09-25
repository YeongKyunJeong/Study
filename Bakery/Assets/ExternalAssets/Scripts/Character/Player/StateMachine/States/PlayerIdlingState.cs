using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public class PlayerIdlingState : IState
    {
        private Mover mover;
        private Joystick joystick;
        private Animator animator;

        public PlayerIdlingState(PlayerManager player)
        {
            mover = player.Mover;
            joystick = player.Joystick;
            animator = player.Animator;
        }

        public void CallUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void OnAnimationEnterEvent()
        {
            throw new System.NotImplementedException();
        }

        public void OnAnimationExitEvent()
        {
            throw new System.NotImplementedException();
        }
    }
}
