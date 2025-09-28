using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

namespace Bakery
{
    public class Customer : PooledObject
    {
        [field: SerializeField] private CustomerStateMachine stateMachine;
        public CustomerStateMachine StateMachine { get => stateMachine; }

        [field: SerializeField] private Animator animator;
        public Animator Animator { get => animator; }

        public Vector3 target;
        public NavMeshAgent Agent;
        public int type;
        public int requirement;
        public int carryingCount;
        public bool isArrived;
        public bool isCarrying;

        public void Initialize(int _type, int _requirement, Vector3 _target, Vector3 from)
        {
            if (Agent == null)
            {
                Agent = GetComponent<NavMeshAgent>();
            }
            Agent.stoppingDistance = 0.01f;

            carryingCount = 0;
            if (stateMachine == null)
                stateMachine = new CustomerStateMachine(this);
            else
                stateMachine.ChangeState(stateMachine.IdlingState);

            target = _target;
            isArrived = false;
            isCarrying = false;

            transform.position = from;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            SetNewTarget(_target);
        }

        public void CallUpdate()
        {
            stateMachine.CallUpdate();
        }

        public void SetNewTarget(Vector3 newTarget) 
        {
            target = newTarget;
            Agent.destination = target;
            isArrived = false;
        }
    }
}
