using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class MovementStateForPlayer : IState
    {
        protected Player player;

        protected PlayerRuntimeData runtimeData;
        protected MovementStateDataForPlayer movementStateData;
        
        protected MovementStateMachineForPlayer stateMachine;
        protected PlayerInputReader inputReader;
        protected PlayerMover mover;


        // To Do : Move Data to SO
        //protected float defaultSpeedModifier = 10f;
        //protected float speedModifier;
        //protected float rotationSpeedModifier;
        //protected float rotationTime = 1;

        public MovementStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine)
        {
            player = _player;

            runtimeData = player.RuntimeData;
            movementStateData = player.SOData.MovementStateData;

            stateMachine = _stateMachine;
            inputReader = player.InputReader;
            mover = player.Mover;

        }

        #region IState Methods
        public virtual void Enter()
        {
            inputReader.MoveEvent += OnMoveInput;
            inputReader.JumpEvent += OnJumpInput;
        }

        public virtual void Exit()
        {
            inputReader.MoveEvent -= OnMoveInput;
            inputReader.JumpEvent -= OnJumpInput;
        }

        public virtual void CallPhysicsUpdate()
        {
            //Move();
        }


        public virtual void CallUpdate()
        {
        }

        //public virtual void HandleInput()
        //{
        //}

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

        #region Movement State Method
        protected virtual void OnMoveInput(Vector2 moveInput) { }

        protected virtual void OnJumpInput() { }


        #endregion


    }
}
