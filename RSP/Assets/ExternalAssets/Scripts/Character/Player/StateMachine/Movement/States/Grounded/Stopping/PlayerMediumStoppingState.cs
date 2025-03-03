namespace RSP
{
    public class PlayerMediumStoppingState : PlayerStoppingState
    {
        public PlayerMediumStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }


        #region IState Method
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.MediumDecelerationForce;
        }
        #endregion

    }
}
