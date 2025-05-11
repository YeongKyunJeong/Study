using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace RSP2
{
    public class SkillMeleeAttackingStateForPlayer : MeleeAttackingState
    {
        private readonly int instantComboAttackHash = Animator.StringToHash("Attack.ComboAttack");
        private readonly int isComboHash = Animator.StringToHash("IsCombo");

        public SkillMeleeAttackingStateForPlayer(Player _player, ActionStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {

        }

        public override void CallUpdate()
        {
            base.CallUpdate();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Enter(int dataKey)
        {
            attackData = attackDataLibrary.AttackDataList[dataKey];
            base.Enter();
            // TODO :: Add resource using logic
        }
        
        protected override void SetAnimatorSelfStateParameter(bool isOn)
        {
            if (animator.IsInTransition(0))
            {
                animator.CrossFadeInFixedTime(instantComboAttackHash, 0.25f);
            }
            animator.SetBool(isComboHash, isOn);
        }

    }
}
