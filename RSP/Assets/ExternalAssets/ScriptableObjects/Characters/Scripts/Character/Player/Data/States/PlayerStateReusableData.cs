using System.Collections.Generic;
using UnityEngine;

namespace RSP
{
    /// <summary>
    /// Holds Data that needs to be reused between States (such as the shouldWalk or movementInput variables)
    /// </summary>
    public class PlayerStateReusableData
    {
        public Vector2 MovementInput { get; set; }
        public float MovementSpeedModifier { get; set; } = 1;
        public float MovementOnSlopesSpeedModifier { get; set; } = 1;
        public float MovementDecelerationForce { get; set; } = 1;   // How fast we want to decelerate our player

        public List<PlayerCameraRecenteringData> SidewaysCameraRecenteringData { get; set; }
        public List<PlayerCameraRecenteringData> BackwardsCameraRecenteringData { get; set; }

        public bool ShouldWalk { get; set; }
        public bool ShouldSprint { get; set; }

        private Vector3 currentTargetRotation;
        private Vector3 timeToReachTargetRotation;
        private Vector3 dampedTargetRotationCurrentVelocity;
        private Vector3 dampedTargetRotationPassedTime;

        public ref Vector3 CurrentTargetRotation
        {
            get
            {
                return ref currentTargetRotation;
            }
        }
        public ref Vector3 TimeToReachTargetRotation
        {
            get
            {
                return ref timeToReachTargetRotation;
            }
        }
        public ref Vector3 DampedTargetRotationCurrentVelocity
        {
            get
            {
                return ref dampedTargetRotationCurrentVelocity;
            }
        }
        public ref Vector3 DampedTargetRotationPassedTime
        {
            get
            {
                return ref dampedTargetRotationPassedTime;
            }
        }
        public Vector3 CurrentJumpForce { get; set; }

        public PlayerRotationData RotationData { get; set; }
    }
}
