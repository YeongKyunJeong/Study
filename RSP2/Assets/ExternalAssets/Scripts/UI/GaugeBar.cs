using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class GuageBar : MonoBehaviour
    {
        [field: SerializeField] public Transform currentGauge;

        private void Awake()
        {
            if (currentGauge == null) currentGauge = transform.GetChild(0).transform;
        }

    }
}
