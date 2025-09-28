using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bakery
{
    public class CustomerStateMachine : StateMachine
    {
        private Customer customer;

        #region States

        private CustomerIdlingState idlingState;
        public CustomerIdlingState IdlingState { get => idlingState; }
        private CustomerWalkingState walkingState;
        public CustomerWalkingState WalkingState { get => walkingState; }

        #endregion

        public CustomerStateMachine(Customer _customer) 
        {
            customer = _customer;

            idlingState = new CustomerIdlingState(_customer, this);
            walkingState = new CustomerWalkingState(_customer, this);

            ChangeState(idlingState);
        }

    }
}
