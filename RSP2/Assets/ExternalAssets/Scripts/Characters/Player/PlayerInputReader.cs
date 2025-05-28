using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP2
{
    public class PlayerInputReader : MonoBehaviour
    {
        private Player player;
        private PlayerInput playerInputComponent;

        public Vector2 MovementInput { get; private set; }
        
        public event Action<Vector2> MoveEvent;
        public event Action JumpEvent;
        public event Action WalkToggleEvent;
        public event Action DashEvent;
        public event Action AttackEvent;
        public event Action AimEvent;
        public event Action QSkillEvent;
        public event Action ESkillEvent;

        //public PlayerInputReader(Player _player)
        //{
        //    player = _player;
        //}

        public void Initialize(Player _player)
        {
            player = _player;
            playerInputComponent = GetComponent<PlayerInput>();
        }

        //private void OnDestroy()
        //{
        //    plyaerInputActions.Disable();
        //}

        //private void Awake()
        //{
        //    player = GetComponent<Player>();
        //}

        public void EnablePlayerInput(bool isOn)
        {
            playerInputComponent.enabled = isOn;
        }

        public void OnMove(InputValue value)
        {
            //if (context.canceled)
            //{
            //    MovementInput = Vector2.zero;
            //}

            MovementInput = value.Get<Vector2>();

            MoveEvent?.Invoke(MovementInput);
        }

        public void OnJump()
        {
            JumpEvent?.Invoke();
        }

        public void OnZoom(InputValue zoomDelta)
        {
            return;
            // TODO :: Use CameraZommer;
        }

        public Vector2 GetMovementInput()
        {
            return MovementInput;
        }

        public void OnWalkToggle()
        {
            WalkToggleEvent?.Invoke();
        }

        public void OnDash()
        {
            DashEvent?.Invoke();
        }

        public void OnAttack()
        {
            AttackEvent?.Invoke();
        }

        public void OnAim()
        {
            Debug.Log("Aim");
            AimEvent?.Invoke();
        }

        public void OnSkillQ()
        {
            QSkillEvent?.Invoke();
        }

        public void OnSkillE()
        {
            Debug.Log("E");
            ESkillEvent?.Invoke();
        }
    }
}
