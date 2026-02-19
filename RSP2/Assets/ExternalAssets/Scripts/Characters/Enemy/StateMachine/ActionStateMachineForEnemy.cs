using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using Random = UnityEngine.Random;

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

        private MeleeAttackingStateForEnemy MeleeAttackingState { get; set; }
        public SkillAttackingStateForEnemy skillAttackingState { get; private set; }

        #endregion

        //public bool IsInAttackingState { get; set; }

        private RuntimeDataForEnemy runtimeData;
        int attackNum;
        public float CoolTimeEndTime { get => coolTimeEndTime; }
        private float coolTimeEndTime;
        private bool isCoolTime = false;
        private float attackCoolTime;

        private AttackType BasicAttackType { get; set; }
        public event Action<bool> AttackingEvent;


        public ActionStateMachineForEnemy(Enemy _enemy)
        {
            enemy = _enemy;

            mover = _enemy.Mover;

            animator = _enemy.Animator;

            runtimeData = _enemy.RuntimeData;

            // enemy.RuntimeData.isChasingStartEvent += SetDefaultState;

            IdlingState = new IdlingStateForEnemy(_enemy, this);

            ChasingState = new ChasingState(_enemy, this);

            BasicAttackType = enemy.AttackDataArray[0].AttackType;

            if (BasicAttackType == AttackType.MeleeAttackSkill)
            {
                MeleeAttackingState = new MeleeAttackingStateForEnemy(_enemy, this);
            }
            else
            {
                // TODO :: Add RangeAttackingState
                //RangeAttackingState = new RangeAttackingStateForEnemy(_enemy, this);
            }

            attackNum = enemy.AttackDataArray.Length;
            skillAttackingState = new SkillAttackingStateForEnemy(_enemy, this);

            attackCoolTime = 0;

            SetDefaultState();
        }

        public override void CallUpdate()
        {
            base.CallUpdate();

            if (isCoolTime)
            {
                float restCoolTime = coolTimeEndTime - Time.time;

                if(restCoolTime <= 0)
                {
                    runtimeData.ResetCoolTime();
                    attackCoolTime = 0;
                    isCoolTime = false;
                }
                else
                {
                    runtimeData.SetCoolTime(restCoolTime);
                }
            }

        }

        public override void ChangeState(IState nextState)
        {
            if (!enemy.RuntimeData.IsHostile) return;

            base.ChangeState(nextState);
        }

        public override void SetDefaultState()
        {
            base.ChangeState(IdlingState);
            //IsInAttackingState = false;
        }

        public void OnHit()
        {
            // TODO :: Add force
            animator.CrossFadeInFixedTime(instantHitHash, 0.25f);
        }

        public void OnDie()
        {
            //enemy.Mover.UpdateNextHorizontalMovementVector(Vector3.zero);

            enemy.Animator.CrossFadeInFixedTime(instantDyingHash, 0.25f);
            currentState.Exit();
            currentState = null; // TODO :: Add dyingState
        }

        public void ChangeAttackState(int attackNum = -1)
        {
            if (attackNum == -1)
            {
                attackNum = Random.Range(0, attackNum);
            }

            if (attackNum == 0)
            {
                ChangeToBasicAttackState();
            }
            else
            {
                ChangeStateWithAttackData(skillAttackingState, attackNum);
            }
        }


        public void ChangeToBasicAttackState()
        {
            switch (BasicAttackType)
            {
                case AttackType.MeleeAttackSkill:
                    {
                        ChangeState(MeleeAttackingState);
                        break;
                    }
                case AttackType.RangeAttackSkill:
                    {
                        //ChangeState(RangeAttackingState);
                        break;
                    }
            }
        }

        public bool ApplyAttackCoolTime(float coolTime, bool ignoreBeforeCoolTime = false)
        {
            if (!ignoreBeforeCoolTime && isCoolTime)
            {
                return false;
            }

            attackCoolTime = coolTime;
            coolTimeEndTime = Time.time + coolTime;
            isCoolTime = true;

            return true;
        }

        private IEnumerator StartAttackCoolTimeCoroutine(float coolTime)
        {
            while (runtimeData.RestAttackCoolTime > 0)
            {

            }

            yield return null;

        }

    }
}
