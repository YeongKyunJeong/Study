using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class ActionStateMachineForEnemy : StateMachine
    {
        private Enemy enemy;
        private MoverForEnemy mover;
        private Animator animator;

        private readonly int instantHitHash = Animator.StringToHash("Hit");
        private readonly int instantDyingHash = Animator.StringToHash("Dying");

        #region Action States

        public IdlingStateForEnemy IdlingState { get; private set; }
        public ChasingState ChasingState { get; private set; }

        // temporary class type, change later to exact class
        public BaseAttackingStateForEnemy AttackingState { get; private set; }

        #endregion

        public bool IsInAttackingState { get; set; }
        public event Action<bool> AttackingEvent;


        public ActionStateMachineForEnemy(Enemy _enemy)
        {
            enemy = _enemy;

            mover = enemy.Mover;

            animator = enemy.Animator;

            IdlingState = new IdlingStateForEnemy(_enemy, this);

            ChasingState = new ChasingState(_enemy, this);

            AttackingState = new BaseAttackingStateForEnemy(_enemy, this);

            SetDefaultState();
        }

        public override void SetDefaultState()
        {
            ChangeState(IdlingState);
            IsInAttackingState = false;
        }

        public void OnHit()
        {
            // TODO :: Add force
            animator.CrossFadeInFixedTime(instantHitHash, 0.25f);
        }

        public void OnDie()
        {
            enemy.Mover.UpdateNextHorizontalMovementVector(Vector3.zero);
            //enemy.Mover.UpdateNextVerticalVelocityVector(Vector3.zero);
            enemy.Animator.CrossFadeInFixedTime(instantDyingHash, 0.25f);
            currentState = null; // TODO :: Add dyingState
        }

        public void BroadcastAttackingEvent(bool isStart)
        {
            IsInAttackingState = isStart;

            AttackingEvent?.Invoke(isStart);
        }
    }
}
