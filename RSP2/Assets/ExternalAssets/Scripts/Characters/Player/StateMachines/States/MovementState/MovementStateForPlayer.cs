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

        protected Transform mainCameraTransform;

        protected MovementStateMachineForPlayer stateMachine;
        protected PlayerInputReader inputReader;
        protected PlayerMover mover;
        protected CharacterController controller;


        protected Vector2 moveInput;
        //protected float fixedDeltaTime;

        public MovementStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine)
        {
            player = _player;

            runtimeData = player.RuntimeData;
            movementStateData = player.SOData.MovementStateData;

            //mainCameraTransform = Camera.main.transform;

            stateMachine = _stateMachine;
            inputReader = player.InputReader;
            mover = player.Mover;
            controller = player.Controller;

            //fixedDeltaTime = Time.fixedDeltaTime;
        }

        #region IState Methods
        public virtual void Enter()
        {
            inputReader.MoveEvent += OnMoveInput;
            inputReader.JumpEvent += OnJumpInput;
            inputReader.WalkToggleEvent += OnWalkToggleInput;
            inputReader.DashEvent += OnDashInput;

            moveInput = runtimeData.MoveInput;
        }

        public virtual void Exit()
        {
            inputReader.MoveEvent -= OnMoveInput;
            inputReader.JumpEvent -= OnJumpInput;
            inputReader.WalkToggleEvent -= OnWalkToggleInput;
            inputReader.DashEvent -= OnDashInput;
        }

        public virtual void CallUpdate()
        {
            //Move();

        }


        public virtual void CallPhysicsUpdate()
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


        #region Movement Input Method

        protected virtual void OnMoveInput(Vector2 _moveInput)
        {
            moveInput = _moveInput;
            runtimeData.MoveInput = moveInput;
        }

        protected virtual void OnJumpInput() { }

        protected virtual void OnWalkToggleInput()
        {
            runtimeData.IsWalking = !runtimeData.IsWalking;
        }

        protected virtual void OnDashInput() { }

        #endregion


    }

    public static class InputToDirectionVectorConverter
    {
        static Transform mainCameraTransform = Camera.main.transform;
        //static Vector3 horizontalMovementVector;
        static Vector3 forward;
        static Vector3 right;

        public static Vector3 ConvertInputToMovementDirectionVector(Vector3 input)
        {
            forward = mainCameraTransform.forward;
            right = mainCameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            return (forward * input.y + right * input.x);
        }

    }



}
