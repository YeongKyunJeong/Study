using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
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

        public void CallUpdate()
        {
            currentState?.CallUpdate();
        }

        public void CallOnAnimationEnterEvent()
        {
            currentState?.OnAnimationEnterEvent();
        }

        public void CallOnAnimationExitEvent()
        {
            currentState?.OnAnimationExitEvent();
        }
    }
}