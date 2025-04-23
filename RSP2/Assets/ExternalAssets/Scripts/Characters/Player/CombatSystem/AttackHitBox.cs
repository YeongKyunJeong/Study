using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class AttackHitBox : MonoBehaviour
    {
        private Collider hitBoxCollider;
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
        private HashSet<Collider> detectedTarget;

        private Transform hitBoxTransform;
        public Transform HitBoxTransform
        {
            get
            {
                return hitBoxTransform;
            }

            private set
            {
                hitBoxTransform = value;
            }
        }

        public event Action<CombatSystem> HitEvent;
        private CombatSystem hitCombatSystem;

        private LayerMask targetLayerMask;

        
        public bool IsEnabled { get { return hitBoxCollider.enabled; } }

        private void Awake()
        {
            hitBoxCollider = GetComponent<Collider>();
            hitBoxTransform = transform;
            detectedTarget = new HashSet<Collider>();
            Deactivate();
            if (GetComponent<Player>() == null && GetComponent<Enemy>() == null)
                targetLayerMask = 1 << LayerMask.NameToLayer("Combat Unit");
            //Debug.Log(hitBoxCollider.name);
        }

        public void Initialize(LayerMask _targetLayerMask)
        {
            targetLayerMask = _targetLayerMask;
        }

        public void Activate()
        {
            if (!hitBoxCollider.enabled)
            {
                detectedTarget.Clear();
                hitBoxCollider.enabled = true;
            }
        }

        public void Deactivate()
        {
            if (hitBoxCollider.enabled)
                hitBoxCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & targetLayerMask.value) == 0) return;

            if (detectedTarget.Contains(other)) return;

            detectedTarget.Add(other);
            hitCombatSystem = other.GetComponent<CombatSystem>();

            if (hitCombatSystem == null) return;

            HitEvent?.Invoke(hitCombatSystem);
            Debug.Log($"'{other.gameObject.name}' is in the target layer mask!");

        }

    }
}
