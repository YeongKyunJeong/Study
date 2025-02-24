using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP
{
    public class Player : MonoBehaviour
    {
        private PlayerMovementStateMachine movementStateMachine;

        private void Awake()
        {
            movementStateMachine = new PlayerMovementStateMachine();
        }

        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdlingStates);
        }

        private void Update()
        {
            movementStateMachine.HandleInput();

            movementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }
    }
}
