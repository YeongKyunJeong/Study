using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class SkillAttackingStateForEnemy : MeleeAttackingStateForEnemy
    {
        private readonly int instantAttackHash = Animator.StringToHash("Attack.SkillAttack");
        private readonly int isSkillHash = Animator.StringToHash("IsSkill");
        private readonly int skillIndexHash = Animator.StringToHash("SkillIndex");

        public SkillAttackingStateForEnemy(Enemy _enemy, ActionStateMachineForEnemy _stateMachine) : base(_enemy, _stateMachine)
        {

        }

        public override void Enter(int dataKey)
        {
            attackData = enemy.AttackDataArray[dataKey];
            base.Enter();

        }

        protected override void SetAnimatorSelfStateParameter(bool isOn)
        {
                if (isOn)
                {
                    animator.SetFloat(skillIndexHash, attackData.AnimationKey);
                    if (animator.IsInTransition(0))
                    {
                        animator.CrossFadeInFixedTime(instantAttackHash, 0.25f);
                    }
                }
                animator.SetBool(isSkillHash, isOn);
        }
    }
}
