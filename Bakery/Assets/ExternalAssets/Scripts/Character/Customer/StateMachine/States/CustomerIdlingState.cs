using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Bakery
{
    public class CustomerIdlingState : IState
    {
        private readonly int idlingHash = Animator.StringToHash("Idling");
        private readonly int stackIdlingHash = Animator.StringToHash("CarryingIdling");

        private Customer customer;
        private CustomerStateMachine stateMachine;
        private Animator animator;
        private NavMeshAgent agent;
        private bool isCarrying;

        public CustomerIdlingState(Customer _customer, CustomerStateMachine _stateMachine)
        {
            customer = _customer;
            stateMachine = _stateMachine;
            animator = _customer.Animator;
            agent = _customer.Agent;

            isCarrying = false;
        }

        public void CallUpdate()
        {
            if (!customer.isArrived)
            {
                stateMachine.ChangeState(stateMachine.WalkingState);
                return;
            }

            if (customer.carryingCount == 0 && isCarrying)
            {
                isCarrying = false;
                animator.Play(idlingHash, 0);
            }
            else if(customer.carryingCount != 0 && !isCarrying)
            {
                isCarrying = true;
                animator.Play(stackIdlingHash, 0);
            }
        }

        public void Enter()
        {
            if (customer.carryingCount == 0)
            {
                isCarrying = false;
                animator.Play(idlingHash, 0);
            }
            else
            {
                isCarrying = true;
                animator.Play(stackIdlingHash, 0);
            }
        }

        public void Exit()
        {

        }
    }
}
