using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine stateMachine;

        protected PlayerGroundedData movementData;

        #region Fields for Caching
        private Vector3 movementDirection;
        private float movementSpeed;
        private Vector3 currentPlayerHorizontalVelocity;
        private Vector3 playerHorizontalVelocity;
        private float directionAngle;
        #endregion

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;

            movementData = stateMachine.Player.Data.GroundedData;

            InitializeData();
        }

        private void InitializeData()
        {
            stateMachine.ReusableData.TimeToReachTargetRotation = movementData.BaseRotatiopnData.TargetRotaionReachTime;
        }

        #region IState Methods
        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name);

            AddInputActionsCallbacks();
        }


        public virtual void Exit()
        {
            RemoveInputActionsCallbacks();
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
            stateMachine.ReusableData.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero || stateMachine.ReusableData.MovementSpeedModifier == 0f)
            {
                return;
            }

            movementDirection = GetMovementInputDirection();
            float targetRotationYAngle = Rotate(movementDirection);
            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);
            movementSpeed = GetMovementSpeed();

            currentPlayerHorizontalVelocity = GetPlayHorizontalVelocity();

            stateMachine.Player.Rigidbody.AddForce(
                movementSpeed * targetRotationDirection - currentPlayerHorizontalVelocity,
                ForceMode.VelocityChange);  // Due to we use FixedUpdate whice is time independent
        }

        private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);
            RotateTowardsTargetRotation();

            return directionAngle;
        }

        private float GetDirectionAngle(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            if (directionAngle < 0)
            {
                directionAngle += 360f;
            }

            return directionAngle;
        }
        private float AddCameraRotationToAngle(float angle)
        {
            angle += stateMachine.Player.MainCameraTransform.eulerAngles.y;
            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }

        private void UpdateTargetRotationData(float targetAngle)
        {
            stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;

            stateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0;
        }

        #endregion

        #region Reusable Methods

        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(stateMachine.ReusableData.MovementInput.x, 0f, stateMachine.ReusableData.MovementInput.y);
        }

        protected float GetMovementSpeed()
        {
            return movementData.BaseSpeed * stateMachine.ReusableData.MovementSpeedModifier;
        }

        protected Vector3 GetPlayHorizontalVelocity()
        {
            playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0;
            return playerHorizontalVelocity;
        }

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;

            if (currentYAngle == stateMachine.ReusableData.CurrentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.ReusableData.CurrentTargetRotation.y,
                ref stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, stateMachine.ReusableData.TimeToReachTargetRotation.y - stateMachine.ReusableData.DampedTargetRotationPassedTime.y);

            stateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
            stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
        }

        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            float directionAngle = GetDirectionAngle(direction);
            if (shouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }

            if (directionAngle != stateMachine.ReusableData.CurrentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }

        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        protected void ResetVelocity()
        {
            // Only use rigidbody.velocity when we need to make sure it is an instaneous change
            stateMachine.Player.Rigidbody.velocity = Vector3.zero;
        }

        protected virtual void AddInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
        }


        protected virtual void RemoveInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
        }
        #endregion

        #region Input Methods
        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            stateMachine.ReusableData.ShouldWalk = !stateMachine.ReusableData.ShouldWalk;
        }
        #endregion
    }
}
