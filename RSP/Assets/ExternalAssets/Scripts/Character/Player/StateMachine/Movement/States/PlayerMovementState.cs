using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine stateMachine;

        protected Vector2 movementInput;

        protected float baseSpeed = 5f;
        protected float speedModifier = 1f;

        #region Field for Caching
        private Vector3 movementDirection;
        private float movementSpeed;
        private Vector3 currentPlayerHorizontalVelocity;
        private Vector3 playerHorizontalVelocity;
        #endregion

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;
        }
        #region IState Methods
        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name);
        }

        public virtual void Exit()
        {
        }

        public virtual void HandleInput()
        {
            ReadMovementInput();
        }


        public virtual void Update()
        {
        }

        public virtual void PhysicsUpdate()
        {
            Move();
        }
        #endregion

        #region Main Methods
        private void ReadMovementInput()
        {
            movementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            if (movementInput == Vector2.zero || speedModifier == 0f)
            {
                return;
            }

            movementDirection = GetMovementInputDirection();
            movementSpeed = GetMovementSpeed();
            currentPlayerHorizontalVelocity = GetPlayHorizontalVelocity();
            stateMachine.Player.Rigidbody.AddForce(
                movementSpeed * movementDirection - currentPlayerHorizontalVelocity,
                ForceMode.VelocityChange);  // Due to we use FixedUpdate whice is time independent
        }


        #endregion

        #region Reusable Methods

        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(movementInput.x, 0f, movementInput.y);
        }

        protected float GetMovementSpeed()
        {
            return baseSpeed * speedModifier;
        }

        protected Vector3 GetPlayHorizontalVelocity()
        {
            playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0;
            return playerHorizontalVelocity;
        }
        #endregion
    }
}
