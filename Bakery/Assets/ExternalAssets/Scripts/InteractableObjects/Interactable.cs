using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public abstract class Interactable : MonoBehaviour
    {
        [field: SerializeField] protected LayerMask detectTargetLayer;
        [field: SerializeField] protected Transform detectionCenterPoint;
        [field: SerializeField] protected Vector3 halfSize;

        protected InteractionManager interactionManager;
        protected Vector3 detectionCenter;
        protected int detectionCount = 5;

        protected Collider[] hitColliders = new Collider[5];

        public abstract void Initialize(InteractionManager _interactionManager);

        public abstract void Detect();


        public abstract void OnDetected();

    }
}
