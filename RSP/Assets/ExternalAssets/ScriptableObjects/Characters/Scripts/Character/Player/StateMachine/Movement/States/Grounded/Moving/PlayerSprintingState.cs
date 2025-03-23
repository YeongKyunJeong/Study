using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP
{
    public class PlayerSprintingState : PlayerMovingState
    {
        private PlayerSprintData sprintData;

        private float startTime;

        private bool keepSprinting;
        private bool shouldResetSprintingState;

        public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            sprintData = groundedMovementData.SprintData;
        }


        #region IState
        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = sprintData.SpeedModifier;

            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.SprintParameterHash);

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.StrongForce;

            shouldResetSprintingState = true;

            startTime = Time.time;

            if (!stateMachine.ReusableData.ShouldSprint)
            {
                keepSprinting = false;
            }
        }

        public override void Update()
        {
            base.Update();

            if (keepSprinting)
            {
                return;
            }

            if (Time.time < startTime + sprintData.SprintToRunTime)
            {
                return;
            }

            StopSprinting();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.SprintParameterHash);

            if (shouldResetSprintingState)
            {
                keepSprinting = false;
                stateMachine.ReusableData.ShouldSprint = false;
            }

        }

        #endregion


        #region Main Methods

        private void StopSprinting()
        {
            if(stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                // To do : Hard Stopping State
                stateMachine.ChangeState(stateMachine.IdlingState);

                return;
            }
            stateMachine.ChangeState(stateMachine.RunningState);
        }

        #endregion


        #region Reusable Methods

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Sprint.performed += OnSprintPerform;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Sprint.performed -= OnSprintPerform;
        }

        protected override void OnFall()
        {
            shouldResetSprintingState = false;

            base.OnFall();
        }

        #endregion


        #region Input Methods
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.HardStoppingStates);

            base.OnMovementCanceled(context);
        }

        protected override void OnJumpStated(InputAction.CallbackContext context)
        {
            shouldResetSprintingState = false;

            base.OnJumpStated(context);
        }

        private void OnSprintPerform(InputAction.CallbackContext context)
        {
            keepSprinting = true;
            
            // Only when key was held enough time
            stateMachine.ReusableData.ShouldSprint = true;
        }
        #endregion
    }
}
