using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine stateMachine;
        protected Player player;

        // To Do : Move Data to SO
        protected float defaultSpeedModifier = 1;
        protected float speedModifier;

        public PlayerMovementState(Player _player, PlayerMovementStateMachine _stateMachine)
        {
            stateMachine = _stateMachine;
            player = _player;
        }

        #region IState Methods
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
        public virtual void PhysicsUpdate()
        {
            Move();
        }


        public virtual void Update()
        {
        }

        public virtual void HandleInput()
        {
        }

        public virtual void OnAnimationEnterEvent()
        {
        }

        public virtual void OnAnimationExitEvent()
        {
        }

        public virtual void OnAnimationTransitEvent()
        {
        }

        #endregion


        private void Move()
        {
            if (player.RuntimeData.MovementInput == Vector2.zero)
            {
                Debug.Log("No Movement Input");
                return;
            }

            player.MoveCall();
        }

    }
}
