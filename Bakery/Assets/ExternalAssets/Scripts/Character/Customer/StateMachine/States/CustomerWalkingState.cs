using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Bakery
{
    public class CustomerWalkingState : IState
    {
        private readonly int walkingHash = Animator.StringToHash("Walking");
        private readonly int stackWalkingHash = Animator.StringToHash("CarryingWalking");

        private Customer customer;
        private CustomerStateMachine stateMachine;
        private Animator animator;
        private NavMeshAgent agent;
        private bool isCarrying;

        public CustomerWalkingState(Customer _customer, CustomerStateMachine _stateMachine)
        {
            customer = _customer;
            stateMachine = _stateMachine;
            animator = _customer.Animator;
            agent = _customer.Agent;

            isCarrying = false;
        }

        public void CallUpdate()
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                customer.isArrived = true;
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }

            if (customer.carryingCount == 0 && isCarrying)
            {
                isCarrying = false;
                animator.Play(walkingHash, 0);
            }
            else if (customer.carryingCount != 0 && !isCarrying)
            {
                isCarrying = true;
                animator.Play(stackWalkingHash, 0);
            }
        }

        public void Enter()
        {
            if (customer.carryingCount == 0)
            {
                isCarrying = false;
                animator.Play(walkingHash, 0);
            }
            else
            {
                isCarrying = true;
                animator.Play(stackWalkingHash, 0);
            }
        }

        public void Exit()
        {

        }
    }
}
