using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP
{
    public class PlayerRunningState : PlayerMovingState
    {
        private PlayerSprintData sprintData;

        private float startTime;

        public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            sprintData = movementData.SprintData;
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.MediumForce;

            startTime = Time.time;
        }

        public override void Update()
        {
            base.Update();

            if (!stateMachine.ReusableData.ShouldWalk)
            {
                return;
            }

            // Only through the case sprintint => running -> idling, else use stateMachine.ChangeState();
            if (Time.time < startTime + sprintData.RunToWalkTime)
            {
                return;
            }
            StopRunning();
        }
        #endregion

        #region Main Method
        private void StopRunning()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                // To do : Midium Stopping State
                stateMachine.ChangeState(stateMachine.IdlingState);

                return;
            }
            stateMachine.ChangeState(stateMachine.WalkingState);
        }
        #endregion


        #region Input Methods
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            //base.OnMovementCanceled(context); // Chage state to Idling state instantly
            stateMachine.ChangeState(stateMachine.MediumStoppingState);
        }

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.WalkingState);
        }
        #endregion

    }
}
