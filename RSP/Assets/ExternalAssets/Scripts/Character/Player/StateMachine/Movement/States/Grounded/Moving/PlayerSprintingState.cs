using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP
{
    public class PlayerSprintingState : PlayerMovingState
    {
        private PlayerSprintData sprintData;

        private float startTime;

        private bool keepSprinting;
        public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            sprintData = movementData.SprintData;
        }

        #region IState
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = sprintData.SpeedModifier;

            startTime = Time.time;
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

            keepSprinting = false;
        }
        #endregion

        #region Main Methods
        private void StopSprinting()
        {
            if(stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                // To do : Hard Stopping State
                stateMachine.ChangeState(stateMachine.IdlingStates);

                return;
            }
            stateMachine.ChangeState(stateMachine.RunningStates);
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
        #endregion


        #region Input Methods
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            //base.OnMovementCanceled(context); // Chage state to Idling state instantly
            stateMachine.ChangeState(stateMachine.HardStoppingStates);
        }

        private void OnSprintPerform(InputAction.CallbackContext context)
        {
            keepSprinting = true;
        }
        #endregion
    }
}
