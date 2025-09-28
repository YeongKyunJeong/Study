using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void CallUpdate();
    }
}