using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP
{
    public class PlayerRollingState : PlayerLandingState
    {
        private PlayerRollData rollData;

        public PlayerRollingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            rollData = groundedMovementData.RollData;
        }


        #region IState Methods

        public override void Enter()
        {
            stateMachine.ReusableData.MovementSpeedModifier = rollData.SpeedModifier;

            base.Enter();

            // We able to move in the Rolling State, and only enter this state when if we were pressing a Movement Input key
            // So 'stateMachine.ReusableData.ShouldSprint' of Grounded State will never called
            // We will do not keep sprinting after rolling
            stateMachine.ReusableData.ShouldSprint = false;
            
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if(stateMachine.ReusableData.MovementInput != Vector2.zero)
            {
                return;
            }

            // Only when there is no input
            RotateTowardsTargetRotation();
        }

        public override void OnAnimationTransitionEvent() // We only added this transition only at last frame 
        {
            if(stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.MediumStoppingState);

                return;
            }

            OnMove();
        }

        #endregion


        #region Input Methods
        protected override void OnJumpStated(InputAction.CallbackContext context)
        {
            // Not to transit Jumping State until lasy frame
        }

        #endregion
    }
}
