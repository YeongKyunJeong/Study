using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class AttackStateForPlayer : MovementStateForPlayer
    {
        protected Vector3 horizontalMomentum;
        protected float passedTime;


        public AttackStateForPlayer(Player _player, MovementStateMachineForPlayer _stateMachine) : base(_player, _stateMachine)
        {

        }


    }





}
