using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class BasicMeleeAttackingStateForPlayer : MeleeAttackingState
    {
        //protected readonly int landAttackingHash = Animator.StringToHash("IsLandAttacking");
        private readonly int instantBasicMeleeAttackHash = Animator.StringToHash("Attack.BasicMeleeAttack");
        // To Do : Add combo attack
        // To Do :Refer to attack data from scriptable object to set the data  

        public BasicMeleeAttackingStateForPlayer(Player _player, ActionStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {
        }


        #region IState Methods

        public override void Enter()
        {
            base.Enter();
            //base.SetAnimatorSelfStateParameter(isOn);
            if (animator.IsInTransition(0))
            {
                animator.CrossFadeInFixedTime(instantBasicMeleeAttackHash, 0.25f);
            }
        }

        #endregion

        protected override void SetHitBoxPosition()
        {
            base.SetHitBoxPosition();
        }


    }
}
