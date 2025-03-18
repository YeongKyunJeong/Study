using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP2
{
    public class KPlayerInput : MonoBehaviour
    {
        public Action<Vector2> MoveEvent;
        public Action JumpEvent;
        public Action LookEvent;

        // X
        // InputValue value
        // InputAction.CallbackContext context
        public void OnMovement(InputValue value)
        {
            MoveEvent?.Invoke(value.Get<Vector2>());
            Debug.Log("Movement: " + value.Get<Vector2>());
        }
        
        public void OnJump()
        {
            JumpEvent?.Invoke();
        }
        

    }
}
