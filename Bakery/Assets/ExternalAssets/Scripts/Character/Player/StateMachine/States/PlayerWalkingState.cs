using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bakery
{
    public class PlayerWalkingState : IState
    {
        private Mover mover;
        private Joystick joystick;
        private Animator animator;

        public PlayerWalkingState(PlayerManager player)
        {
            mover = player.Mover;
            joystick = player.Joystick;
            animator = player.Animator;
        }

        public void CallUpdate()
        {
        }
    

        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void OnAnimationEnterEvent()
        {

        }

        public void OnAnimationExitEvent()
        {

        }
    }
}
