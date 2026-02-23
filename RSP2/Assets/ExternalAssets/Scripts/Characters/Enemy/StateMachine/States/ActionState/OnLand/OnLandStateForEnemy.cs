using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class OnLandStateForEnemy : ActionStateForEnemy
    {
        protected readonly int onLandHash = Animator.StringToHash("@OnLand");
        protected Vector3 slopeNormalVector;


        public OnLandStateForEnemy(Enemy _enemy, ActionStateMachineForEnemy _stateMachine) : base(_enemy, _stateMachine)
        {
            isOnLandState = true;
            isAttackingState = false;
        }


        #region IStateMethods

        public override void Enter()
        {
            base.Enter();

            SetAnimatorOnLandParameter(true);
        }

        public override void CallUpdate()
        {
            base.CallUpdate();

            slopeNormalVector = FallingCalculator.CheckIsSlope(enemy.transform);
        }

        public override void Exit()
        {
            base.Exit();

            SetAnimatorSelfStateParameter(false);
        }

        #endregion

        protected virtual void SetAnimatorOnLandParameter(bool isOn)
        {
            animator.SetBool(onLandHash, isOn);
        }

        protected virtual bool DoAttackAndReturn()
        {
            int validCount = 0;
            for (int i = 0; i < runtimeData.attackCount; i++)
            {
                if (IsInAttackRange(i))
                {
                    runtimeData.ValidAttackIndicesBuffer[validCount] = i;
                    validCount++;
                }
            }

            if (validCount > 0)
            {
                int randomIndex = Random.Range(0, validCount);
                int selected = runtimeData.ValidAttackIndicesBuffer[randomIndex];
                SetAnimatorOnLandParameter(false);
                stateMachine.ChangeAttackState(selected);
                return true;
            }
            return false;
        }
    }
}
