namespace RSP
{
    public class PlayerHardStoppingState : PlayerStoppingState
    {
        public PlayerHardStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }


        #region IState Method
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.HardDecelerationForce;
        }
        #endregion


        #region Reusable Methods
        protected override void OnMove()
        {
            // As we can't transition to Hard Stopping State to Walking State
            if (stateMachine.ReusableData.ShouldWalk)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.RunningState); 
        }
        #endregion
    }
}
