using UnityEngine.InputSystem;

namespace RSP
{
    public class PlayerWalkingState : PlayerMovingState
    {
        private PlayerWalkData walkData;

        public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            walkData = groundedMovementData.WalkData;

        }

        #region IState Methods

        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = groundedMovementData.WalkData.SpeedModifier;

            stateMachine.ReusableData.BackwardsCameraRecenteringData = walkData.BackwardsCameraRecenteringData;

            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.WeakForce;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);

            SetBaseCameraRecenteringData();
        }

        #endregion


        #region Input Methods
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            //base.OnMovementCanceled(context); // Chage state to Idling state instantly
            stateMachine.ChangeState(stateMachine.LightStoppingState);

            base.OnMovementCanceled(context);
        }

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.RunningState);
        }
        #endregion
    }
}
