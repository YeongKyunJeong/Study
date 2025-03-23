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

            StartAnimation(stateMachine.Player.AnimationData.HardStopParameterHash);

            stateMachine.ReusableData.MovementDecelerationForce = groundedMovementData.StopData.HardDecelerationForce;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.HardStopParameterHash);
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
