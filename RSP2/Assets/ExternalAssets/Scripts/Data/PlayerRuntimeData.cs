using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class PlayerRuntimeData
    {
        public bool IsWalking;

        public PlayerRuntimeData()
        {
            IsWalking = false;
        }

        public Vector2 MoveInput { get; set; }
        public Vector3 HorizontalMovementVector { get; set; }
        public Vector3 VerticalVelocityVector { get; set; }

        //public float MovementSpeedModifier { get; set; } = 1;


        //public Vector3 TargetRotationDir { get; set; }
        //public float RotationLerpUpdate { get; set; }

        //public float RotationSpeedModifier { get; set; }

        //public float CurrentTargetYAngle { get; set; }
        //private Vector3 dampedTargetRotationCurrentVelocity;
        //public ref Vector3 DampedTargetRotationCurrentVelocity
        //{
        //    get
        //    {
        //        return ref dampedTargetRotationCurrentVelocity;
        //    }
        //}
        //private Vector3 dampedTargetRotationPassedTime;
        //public ref Vector3 DampedTargetRotationPassedTime
        //{
        //    get
        //    {
        //        return ref dampedTargetRotationPassedTime;
        //    }
        //}
        //private Vector3 timeToReachTargetYRotation;
        //public ref Vector3 TimeToReachTargetYRotation
        //{
        //    get
        //    {
        //        return ref timeToReachTargetYRotation;
        //    }
        //}


    }
}
