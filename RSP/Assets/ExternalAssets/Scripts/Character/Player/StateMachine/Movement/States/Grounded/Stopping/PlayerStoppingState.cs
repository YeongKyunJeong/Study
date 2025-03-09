using UnityEngine.InputSystem;

namespace RSP
{
    public class PlayerStoppingState : PlayerGroundedState
    {
        public PlayerStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = 0f;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // Whene ever we enter Stopping States, it will finish rotating even though we're not pressing on a Movement key 
            RotateTowardsTargetRotation();

            if (!IsMovingHorizontally())
            {
                return;
            }
            DecelerateHorizontally();
        }

        public override void OnAnimationTransitionEvent()
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }
        #endregion

        #region Reusable Methods
        // It is never able to enter a Sopping State with our "Movement" Input keys already pressed
        // So we can add a callback to our Movement started action instead of using Update method 
        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            // stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;  <= This will be not called

            stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
        }

        #endregion

        #region Input Methods
        // This is not called beacuse OnAnimationTransition and we already only enter Stoppint State when we have already stopped input
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
        }

        private void OnMovementStarted(InputAction.CallbackContext context)
        {
            OnMove();
        }
        #endregion
    }
}
