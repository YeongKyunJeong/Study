using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public class PlayerIdlingState : IState
    {
        private readonly int idlingHash = Animator.StringToHash("Idling");
        private readonly int stackIdlingHash = Animator.StringToHash("StackIdling");

        private PlayerStateMachine stateMachine;
        private Mover mover;
        private Joystick joystick;
        private Animator animator;
        private RuntimeDataForPlayer runtimeData;

        public PlayerIdlingState(PlayerManager player, PlayerStateMachine _stateMachine)
        {
            runtimeData = player.runtimeData;
            stateMachine = _stateMachine;
            mover = player.Mover;
            joystick = player.Joystick;
            animator = player.Animator;
        }

        public void CallUpdate()
        {
            if(joystick.Horizontal != 0 || joystick.Vertical != 0) 
            {
                stateMachine.ChangeState(stateMachine.WalkingState);
                return;
            }
        }

        public void Enter()
        {
            if (runtimeData.isCarryingBread) 
            {
                animator.Play(stackIdlingHash, 0);
            }
            else
            {
                animator.Play(idlingHash, 0);
            }

            mover.UpdateMoveVector(Vector2.zero);
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
