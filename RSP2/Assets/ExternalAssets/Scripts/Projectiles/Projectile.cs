using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public enum ProjectileRangeType
    {
        None,
        ByTime,
        ByDistance
    }

    [System.Serializable]
    public struct ProjectileData
    {
        [field: SerializeField] public string poolTag { get; private set; }

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
        [field: SerializeField] public ProjectileData data { get; private set; }
        private CombatSystem shooterCombatSystem;

        private Rigidbody rigidbd;

        private float flyingDistanceSqr;
        private float distanceLimit;
        private float speedSqr;

        private float startTime;

        private float speedModifier;

        private void Awake()
        {
            rigidbd = GetComponent<Rigidbody>();
            
            if (transform.childCount == 0)
            {
                Instantiate(Model, transform);
            }
            else
            {
                Model = transform.GetChild(0).gameObject;
            }
        }

        public void SetData(CombatSystem newShooter, ProjectileData newTrack, float newSpeedModifier)
        {
            data = newTrack;
            shooterCombatSystem = newShooter;
            speedModifier = newSpeedModifier;

            switch (data.RangeType)
            {
                case ProjectileRangeType.ByTime:
                    {
                        startTime = Time.time;
                        break;
                    }
                case ProjectileRangeType.ByDistance:
                    {
                        speedSqr = data.ShootVelocity.sqrMagnitude;
                        distanceLimit = data.ShootDistanceLimit * data.ShootDistanceLimit;
                        flyingDistanceSqr = 0;
                        break;
                    }
            }
        }

        public void CallUpdate()
        {
            rigidbd.position += speedModifier * Time.deltaTime * data.ShootVelocity;

            switch (data.RangeType)
            {
                case ProjectileRangeType.ByTime:
                    {
                        if (Time.time - startTime >= data.ShootTimelimit)
                        {
                            ReturnToPool();
                        }
                        break;
                    }
                case ProjectileRangeType.ByDistance:
                    {
                        flyingDistanceSqr += speedSqr * Time.deltaTime;
                        if (flyingDistanceSqr >= distanceLimit)
                        {
                            ReturnToPool();
                        }
                        break;
                    }
            }
        }

        //private void OnDisable()
        //{
        //    if (ProjectileManager.Instance == null) return;

        //    ProjectileManager.DeHash(this);
        //}

    }
}
