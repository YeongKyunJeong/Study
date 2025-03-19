using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class PlayerMovementState : IState
    {
        protected Player player;
        protected MovementStateMachineForPlayer stateMachine;
        protected PlayerRuntimeData runtimeData;

        // To Do : Move Data to SO
        protected float defaultSpeedModifier = 10f;
        protected float speedModifier;
        protected float rotationSpeedModifier;
        //protected float rotationTime = 1;

        public PlayerMovementState(Player _player, MovementStateMachineForPlayer _stateMachine)
        {
            player = _player;
            runtimeData = player.RuntimeData;
            stateMachine = _stateMachine;
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
            //Move();
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
            //if (player.RuntimeData.MovementInput == Vector2.zero)
            //{
            //    return;
            //}

        }

    }
}
