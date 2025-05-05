using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]
    public class ConsumableDataLibrary
    {
        [field: SerializeField] public List<ConsumableData> ConsumableData { get; private set; }
    }
}
