using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class ActionStateMachineForEnemy : StateMachine
    {
        private Enemy enemy;

        private readonly int instantDyingHash = Animator.StringToHash("Dying");

        #region Action States

        public IdlingStateForEnemy IdlingState { get; private set; }
        public ChasingState ChasingState { get; private set; }

        // temporary class type, change later to exact class
        public ActionStateForEnemy BasicAttackingState { get; private set; }

        #endregion


        public ActionStateMachineForEnemy(Enemy _enemy)
        {
            enemy = _enemy;

            IdlingState = new IdlingStateForEnemy(_enemy, this);

            ChasingState = new ChasingState(_enemy, this);

            SetDefaultState();
        }

        public override void SetDefaultState()
        {
            ChangeState(IdlingState);
        }

        public void OnDie()
        {
            enemy.Animator.CrossFadeInFixedTime(instantDyingHash, 0.25f);
            currentState = null; // TODO :: Add dyingState
        }
    }
}
