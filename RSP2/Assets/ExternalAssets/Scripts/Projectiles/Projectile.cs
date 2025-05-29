using System;
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
        [field: SerializeField] private TrailRenderer trailRenderer;
        [field: SerializeField] public ProjectileData Data { get; private set; }
        protected Collider hitBoxCollider;
        public Collider HitBoxCollider
        {
            get
            {
                return hitBoxCollider;
            }

            private set
            {
                hitBoxCollider = value;
            }
        }
        private Rigidbody rigidbd;

        protected LayerMask targetLayerMask;
        public LayerMask TargetLayerMask { get { return targetLayerMask; } }

        public event Action<CombatSystem, Collider> EnterEvent;
        protected CombatSystem hitCombatSystem;
        private Collider hitCollider;

        private Vector3 frontDir;
        private Vector3 resultVelocity;

        private float flyingDistanceSqr;
        private float distanceLimit;
        private float speedSqr;

        private float startTime;

        protected HashSet<Collider> detectedTarget;
        private CombatSystem shooterCombatSystem;
        private float speedModifier;

        private void Awake()
        {
            hitBoxCollider = GetComponent<Collider>();
            rigidbd = GetComponent<Rigidbody>();
            detectedTarget = new HashSet<Collider>();
            targetLayerMask = 1 << LayerMask.NameToLayer("Combat Unit");
            if (transform.childCount == 0)
            {
                Instantiate(Model, transform);
            }
            else
            {
                Model = transform.GetChild(0).gameObject;
            }
        }


        public void CallUpdate()
        {
            rigidbd.position += speedModifier * Time.deltaTime * resultVelocity;

            switch (Data.RangeType)
            {
                case ProjectileRangeType.ByTime:
                    {
                        if (Time.time - startTime >= Data.ShootTimelimit)
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

        public void SetData(CombatSystem newShooter, ProjectileData newTrack, float newSpeedModifier)
        {
            shooterCombatSystem = newShooter;
            Data = newTrack;
            transform.position += ConvertVectorByTransformSpace(Data.ShootPosition);
            resultVelocity = ConvertVectorByTransformSpace(Data.ShootVelocity);
            speedModifier = newSpeedModifier;
            EnterEvent = null;

            switch (Data.RangeType)
            {
                case ProjectileRangeType.ByTime:
                    {
                        startTime = Time.time;
                        break;
                    }
                case ProjectileRangeType.ByDistance:
                    {
                        speedSqr = Data.ShootVelocity.sqrMagnitude;
                        distanceLimit = Data.ShootDistanceLimit * Data.ShootDistanceLimit;
                        flyingDistanceSqr = 0;
                        break;
                    }
            }
            trailRenderer.Clear();
        }

        private Vector3 ConvertVectorByTransformSpace(Vector3 vector)
        {
            return vector.x * transform.right + vector.y * transform.up + vector.z * transform.forward;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & targetLayerMask.value) == 0) return;

            Debug.Log($"{other.name} hit");
            ;
            FindCombatSystemAndCallEvent(other);
        }

        private void FindCombatSystemAndCallEvent(Collider other)
        {
            if (detectedTarget.Contains(other)) return;

            detectedTarget.Add(other);
            hitCombatSystem = other.GetComponent<CombatSystem>();

            if (hitCombatSystem == null) return;

            EnterEvent?.Invoke(hitCombatSystem, other);
            return;
        }


        //private void OnDisable()
        //{
        //    if (ProjectileManager.Instance == null) return;

        //    ProjectileManager.DeHash(this);
        //}

    }
}
