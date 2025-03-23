using UnityEngine;

namespace RSP
{
    [System.Serializable]
    public class PlayerRotationData
    {
        [field: SerializeField] public Vector3 TargetRotaionReachTime { get; private set; }
    }
}
