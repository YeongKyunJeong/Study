namespace RSP
{
    public class PlayerLightStoppingState : PlayerStoppingState
    {
        public PlayerLightStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }


        #region IState Method
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementDecelerationForce = groundedMovementData.StopData.LightDecelerationForce;

            stateMachine.ReusableData.CurrentJumpForce = airborneData.JumpData.WeakForce;

        }
        #endregion

    }
}
