using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public class StallForPlayer : Interactable
    {
        public override void Initialize(InteractionManager _interactionManager)
        {
            interactionManager = _interactionManager;
            if (detectionCenterPoint == null) detectionCenter = transform.position;
        }

        public override void Detect()
        {
            detectionCount = Physics.OverlapBoxNonAlloc(detectionCenter, halfSize, hitColliders, Quaternion.identity, detectTargetLayer);

            if (detectionCount > 0)
            {
                OnDetected();
            }
        }

        public override void OnDetected()
        {
            interactionManager.OnInteraction(InteractionType.Stall_Player);
        }
    }
}
