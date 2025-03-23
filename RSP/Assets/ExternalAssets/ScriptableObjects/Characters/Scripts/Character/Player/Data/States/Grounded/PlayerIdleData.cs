using System.Collections.Generic;
using UnityEngine;

namespace RSP
{
    [System.Serializable]
    public class PlayerIdleData
    {
        [field: SerializeField] public List<PlayerCameraRecenteringData> BackwardsCameraRecenteringData { get; private set; }
    }
}
