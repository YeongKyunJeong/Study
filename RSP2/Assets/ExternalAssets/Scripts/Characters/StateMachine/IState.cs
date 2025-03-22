using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        //public void HandleInput();
        public void CallUpdate();
        public void CallPhysicsUpdate();

        public void OnAnimationEnterEvent();
        public void OnAnimationExitEvent();
        public void OnAnimationTransitEvent();
        //public void OnTriggerEnter(Collider collider);
        //public void OnTriggerExit(Collider collider);
    }
}
