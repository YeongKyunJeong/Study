using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public enum ProjectileRangeType
    {
        ByTime,
        ByDistance
    }

    [System.Serializable]
    public struct Track
    {
        public Vector3 ShootVelocity;
        public Vector3 ShootPosition;
        public ProjectileRangeType RangeType;
        public float ShootStartTime;

        public float ShootDistanceLimit;
        public float ShootTimelimit;
    }


    public class Projectile : PooledObject
    {
        [field: SerializeField] public GameObject Model { get; private set; }
        [field: SerializeField] public Collider Collider { get; private set; }
        [field: SerializeField] public Track Track { get; private set; }
        public Track SetTrack { set => Track = value; }

        private Rigidbody rigidbd;

        private float flyingDistanceSqr;
        private float distanceLimit;
        private float speedSqr;

        private float startTime;

        private void Awake()
        {
            if (transform.childCount == 0)
            {
                Instantiate(Model, transform);
            }
            else
            {
                Model = transform.GetChild(0).gameObject;
            }
            rigidbd = GetComponent<Rigidbody>();
            switch (Track.RangeType)
            {
                case ProjectileRangeType.ByTime:
                    {
                        startTime = Time.time;
                        break;
                    }
                case ProjectileRangeType.ByDistance:
                    {
                        speedSqr = Track.ShootVelocity.magnitude;
                        flyingDistanceSqr = 0;
                        break;
                    }
            }

        }

        public void CallUpdate()
        {
            rigidbd.position += Track.ShootVelocity * Time.deltaTime;

            switch (Track.RangeType)
            {
                case ProjectileRangeType.ByTime:
                    {
                        if (Time.time - startTime >= Track.ShootTimelimit)
                        {
                            ReturnToPool();
                        }
                        break;
                    }
                case ProjectileRangeType.ByDistance:
                    {
                        flyingDistanceSqr += speedSqr * Time.deltaTime;
                        if (flyingDistanceSqr >= Track.ShootDistanceLimit)
                        {
                            ReturnToPool(); 
                        }
                        break;
                    }
            }
        }

    }
}
