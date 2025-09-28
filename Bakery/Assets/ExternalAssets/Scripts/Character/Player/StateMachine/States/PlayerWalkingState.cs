using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bakery
{
    public class PlayerWalkingState : IState
    {
        private readonly int walkingHash = Animator.StringToHash("Walking");
        private readonly int stackWalkingHash = Animator.StringToHash("CarryingWalking");

        private PlayerStateMachine stateMachine;
        private Mover mover;
        private Joystick joystick;
        private Animator animator;
        private PlayerRuntimeData runtimeData;
        private bool isCarrying;

        public PlayerWalkingState(PlayerManager player, PlayerStateMachine _stateMachine)
        {
            runtimeData = player.runtimeData;
            stateMachine = _stateMachine;
            mover = player.Mover;
            joystick = player.Joystick;
            animator = player.Animator;
            isCarrying = false;
        }


        public void CallUpdate()
        {
            if (runtimeData.isCarryingBread && !isCarrying)
            {
                isCarrying = true;
                animator.Play(stackWalkingHash, 0);
            }
            else if( !runtimeData.isCarryingBread && isCarrying) 
            {
                isCarrying = false;
                animator.Play(walkingHash, 0);
            }

            if (joystick.Horizontal == 0 && joystick.Vertical == 0)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }

            Vector2 inputVector = new Vector2(joystick.Horizontal, joystick.Vertical);
            mover.UpdateMoveVector(inputVector);
        }


        public void Enter()
        {
            if (runtimeData.isCarryingBread)
            {
                isCarrying = true;
                animator.Play(stackWalkingHash, 0);
            }
            else
            {
                isCarrying = false;
                animator.Play(walkingHash, 0);
            }
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
