using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public abstract class StateMachine
    {
        protected IState currentState;

        public void ChangeState(IState nextState)
        {
            currentState?.Exit();

            currentState = nextState;
            Debug.Log(currentState.ToString());

            currentState.Enter();
        }

        //public void DeliverInput()
        //{
        //    currentState?.HandleInput();
        //}

        public void CallUpdate()
        {
            currentState?.CallUpdate();
        }

        public void CallPhysicsUpdate()
        {
            currentState?.CallPhysicsUpdate();
        }

        public void CallOnAnimationEnterEvent()
        {
            currentState?.OnAnimationEnterEvent();
        }

        public void CallOnAnimationExitEvent()
        {
            currentState?.OnAnimationExitEvent();
        }

        public void CallOnAnimationTransitEvent()
        {
            currentState?.OnAnimationTransitEvent();
        }




    }
}
