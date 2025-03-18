using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class KPlayerMovement : MonoBehaviour
    {
        KPlayerInput kPlayerInput;
        
        private Vector2 _movementDirection = Vector2.zero;
        
        void Start()
        {
            kPlayerInput = GetComponent<KPlayerInput>();
            kPlayerInput.MoveEvent += OnMove;
        }

        void FixedUpdate()
        {
            ApplyMovement(_movementDirection);
        }
        
        private void OnMove(Vector2 direction)
        {
            _movementDirection = direction;
        }

        private void ApplyMovement(Vector2 direction)
        {
            transform.Translate(direction);
        }

    }
}
