using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Bakery
{
    public class Mover : MonoBehaviour
    {
        private CharacterController controller;
        private Transform playerTransform;
        private float speed;
        private Vector3 movementVector;
        private bool moving;

        public void Initialize(PlayerManager _player)
        {
            controller = _player.Controller;
            playerTransform = _player.transform;
            speed = _player.Speed;
            moving = false;
        }

        public void UpdateMoveVector(Vector2 inputVector)
        {
            if (inputVector.sqrMagnitude < 0.1f)
            {
                movementVector = Vector2.zero;
                moving = false;
                return;
            }

            movementVector = new Vector3(inputVector.x, 0, inputVector.y);
            movementVector = movementVector.normalized;
            playerTransform.rotation = Quaternion.LookRotation(movementVector);
            moving = true;
        }

        public void CallMoveUpdate()
        {
            if (moving)
            {
                controller.Move(movementVector * (speed * Time.deltaTime));
            }
            else
            {
                controller.Move(Vector3.zero);
            }

        }
    }
}
