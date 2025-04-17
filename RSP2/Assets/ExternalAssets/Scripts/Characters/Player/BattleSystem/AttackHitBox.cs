using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class AttackHitBox : MonoBehaviour
    {
        private LayerMask targetLayerMask;

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

        public bool IsEnabled { get { return hitBoxCollider.enabled; } }

        private void Awake()
        {
            hitBoxCollider = GetComponent<Collider>();
            hitBoxTransform = transform;
            Deactivate();
            targetLayerMask = 1 << LayerMask.NameToLayer("Battle Unit");
            //Debug.Log(hitBoxCollider.name);
        }

        public void Activate()
        {
            if (!hitBoxCollider.enabled)
                hitBoxCollider.enabled = true;
        }

        public void Deactivate()
        {
            if (hitBoxCollider.enabled)
                hitBoxCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & targetLayerMask.value) != 0)
            {
                Debug.Log($"'{other.gameObject.name}' is in the target layer mask!");
            }
        }

    }
}
