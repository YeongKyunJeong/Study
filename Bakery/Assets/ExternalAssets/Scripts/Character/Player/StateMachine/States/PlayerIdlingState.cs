using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public class PlayerIdlingState : IState
    {
        private readonly int idlingHash = Animator.StringToHash("Idling");
        private readonly int stackIdlingHash = Animator.StringToHash("CarryingIdling");

        private PlayerStateMachine stateMachine;
        private Mover mover;
        private Joystick joystick;
        private Animator animator;
        private PlayerRuntimeData runtimeData;
        private bool isCarrying;

        public PlayerIdlingState(PlayerManager player, PlayerStateMachine _stateMachine)
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
                animator.Play(stackIdlingHash, 0);
            }
            else if (!runtimeData.isCarryingBread && isCarrying)
            {
                isCarrying = false;
                animator.Play(idlingHash, 0);
            }

            if (joystick.Horizontal != 0 || joystick.Vertical != 0) 
            {
                stateMachine.ChangeState(stateMachine.WalkingState);
                return;
            }
        }

        public void Enter()
        {
            if (runtimeData.isCarryingBread) 
            {
                isCarrying = true;
                animator.Play(stackIdlingHash, 0);
            }
            else
            {
                isCarrying = false;
                animator.Play(idlingHash, 0);
            }

            mover.UpdateMoveVector(Vector2.zero);
        }

        public void Exit()
        {

        }
    }
}
