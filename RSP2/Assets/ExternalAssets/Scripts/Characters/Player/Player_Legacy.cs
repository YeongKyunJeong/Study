using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class Player_Legacy : MonoBehaviour
    {
        //public class Player : MonoBehaviour
        //{

        //private float fixedDeltaTime;

        //private Vector3 horizontalMovementVector;

        //private float currentYAngle;
        //private float currentTargetYAngle;
        //private float targetYAngleOnCalculating;
        //private float dampedTargetRotationPassedTime;
        //private float smoothedRotationAngle;
        //private Quaternion smoothedRotationQuternion;

        //private Vector3 forward;
        //private Vector3 right;

        //private Vector3 normalizedTargetRotationDir;
        //private float rotationLerpT;
        //private float rotationLerpTFixedUpdate;


        //    public void MoveCall(bool updateTargetRotation = true)
        //    {
        //        horizontalMovementVector = CalculateMovementVector();
        //        RuntimeData.HorizontalMovementVector = horizontalMovementVector;

        //        Controller.Move(horizontalMovementVector);

        //        if (updateTargetRotation)
        //        {
        //            //Rotate(horizontalMovementVector);
        //            RotateByEulerAngle(horizontalMovementVector);
        //        }
        //    }
        //private void RotateByEulerAngle(Vector3 targetDir)
        //{
        //    currentTargetYAngle = GetTargetDirAngle(targetDir);

        //    UpdateTargetYAngleAndResetPassedTime();

        //    //currentYAngle = Rigidbody.rotation.eulerAngles.y;
        //    currentYAngle = transform.rotation.eulerAngles.y;

        //    if (currentYAngle == currentTargetYAngle)
        //    {
        //        return;
        //    }

        //    smoothedRotationAngle = Mathf.SmoothDampAngle(currentYAngle, RuntimeData.CurrentTargetYAngle,
        //        ref RuntimeData.DampedTargetRotationCurrentVelocity.y,
        //        RuntimeData.TimeToReachTargetYRotation.y - RuntimeData.DampedTargetRotationPassedTime.y);

        //    RuntimeData.DampedTargetRotationPassedTime.y += Time.deltaTime;

        //    smoothedRotationQuternion = Quaternion.Euler(0f, smoothedRotationAngle, 0f);
        //    transform.rotation = smoothedRotationQuternion;
        //}
        //private void UpdateTargetYAngleAndResetPassedTime()
        //{
        //    if (currentTargetYAngle != RuntimeData.CurrentTargetYAngle)
        //    {
        //        RuntimeData.CurrentTargetYAngle = currentTargetYAngle;
        //        RuntimeData.DampedTargetRotationPassedTime.y = 0;
        //        Debug.Log("Update");
        //    }
        //}

        //private float GetTargetDirAngle(Vector3 targetDir)
        //{
        //    targetYAngleOnCalculating = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
        //    if (targetYAngleOnCalculating < 0)
        //    {
        //        targetYAngleOnCalculating += 360;
        //    }
        //    return targetYAngleOnCalculating;
        //}

        //private void Rotate(Vector3 targetDir)
        //{
        //    if (UpdateTargetRotationDir(targetDir))
        //    {
        //        rotationLerpT = 0;
        //    }

        //    if (rotationLerpT >= 1)
        //    {
        //        return;
        //    }

        //    rotationLerpT += RuntimeData.RotationLerpUpdate;
        //    // if (rotationLerpT > 1) { rotationLerpT = 1; }
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(RuntimeData.TargetRotationDir), rotationLerpT);

        //    // Vector3 eulerAngles = transform.rotation.eulerAngles;
        //    // eulerAngles.y += 3 * Time.fixedDeltaTime;
        //    // transform.rotation.eulerAngles = eulerAngles;

        //    //targetRotationAngle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
        //    //transform.rotation = Quaternion.LookRotation(CalculateMovementVector());
        //}


        //private bool UpdateTargetRotationDir(Vector3 targetRotationDir)
        //{
        //    //targetRotationAngle 

        //    normalizedTargetRotationDir = targetRotationDir.normalized;
        //    if (CheckRotationDirChanged())
        //    {
        //        return false;
        //    };
        //    //Debug.Log(normalizedTargetRotationDir);
        //    RuntimeData.TargetRotationDir = normalizedTargetRotationDir;
        //    return true;
        //}

        //private bool CheckRotationDirChanged()
        //{
        //    return Mathf.Abs(normalizedTargetRotationDir.x - RuntimeData.TargetRotationDir.x) < 0.05f
        //                    && Mathf.Abs(normalizedTargetRotationDir.z - RuntimeData.TargetRotationDir.z) < 0.05f;
        //}

        //private Vector3 CalculateMovementVector()
        //{
        //    forward = mainCameraTransform.forward;
        //    right = mainCameraTransform.right;

        //    forward.y = 0f;
        //    right.y = 0f;

        //    forward.Normalize();
        //    right.Normalize();

        //    return (forward * RuntimeData.MovementInput.y + right * RuntimeData.MovementInput.x)
        //        * RuntimeData.MovementSpeedModifier;
        //}



        //}

        ///

    }
}
