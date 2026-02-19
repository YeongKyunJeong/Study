using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RSP2
{
    public abstract class StateMachine
    {
        protected IState currentState;

        public virtual void ChangeState(IState nextState)
        {
            currentState?.Exit();
            currentState = nextState;
            currentState.Enter();
        }

        public virtual void ChangeStateWithAttackData(IState nextAttackState, int dataKey)
        {
            currentState?.Exit();
            currentState = nextAttackState;
            currentState.Enter(dataKey);
        }

        public virtual void CallUpdate()
        {
            currentState?.CallUpdate();
        }

        public virtual void CallPhysicsUpdate()
        {
            currentState?.CallPhysicsUpdate();
        }

        public virtual void CallOnAnimationEnterEvent()
        {
            currentState?.OnAnimationEnterEvent();
        }

        public virtual void CallOnAnimationExitEvent()
        {
            currentState?.OnAnimationExitEvent();
        }

        public virtual void CallOnAnimationTransitEvent()
        {
            currentState?.OnAnimationTransitEvent();
        }


        public virtual void SetDefaultState()
        {

        }

    }
}
