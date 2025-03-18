using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class Player : MonoBehaviour
    {
        private PlayerMovementStateMachine movementStateMachine;

        [field: SerializeField] public PlayerInputReader InputReader { get; private set; }
        [field: SerializeField] public CharacterController Controller { get; private set; }
        [field: SerializeField] public Transform mainCameraTransform { get; private set; }
        //[field: SerializeField]
        public Rigidbody Rigidbody { get; private set; }

        public PlayerRuntimeData RuntimeData { get; private set; }

        private float fixedDeltaTime;

        private Vector3 horizontalMovementVector;

        private float currentYAngle;
        private float currentTargetYAngle;
        private float targetYAngleOnCalculating;
        private float dampedTargetRotationPassedTime;
        private float smoothedRotationAngle;
        private Quaternion smoothedRotationQuternion;

        private Vector3 forward;
        private Vector3 right;

        private Vector3 normalizedTargetRotationDir;
        private float rotationLerpT;
        private float rotationLerpTFixedUpdate;

        private void Awake()
        {
            RuntimeData = new PlayerRuntimeData();
            fixedDeltaTime = Time.fixedDeltaTime;

            movementStateMachine = new PlayerMovementStateMachine(this);


            if (InputReader == null)
            {
                throw new NotImplementedException("Player Input Reader Not Assigned");
            }

            InputReader.Initialize(this);
            InputReader.MovementEvent += ReadAndSaveMovementInput;

            if (Controller == null)
            {
                throw new NotImplementedException("Character Controller Not Assigned");
            }

            if (mainCameraTransform == null)
            {
                throw new NotImplementedException("Main Camera Transform Not Assigned");
            }

            //if (Rigidbody == null)
            //{
            //    throw new NotImplementedException("Rigidbody Not Assigned");
            //}
            Rigidbody = Controller.attachedRigidbody;
        }

        private void Start()
        {
        }

        private void Update()
        {
            movementStateMachine.DeliverUpdate();
        }

        private void FixedUpdate()
        {
            movementStateMachine.DeliverPhysicsUpdate();
        }


        private void ReadAndSaveMovementInput(Vector2 newMovementInput)
        {
            RuntimeData.MovementInput = newMovementInput;
        }

        public void MoveCall(bool updateTargetRotation = true)
        {
            horizontalMovementVector = CalculateMovementVector();
            RuntimeData.HorizontalMovementVector = horizontalMovementVector;

            Controller.Move(horizontalMovementVector);

            if (updateTargetRotation)
            {
                //Rotate(horizontalMovementVector);
                RotateByEulerAngle(horizontalMovementVector);
            }
        }

        private void RotateByEulerAngle(Vector3 targetDir)
        {
            currentTargetYAngle = GetTargetDirAngle(targetDir);

            UpdateTargetYAngleAndResetPassedTime();

            //currentYAngle = Rigidbody.rotation.eulerAngles.y;
            currentYAngle = transform.rotation.eulerAngles.y;

            if (currentYAngle == currentTargetYAngle)
            {
                return;
            }

            smoothedRotationAngle = Mathf.SmoothDampAngle(currentYAngle, RuntimeData.CurrentTargetYAngle,
                ref RuntimeData.DampedTargetRotationCurrentVelocity.y,
                RuntimeData.TimeToReachTargetYRotation.y - RuntimeData.DampedTargetRotationPassedTime.y);

            RuntimeData.DampedTargetRotationPassedTime.y += Time.deltaTime;

            smoothedRotationQuternion = Quaternion.Euler(0f, smoothedRotationAngle, 0f);
            transform.rotation = smoothedRotationQuternion;
        }

        private void UpdateTargetYAngleAndResetPassedTime()
        {
            if (currentTargetYAngle != RuntimeData.CurrentTargetYAngle)
            {
                RuntimeData.CurrentTargetYAngle = currentTargetYAngle;
                RuntimeData.DampedTargetRotationPassedTime.y = 0;
                Debug.Log("Update");
            }
        }

        private float GetTargetDirAngle(Vector3 targetDir)
        {
            targetYAngleOnCalculating = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
            if (targetYAngleOnCalculating < 0)
            {
                targetYAngleOnCalculating += 360;
            }
            return targetYAngleOnCalculating;
        }

        private void Rotate(Vector3 targetDir)
        {
            if (UpdateTargetRotationDir(targetDir))
            {
                rotationLerpT = 0;
            }

            if (rotationLerpT >= 1)
            {
                return;
            }

            rotationLerpT += RuntimeData.RotationLerpUpdate;
            // if (rotationLerpT > 1) { rotationLerpT = 1; }
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(RuntimeData.TargetRotationDir), rotationLerpT);

            // Vector3 eulerAngles = transform.rotation.eulerAngles;
            // eulerAngles.y += 3 * Time.fixedDeltaTime;
            // transform.rotation.eulerAngles = eulerAngles;

            //targetRotationAngle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.LookRotation(CalculateMovementVector());
        }


        private bool UpdateTargetRotationDir(Vector3 targetRotationDir)
        {
            //targetRotationAngle 

            normalizedTargetRotationDir = targetRotationDir.normalized;
            if (CheckRotationDirChanged())
            {
                return false;
            };
            //Debug.Log(normalizedTargetRotationDir);
            RuntimeData.TargetRotationDir = normalizedTargetRotationDir;
            return true;
        }

        private bool CheckRotationDirChanged()
        {
            return Mathf.Abs(normalizedTargetRotationDir.x - RuntimeData.TargetRotationDir.x) < 0.05f
                            && Mathf.Abs(normalizedTargetRotationDir.z - RuntimeData.TargetRotationDir.z) < 0.05f;
        }

        private Vector3 CalculateMovementVector()
        {
            forward = mainCameraTransform.forward;
            right = mainCameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            return (forward * RuntimeData.MovementInput.y + right * RuntimeData.MovementInput.x)
                * RuntimeData.MovementSpeedModifier;
        }

        //public void SubscribeMovementEvent(Action<Vector2> action)
        //{
        //    InputReader.MovementEvent += action;
        //}

        //public void CancelMovementEvent(Action<Vector2> action)
        //{
        //    InputReader.MovementEvent -= action;
        //}
    }

}
